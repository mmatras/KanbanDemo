namespace DemoKanban.Services
{
    public class EmailService : IEmailService
    {
        public EmailStatus Send(string to, string message)
        {
            Console.WriteLine($"Sending e-mail to: {to}, body: {message}");
            return EmailStatus.Success;
        }
    }
}
