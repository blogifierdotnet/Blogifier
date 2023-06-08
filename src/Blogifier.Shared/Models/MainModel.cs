namespace Blogifier.Shared;

public class MainModel
{
  public MainDto Main { get; set; }

  public MainModel(MainDto main)
  {
    Main = main;
  }
}
