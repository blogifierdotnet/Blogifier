using Blogifier.Extensions;
using Blogifier.Identity;
using Blogifier.Shared;
using Blogifier.Storages;
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
  private readonly StorageProvider _storageProvider;

  public ImportManager(
    UserProvider userProvider,
    ReverseProvider reverseProvider,
    PostProvider postProvider,
    StorageProvider storageProvider)
  {
    _userProvider = userProvider;
    _reverseProvider = reverseProvider;
    _postProvider = postProvider;
    _storageProvider = storageProvider;
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
      if (post.Cover != null && !post.Cover.Equals(BlogifierSharedConstant.DefaultCover, StringComparison.OrdinalIgnoreCase))
      {
        await _storageProvider.UploadFromWeb(user.Id, post.Slug!, post.Cover, publishedAt);
      }

      var importImagesContent = await _storageProvider.UploadImagesFoHtml(webRoot, user.Id, post.Slug!, publishedAt, post.Content);
      var importFilesContent = await _storageProvider.UploadFilesFoHtml(webRoot, user.Id, post.Slug!, publishedAt, post.Content);

      var markdownContent = _reverseProvider.ToMarkdown(importFilesContent);
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
