using DemoKanban.Models.Dto;
using KanbanBlazorClient.Auth;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace KanbanBlazorClient.Services
{
    public class KanbanDemoService : IKanbanDemoService
    {
        private readonly ITokenService tokenService;
        private readonly HttpClient httpClient;

        public KanbanDemoService(ITokenService tokenService, HttpClient httpClient)
        {
            this.tokenService = tokenService;
            this.httpClient = httpClient;
        }

        async Task AddToken()
        {
            var token = await tokenService.GetToken();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.Token);
        }

        public Task<EditIssueDto> GetIssue(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IssueViewDto>> GetIssues(string? query = "")
        {
            var uri = string.IsNullOrWhiteSpace(query) ? "/api/issue" : $"/api/issue?query={query}";

            await AddToken();
            var issues = await httpClient.GetFromJsonAsync<IEnumerable<IssueDto>>(uri);

            return issues.Select(issue => new IssueViewDto(issue.Id, issue.Title, issue.State, issue.Notes, 
                issue.AssignedPersonDisplayName, issue.IsUrgent));
        }

        public Task<bool> UpdateIssue(EditIssueDto issue)
        {
            throw new NotImplementedException();
        }
    }
}
