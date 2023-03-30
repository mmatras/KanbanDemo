using KanbanDemo.Dtos;

namespace KanbanBlazorClient.Auth
{
    public interface ITokenService
    {
        Task<AccessTokenServiceDto?> GetToken();
        Task RemoveToken();
        Task SetToken(string token, DateTime expires);
    }
}
