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
            if (Disabled() != null && Disabled().Contains(name))
            {
                return await Task.FromResult(new HtmlString(""));
            }
            try
            {
                return Exists(name)
                ? await helper.InvokeAsync(name, arguments)
                : await Task.FromResult(GetContent(name));
            }
            catch (System.Exception ex)
            {
                return await Task.FromResult(GetContent(ex.Message));
            }
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