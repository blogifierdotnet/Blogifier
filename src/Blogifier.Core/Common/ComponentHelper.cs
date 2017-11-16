using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
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

        public ComponentHelper(IViewComponentSelector selector)
        {
            _selector = selector;
        }

        public async Task<IHtmlContent> InvokeAsync(IViewComponentHelper helper, string name, object arguments = null)
        {
            return Exists(name)
                ? await helper.InvokeAsync(name, arguments)
                : await Task.FromResult(GetContent(name));
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