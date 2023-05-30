using Blogifier.Extensions;
using Blogifier.Identity;
using Blogifier.Shared;
using Blogifier.Storages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Posts;

public class ImportManager
{
  private readonly ILogger _logger;
  private readonly UserProvider _userProvider;
  private readonly MarkdigProvider _markdigProvider;
  private readonly PostProvider _postProvider;
  private readonly StorageProvider _storageProvider;

  public ImportManager(
    ILogger<ImportManager> logger,
    UserProvider userProvider,
    MarkdigProvider markdigProvider,
    PostProvider postProvider,
    StorageProvider storageProvider)
  {
    _logger = logger;
    _userProvider = userProvider;
    _markdigProvider = markdigProvider;
    _postProvider = postProvider;
    _storageProvider = storageProvider;
  }

  public async Task<IEnumerable<PostEditorDto>> WriteAsync(ImportDto request, string webRoot, string userId)
  {
    var user = await _userProvider.FindByIdAsync(userId);
    var titles = request.Posts.Select(m => m.Title);
    var matchPosts = await _postProvider.MatchTitleAsync(titles);

    var posts = new List<PostEditorDto>();

    foreach (var post in request.Posts)
    {
      var postDb = matchPosts.FirstOrDefault(m => m.Title.Equals(post.Title, StringComparison.Ordinal));
      if (postDb != null)
      {
        posts.Add(postDb);
        continue;
      }

      var publishedAt = post.PublishedAt!.Value;

      if (post.Cover != null && !post.Cover.Equals(BlogifierConstant.DefaultCover, StringComparison.Ordinal))
      {
        await _storageProvider.UploadFromWeb(webRoot, user.Id, post.Slug!, post.Cover, publishedAt);
      }

      var importImagesContent = await _storageProvider.UploadImagesFoHtml(webRoot, user.Id, post.Slug!, publishedAt, post.Content);
      var importFilesContent = await _storageProvider.UploadFilesFoHtml(webRoot, user.Id, post.Slug!, publishedAt, post.Content);

      var markdownContent = _markdigProvider.ToMarkdown(importFilesContent);
      post.Content = markdownContent;

      var markdownDescription = _markdigProvider.ToMarkdown(post.Description);
      post.Description = markdownDescription;

      post.State = PostState.Release;
      posts.Add(post);
    }

    return await _postProvider.AddAsync(posts, userId);
  }
}
