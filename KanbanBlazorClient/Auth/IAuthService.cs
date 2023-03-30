using KanbanBlazorClient.Dtos;

namespace KanbanBlazorClient.Auth
{
    public interface IAuthService
    {
        Task<LoginComponentResultDto> Login(LoginComponentDto login);
    }
}
