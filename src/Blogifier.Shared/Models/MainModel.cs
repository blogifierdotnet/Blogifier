namespace Blogifier.Shared;

public class MainModel(MainDto main)
{
  public MainDto Main { get; set; } = main;
}
