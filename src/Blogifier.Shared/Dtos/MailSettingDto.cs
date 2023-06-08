using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared;

public class MailSettingDto
{
  public bool Enabled { get; set; }
  [StringLength(160)]
  public string Host { get; set; } = default!;
  public int Port { get; set; }
  [EmailAddress]
  [StringLength(120)]
  public string UserEmail { get; set; } = default!;
  [StringLength(120)]
  public string UserPassword { get; set; } = default!;
  [StringLength(120)]
  public string FromName { get; set; } = default!;
  [EmailAddress]
  [StringLength(120)]
  public string FromEmail { get; set; } = default!;
  [StringLength(120)]
  public string ToName { get; set; } = default!;
}
