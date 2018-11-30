using Microsoft.AspNetCore.Mvc;

namespace App.Pages.Admin.Shared
{
    public class ErrorModel : AdminPageModel
    {
        [BindProperty]
        public int Code { get; set; }

        public void OnGet(int code)
        {
            Code = code;
        }
    }
}