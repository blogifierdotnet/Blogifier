using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Shared;

public class MailSetting
{
  [Key]
  public int Id { get; set; }
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public DateTime CreatedAt { get; set; }
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
  public bool Enabled { get; set; }
  public Blog? Blog { get; set; }
}
