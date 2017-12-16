using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Packages;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Common
{
    public interface IComponentHelper
    {
        Task<IHtmlContent> AddWidget(IViewComponentHelper helper, string themeName, string widgetName, object arguments = null);
        Task<IHtmlContent> AddZone(IViewComponentHelper helper, string theme, string zone, string[] defaultWidgets = null);
    }

    public class ComponentHelper : IComponentHelper
    {
        private readonly IViewComponentSelector _selector;
        private readonly IUnitOfWork _db;
        private readonly ILogger _logger;

        public ComponentHelper(IViewComponentSelector selector, IUnitOfWork db, ILogger<ComponentHelper> logger)
        {
            _selector = selector;
            _logger = logger;
            _db = db;
        }

        public async Task<IHtmlContent> AddWidget(IViewComponentHelper helper, string themeName, string widgetName, object arguments = null)
        {
            var key = $"{themeName}-{widgetName}";

            if (Disabled() != null && Disabled().Contains(widgetName))
            {
                return await Task.FromResult(new HtmlString(""));
            }           

            var setting = _db.CustomFields.GetValue(CustomType.Application, 0, key);

            if (string.IsNullOrEmpty(setting))
            {
                var args = arguments == null ? "" : arguments.ToString();
                await _db.CustomFields.SetCustomField(CustomType.Application, 0, key, args);
            }

            try
            {
                return Exists(widgetName)
                ? await helper.InvokeAsync(widgetName, arguments)
                : await Task.FromResult(new HtmlString(""));
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error loading widget: {ex.Message}");
                return await Task.FromResult(new HtmlString(""));
            }
        }

        public async Task<IHtmlContent> AddZone(IViewComponentHelper helper, string theme, string zone, string[] defaultWidgets = null)
        {
            IHtmlContent html = new HtmlString("");

            var key = $"{theme}:{zone}";
            var widgets = defaultWidgets.ToList();
            //var field = _db.CustomFields.GetValue(CustomType.Application, 0, key);

            //if (string.IsNullOrEmpty(field))
            //{
            //    await _db.CustomFields.SetCustomField(CustomType.Application, 0, key, widgets.ToString());
            //}
            //else
            //{
            //    widgets = field.Split(',').ToList();
            //}

            if (widgets.Any())
            {
                foreach (var widget in widgets)
                {
                    await AddWidget(helper, widget, zone);
                }
            }

            return await AddWidget(helper, theme, "WidgetZone", new ZoneViewModel { Theme = theme, Zone = zone, Widgets = widgets });
        }

        public static object GetValue(dynamic settings, string prop)
        {
            try
            {
                return settings.GetType().GetProperty(prop).GetValue(settings, null);
            }
            catch
            {
                return null;
            }
        }

        List<string> Disabled()
        {
            var field = _db.CustomFields.GetValue(CustomType.Application, 0, Constants.DisabledPackages);
            return field == null || string.IsNullOrEmpty(field) ? null : field.Split(',').ToList();
        }

        private bool Exists(string name)
        {
            return _selector.SelectComponent(name) != null;
        }

        private static IHtmlContent GetContent(string name)
        {
            return new HtmlString($"<div class=\"widget-not-found\">Widget {name} not found</div>");
        }
    }
}