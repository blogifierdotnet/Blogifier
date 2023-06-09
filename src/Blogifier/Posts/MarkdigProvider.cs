using Markdig;

namespace Blogifier.Posts;

public class MarkdigProvider
{
  private readonly MarkdownPipeline _markdownPipeline;


  public MarkdigProvider()
  {
    _markdownPipeline = new MarkdownPipelineBuilder()
        .UsePipeTables()
        .UseAdvancedExtensions()
        .Build();
  }

  public string ToHtml(string markdown)
  {
    var html = Markdown.ToHtml(markdown, _markdownPipeline);
    //_logger.LogDebug("ToHtml markdown:{markdown}, html:{html}", markdown, html);
    return html;
  }
}
