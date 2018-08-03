using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace App.Helpers
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
}