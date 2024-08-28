namespace IdeoPrivateRoom.Email.Models.Options;

public class SendGridOptions
{
    public string ApiKey { get; set; } = default!;
    public bool SandboxMode { get; set; }
    public string SenderEmail { get; set; } = default!;
}
