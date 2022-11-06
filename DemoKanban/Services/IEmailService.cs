namespace DemoKanban.Services
{
    public enum EmailStatus { Success, Fail }

    public interface IEmailService
    {
        EmailStatus Send(string to, string message);
    }
}
