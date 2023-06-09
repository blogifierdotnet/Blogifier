namespace Blogifier.Shared;

public static class UserHelper
{
  public static string CheckGetAvatarUrl(string? avatar)
  {
    if (!string.IsNullOrEmpty(avatar)) return avatar;
    return BlogifierSharedConstant.DefaultAvatar;
  }
}
