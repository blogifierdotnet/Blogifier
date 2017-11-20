using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Common
{
    public interface IComponentHelper
    {
        Task<IHtmlContent> InvokeAsync(IViewComponentHelper helper, string name, object arguments = null);
    }

    public class ComponentHelper : IComponentHelper
    {
        private readonly IViewComponentSelector _selector;
        IUnitOfWork _db;

        public ComponentHelper(IViewComponentSelector selector, IUnitOfWork db)
        {
            _selector = selector;
            _db = db;
        }

        public async Task<IHtmlContent> InvokeAsync(IViewComponentHelper helper, string name, object arguments = null)
        {
            if (Disabled().Contains(name))
            {
                return await Task.FromResult(new HtmlString(""));
            }
            return Exists(name)
                ? await helper.InvokeAsync(name, arguments)
                : await Task.FromResult(GetContent(name));
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
            var field = _db.CustomFields.Single(f => f.CustomType == CustomType.Application && f.CustomKey == "DISABLED-PACKAGES");
            return field == null || string.IsNullOrEmpty(field.CustomValue) ? null : field.CustomValue.Split(',').ToList();
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