using Blogifier.Shared;
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
			var result = await Http.PostAsJsonAsync<LoginModel>("api/author/login", model);

			if (result.IsSuccessStatusCode)
			{
				showError = false;
				_navigationManager.NavigateTo("admin", true);
			}
			else
			{
				showError = true;
				StateHasChanged();
			}
		}
	}
}
