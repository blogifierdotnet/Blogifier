using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Blogifier.Core.Common
{
    public interface IComponentHelper
    {
        Task<IHtmlContent> AddWidget(IViewComponentHelper helper, string widget, object arguments = null);
        Task<IHtmlContent> AddZoneWidget(IViewComponentHelper helper, string zone, string widget, object arguments = null);
        Task<IHtmlContent> AddZone(IViewComponentHelper helper, string zone, string[] defaultWidgets = null);
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

        public async Task<IHtmlContent> AddWidget(IViewComponentHelper helper, string widget, object arguments = null)
        {
            return await AddZoneWidget(helper, "", widget, arguments);
        }

        public async Task<IHtmlContent> AddZoneWidget(IViewComponentHelper helper, string zone, string widget, object arguments = null)
        {
            if (await Disabled() != null && (await Disabled()).Contains(widget))
                return await Task.FromResult(new HtmlString(""));

            if (widget != "WidgetZone")
            {
                var key = zone == "" ? $"w:{BlogSettings.Theme}-{widget}" : $"z:{BlogSettings.Theme}-{zone}-{widget}";

                var setting = await _db.CustomFields.GetValue(CustomType.Application, 0, key);

                if (string.IsNullOrEmpty(setting))
                {
                    var arg = ObjectToString(arguments);
                    await _db.CustomFields.SetCustomField(CustomType.Application, 0, key, "");
                }
                else
                {
                    // arguments = StringToObject(setting);
                }
            }

            try
            {
                return Exists(widget)
                ? await helper.InvokeAsync(widget, arguments)
                : await Task.FromResult(new HtmlString(""));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading widget: {ex.Message}");
                return await Task.FromResult(new HtmlString(""));
            }
        }

        public async Task<IHtmlContent> AddZone(IViewComponentHelper helper, string zone, string[] defaultWidgets = null)
        {
            var html = new HtmlString("");
            var widgets = defaultWidgets.ToList();

            //var key = $"{BlogSettings.Theme}:{zone}";
            //var field = _db.CustomFields.GetValue(CustomType.Application, 0, key);

            //if (string.IsNullOrEmpty(field))
            //{
            //    await _db.CustomFields.SetCustomField(CustomType.Application, 0, key, widgets.ToString());
            //}
            //else
            //{
            //    widgets = field.Split(',').ToList();
            //}

            //if (widgets.Any())
            //{
            //    foreach (var widget in widgets)
            //    {
            //        await AddWidget(helper, widget, null);
            //    }
            //}

            return await AddWidget(helper, "WidgetZone", new ZoneViewModel { Theme = BlogSettings.Theme, Zone = zone, Widgets = widgets });
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

        async Task<List<string>> Disabled()
        {
            var field = await _db.CustomFields.GetValue(CustomType.Application, 0, Constants.DisabledPackages);
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


        public string ObjectToString(object obj)
        {
            //return JsonConvert.SerializeObject(obj, new ExpandoObjectConverter());
            return JsonConvert.SerializeObject(obj);
        }

        public object StringToObject(string json)
        {
            //return JsonConvert.DeserializeObject<ExpandoObject>(json, new ExpandoObjectConverter());
            return JsonConvert.DeserializeObject(json);
        }
    }
}