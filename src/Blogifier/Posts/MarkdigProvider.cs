using Markdig;
using Microsoft.Extensions.Logging;
using ReverseMarkdown;

namespace Blogifier.Posts;

public class MarkdigProvider
{
  private readonly ILogger _logger;
  private readonly MarkdownPipeline _markdownPipeline;
  private readonly Converter _converter;

  public MarkdigProvider(ILogger<MarkdigProvider> logger)
  {
    _logger = logger;
    _markdownPipeline = new MarkdownPipelineBuilder()
        .UsePipeTables()
        .UseAdvancedExtensions()
        .Build();
    _converter = new ReverseMarkdown.Converter();
  }

  public string ToHtml(string markdown)
  {
    var html = Markdown.ToHtml(markdown, _markdownPipeline);
    _logger.LogDebug("ToHtml markdown:{markdown}, html:{html}", markdown, html);
    return html;
  }

  public string ToMarkdown(string html)
  {
    var markdown = _converter.Convert(html);
    _logger.LogDebug("ToMarkdown  html:{html}, markdown:{markdown}", html, markdown);
    return markdown;
  }
}
