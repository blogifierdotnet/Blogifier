using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
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
        Task<IHtmlContent> InvokeAsync(IViewComponentHelper helper, string name, object arguments = null);
        Task<IHtmlContent> WidgetZone(IViewComponentHelper helper, string theme, string zone);
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

        public async Task<IHtmlContent> InvokeAsync(IViewComponentHelper helper, string name, object arguments = null)
        {
            if (Disabled() != null && Disabled().Contains(name))
            {
                return await Task.FromResult(new HtmlString(""));
            }
            try
            {
                return Exists(name)
                ? await helper.InvokeAsync(name, arguments)
                : await Task.FromResult(new HtmlString(""));
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error loading widget: {ex.Message}");
                return await Task.FromResult(new HtmlString(""));
            }
        }

        public async Task<IHtmlContent> WidgetZone(IViewComponentHelper helper, string theme, string zone)
        {
            var widgets = new List<string>();

            // get list of widgets for theme/zone
            // ust to test - will come from DB
            if(zone == "Sidebar")
            {
                widgets.Add("Hello");
            }
            else
            {
                widgets.Add("Simple");
            }
            return await InvokeAsync(helper, "WidgetZone", new ZoneViewModel { Theme = theme, Zone = zone, Widgets = widgets });
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