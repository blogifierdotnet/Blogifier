using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blogifier.Shared;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;

namespace Blogifier.Admin.Pages.Account;

public partial class Login
{
    private readonly LoginModel _model = new()
    {
        Email = "",
        Password = "",
        CaptchaResponse = ""
    };


    private bool _showError;

    private async Task LoginUser()
    {
        var returnUrl = "admin/";
        var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);

        if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("returnUrl", out var param))
            returnUrl = param.First();

        if (returnUrl.StartsWith("http"))
            returnUrl = "admin/";

        var result = await _http.PostAsJsonAsync("api/author/login", _model);

        if (result.IsSuccessStatusCode)
        {
            _showError = false;
            _navigationManager.NavigateTo(returnUrl, true);
        }
        else
        {
            _showError = true;
            StateHasChanged();
        }
    }


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
            await _jsRuntime.InvokeAsync<int>("googleReCaptcha",
                DotNetObjectReference.Create(this),
                "recaptcha",
                "api/captchakey?valueonly=true");
    }

    [JSInvokable]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallbackOnSuccess(string response)
    {
        _model.CaptchaResponse = response;
    }


    [JSInvokable]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallbackOnExpired(string response)
    {
        _model.CaptchaResponse = response;
    }
}
