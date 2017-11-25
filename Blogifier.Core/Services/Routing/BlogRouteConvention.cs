using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq;
using System.Reflection;

// code from https://www.strathweb.com/2016/06/global-route-prefix-with-asp-net-core-mvc-revisited/

namespace Blogifier.Core.Services.Routing
{
    public static class MvcOptionsExtensions
    {
        public static void UseBlogRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
        {
            opts.Conventions.Insert(0, new BlogRouteConvention(routeAttribute));
        }
    }

    public class BlogRouteConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _centralPrefix;
        private readonly TypeInfo _blogControllerTypeInfo;

        public BlogRouteConvention(IRouteTemplateProvider routeTemplateProvider)
        {
            _centralPrefix = new AttributeRouteModel(routeTemplateProvider);
            _blogControllerTypeInfo = typeof(Controllers.BlogController).GetTypeInfo();
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                if (controller.ControllerType == _blogControllerTypeInfo)
                {
                    var matchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList();
                    if (matchedSelectors.Any())
                    {
                        foreach (var selectorModel in matchedSelectors)
                        {
                            selectorModel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_centralPrefix,
                                selectorModel.AttributeRouteModel);
                        }
                    }

                    var unmatchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel == null).ToList();
                    if (unmatchedSelectors.Any())
                    {
                        foreach (var selectorModel in unmatchedSelectors)
                        {
                            selectorModel.AttributeRouteModel = _centralPrefix;
                        }
                    }
                }
            }
        }
    }
}
