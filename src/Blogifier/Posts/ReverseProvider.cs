using ReverseMarkdown;

namespace Blogifier.Posts;

public class ReverseProvider
{
  private readonly Converter _converter;

  public ReverseProvider()
  {
    var config = new Config
    {
      // generate GitHub flavoured markdown, supported for BR, PRE and table tags
      GithubFlavored = true,
      // will ignore all comments
      RemoveComments = true,
      // Include the unknown tag completely in the result (default as well)
      UnknownTags = Config.UnknownTagsOption.Bypass,
      // remove markdown output for links where appropriate
      SmartHrefHandling = false,
      PassThroughTags = new string[] { "figure" }
    };
    _converter = new Converter(config);
  }

  public string ToMarkdown(string html)
  {
    var markdown = _converter.Convert(html);
    //_logger.LogDebug("ToMarkdown  html:{html}, markdown:{markdown}", html, markdown);
    return markdown;
  }
}
