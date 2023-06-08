using Blogifier.Shared;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blogifier.Admin.Pages.Account
{
	public partial class Login
	{
		public bool showError = false;
		public LoginModel model = new LoginModel { Email = "", Password = "" };

      public async Task LoginUser()
		{
         var returnUrl = "admin/";
         var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);

         if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("returnUrl", out var param))
            returnUrl = param.First();
		
			if(!IsLocalUrl(returnUrl))
				returnUrl = "admin/";

         var result = await Http.PostAsJsonAsync<LoginModel>("api/author/login", model);

			if (result.IsSuccessStatusCode)
			{
				showError = false;
				_navigationManager.NavigateTo(returnUrl, true);
			}
			else
			{
				showError = true;
				StateHasChanged();
			}
		}

		static bool IsLocalUrl(string url)
		{
			if(url.Contains("//"))
				return false;

			Uri result;
			return Uri.TryCreate(url, UriKind.Relative, out result);
		}
	}
}
