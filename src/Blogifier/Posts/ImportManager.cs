using Blogifier.Extensions;
using Blogifier.Identity;
using Blogifier.Shared;
using Blogifier.Storages;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Posts;

public class ImportManager
{
  private readonly UserProvider _userProvider;
  private readonly ReverseProvider _reverseProvider;
  private readonly PostProvider _postProvider;
  private readonly StorageManager _storageManager;

  public ImportManager(
    UserProvider userProvider,
    ReverseProvider reverseProvider,
    PostProvider postProvider,
    StorageManager storageManager)
  {
    _userProvider = userProvider;
    _reverseProvider = reverseProvider;
    _postProvider = postProvider;
    _storageManager = storageManager;
  }

  public async Task<IEnumerable<PostEditorDto>> WriteAsync(ImportDto request, int userId)
  {
    var user = await _userProvider.FirstByIdAsync(userId);
    var titles = request.Posts.Select(m => m.Title);
    var matchPosts = await _postProvider.MatchTitleAsync(titles);

    var posts = new List<PostEditorDto>();

    foreach (var post in request.Posts)
    {
      var postDb = matchPosts.FirstOrDefault(m => m.Title.Equals(post.Title, StringComparison.OrdinalIgnoreCase));
      if (postDb != null)
      {
        posts.Add(postDb);
        continue;
      }

      var publishedAt = post.PublishedAt!.Value.ToUniversalTime();
      var baseAddress = new Uri(post.Slug!);
      if (!string.IsNullOrEmpty(post.Cover))
        await _storageManager.UploadAsync(publishedAt, user.Id, baseAddress, post.Cover);

      var uploadeContent = await _storageManager.UploadsFoHtmlAsync(publishedAt, user.Id, baseAddress, post.Content);

      var markdownContent = _reverseProvider.ToMarkdown(uploadeContent);
      post.Content = markdownContent;

      var markdownDescription = _reverseProvider.ToMarkdown(post.Description);
      post.Description = markdownDescription;

      post.State = PostState.Release;
      post.PublishedAt = publishedAt;
      posts.Add(post);
    }

    return await _postProvider.AddAsync(posts, userId);
  }
}
