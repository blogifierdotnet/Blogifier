using Markdig;

namespace Blogifier.Posts;

public class MarkdigProvider
{
  protected readonly MarkdownPipeline _markdownPipeline;
  public MarkdigProvider()
  {
    _markdownPipeline = new MarkdownPipelineBuilder()
        .UsePipeTables()
        .UseAdvancedExtensions()
        .Build();
  }

  public string ToHtml(string markdown)
    => Markdown.ToHtml(markdown, _markdownPipeline);
}
