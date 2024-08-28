namespace IdeoPrivateRoom.Email.Services.Interfaces;

public interface IViewRenderer
{
    Task<string> RenderToStringAsync(string viewName, object model);
}
