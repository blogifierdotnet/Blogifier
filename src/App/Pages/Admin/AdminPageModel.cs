using Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace App.Pages.Admin
{
    public class AdminPageModel : PageModel
    {
        public bool IsAdmin { get; set; }
        public bool HasNewVersion { get; set; }

        public IEnumerable<Notification> Notifications { get; set; }

        [TempData]
        public string Message { get; set; }
        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        [TempData]
        public string Error { get; set; }
        public bool ShowError => !string.IsNullOrEmpty(Error);

        public string RenderMessage()
        {
            var msg = ShowMessage ?
                $"<script>toastr.success('{Message}')</script>" :
                (ShowError ? $"<script>toastr.error('{Error}')</script>" : "");
            Clear();
            return msg;
        }

        public void Clear()
        {
            Message = "";
            Error = "";
        }
    }
}