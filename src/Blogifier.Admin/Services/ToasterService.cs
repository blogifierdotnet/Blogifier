using Blogifier.Shared.Resources;
using Microsoft.Extensions.Localization;
using Sotsera.Blazor.Toaster;
using System.Net.Http;

namespace Blogifier.Admin.Services;

public class ToasterService(IToaster toaster, IStringLocalizer<Resource> localizer)
{
  private readonly IToaster _toaster = toaster;
  private readonly IStringLocalizer<Resource> _localizer = localizer;

  public bool CheckResponse(HttpResponseMessage response)
  {
    if (response.IsSuccessStatusCode)
    {
      _toaster.Success(_localizer["completed"]);
      return true;
    }
    else
    {
      _toaster.Error(_localizer["generic-error"]);
      return false;
    }
  }
}
