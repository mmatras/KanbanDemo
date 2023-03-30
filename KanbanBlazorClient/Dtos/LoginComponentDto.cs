namespace KanbanBlazorClient.Dtos
{
    public class LoginComponentDto {

        public LoginComponentDto(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; set; }
        public string Password { get; set; }
    }
    public record LoginComponentResultDto(LoginStatus Status, string ErrorMessage, string AccessToken, DateTime Expires);
    public enum LoginStatus 
    {
        Success,
        Error
    }
}
