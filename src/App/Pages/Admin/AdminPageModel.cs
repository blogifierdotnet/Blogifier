using Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages.Admin
{
    public class AdminPageModel : PageModel
    {
        //[BindProperty]
        //public Author Author { get; set; }

        public bool IsAdmin { get; set; }

        [TempData]
        public string Message { get; set; }
        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        [TempData]
        public string Error { get; set; }
        public bool ShowError => !string.IsNullOrEmpty(Error);

        public void Clear()
        {
            Message = "";
            Error = "";
        }
    }
}