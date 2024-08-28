namespace IdeoPrivateRoom.Email.Models;

public class MailAttachment
{
    public MailAttachment(string fileName, Stream stream)
    {
        FileName = fileName;
        Stream = stream;
    }
    public MailAttachment(string path)
    {
        Path = path;
    }
    public MailAttachment(string fileName, byte[] bytes)
    {
        FileName = fileName;
        Bytes = bytes;
    }

    public MailAttachment(string filename, string base64Content, string? type = null, string? disposition = null, string? content_id = null)
    {
        FileName = filename;
        Content = base64Content;
        Type = type;
        Disposition = disposition;
        ContentId = content_id;
    }

    /// <summary>
    /// Gets or sets the filename of the attachment.
    /// </summary>
    public string FileName { get; set; } = default!;

    public Stream Stream { get; set; } = default!;

    public string Path { get; set; } = default!;

    /// <summary>
    /// Gets or sets the Base64 encoded content of the attachment.
    /// </summary>
    public string Content { get; set; } = default!;

    /// <summary>
    /// Gets or sets the mime type of the content you are attaching.
    /// For example, application/pdf or image/jpeg.
    /// </summary>
    public string? Type { get; set; } = default!;

    /// <summary>
    /// Gets or sets the content-disposition of the attachment specifying how you would
    /// like the attachment to be displayed. For example, "inline" results in the attached
    /// file being displayed automatically within the message while "attachment" results
    /// in the attached file requiring some action to be taken before it is displayed
    /// (e.g. opening or downloading the file). Defaults to "attachment". Can be either
    /// "attachment" or "inline".
    /// </summary>
    public string? Disposition { get; set; } = default!;

    /// <summary>
    /// Gets or sets a unique id that you specify for the attachment. This is used when
    /// the disposition is set to "inline" and the attachment is an image, allowing the
    /// file to be displayed within the body of your email
    /// </summary>
    public string? ContentId { get; set; } = default!;

    public byte[] Bytes { get; set; } = default!;
}
