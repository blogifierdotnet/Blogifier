namespace Blogifier.Newsletters;

public class MailSettingData
{
  public bool Enabled { get; set; }
  public string Host { get; set; } = default!;
  public int Port { get; set; }
  public string UserEmail { get; set; } = default!;
  public string UserPassword { get; set; } = default!;
  public string FromName { get; set; } = default!;
  public string FromEmail { get; set; } = default!;
  public string ToName { get; set; } = default!;
}
