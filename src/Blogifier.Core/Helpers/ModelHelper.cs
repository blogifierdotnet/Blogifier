using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Blogifier.Core.Helpers
{
    public class ModelHelper
    {
        // pull validation error key to include it in error message
        // so we can return "key: value" and not just "value"
        public static string GetFirstValidationError(ModelStateDictionary modelState)
        {
            var listError = modelState.ToDictionary(
                m => m.Key, m => m.Value.Errors.Select(s => s.ErrorMessage)
                .FirstOrDefault(s => s != null));

            foreach (var item in listError)
            {
                if (!string.IsNullOrEmpty(item.Value))
                {
                    return $"{item.Key}: {item.Value}";
                }
            }
            return "";
        }
    }

    public class PostListFilter
    {
        HttpRequest _req;

        public PostListFilter(HttpRequest request)
        {
            _req = request;
        }

        public string Page
        {
            get
            {
                return string.IsNullOrEmpty(_req.Query["page"])
                    ? "" : _req.Query["page"].ToString();
            }
        }
        public string Status
        {
            get
            {
                return string.IsNullOrEmpty(_req.Query["status"])
                    ? "A" : _req.Query["status"].ToString();
            }
        }
        public string Search
        {
            get
            {
                return string.IsNullOrEmpty(_req.Query["search"])
                    ? "" : _req.Query["search"].ToString();
            }
        }
        public string Qstring
        {
            get
            {
                var q = "";
                if (!string.IsNullOrEmpty(Status)) q += $"&status={Status}";
                if (!string.IsNullOrEmpty(Search)) q += $"&search={Search}";
                return q;
            }
        }

        public string IsChecked(string status)
        {
            return status == Status ? "checked" : "";
        }
    }
}
