namespace IdeoPrivateRoom.Email.Models.Options;

public class CorsOptions
{
    public static string Section { get; set; } = "CorsOptions";
    public string AllowedOrigins { get; set; }
    public string PolicyName { get; set; }
}