using System;
using System.Threading.Tasks;

namespace Blogifier.Admin.Helper;

public class UploadHelper
{
  private readonly Action<Task>? _onShow;
  public UploadHelper(Action<Task>? onShow = null)
  {
    _onShow = onShow;
  }
}
