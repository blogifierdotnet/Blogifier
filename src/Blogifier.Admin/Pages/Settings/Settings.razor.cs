using Blogifier.Shared;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blogifier.Admin.Pages.Settings
{
	public partial class Settings
	{
		protected Author model { get; set; }

		protected override async Task OnInitializedAsync()
		{
			var result = await _http.GetFromJsonAsync<Author>("api/author/getcurrent");
			if (result != null)
			{
				var author = await _http.GetFromJsonAsync<Author>($"api/author/email/{result.Email}");
				model = author;
			}
		}
	}
}
