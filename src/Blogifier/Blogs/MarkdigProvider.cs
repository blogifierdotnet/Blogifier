using Markdig;
using Microsoft.Extensions.Logging;

namespace Blogifier.Blogs;

public class MarkdigProvider
{
  protected readonly ILogger _logger;
  protected readonly MarkdownPipeline _markdownPipeline;
  public MarkdigProvider(ILogger<MarkdigProvider> logger)
  {
    _logger = logger;
    _markdownPipeline = new MarkdownPipelineBuilder()
        .UsePipeTables()
        .UseAdvancedExtensions()
        .Build();
  }

  public string ToHtml(string markdown)
    => Markdown.ToHtml(markdown, _markdownPipeline);
}
