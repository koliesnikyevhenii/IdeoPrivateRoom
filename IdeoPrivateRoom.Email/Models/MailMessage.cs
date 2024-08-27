namespace IdeoPrivateRoom.Email.Models;

public class MailMessage
{
    public string Subject { get; set; } = default!;
    public List<string> Recipients { get; set; } = new List<string>();
    public List<string>? CcRecipients { get; set; }
    public List<MailAttachment>? Attachments { get; set; }
    public string Body { set; get; } = default!;
}
