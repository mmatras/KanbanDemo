using DemoKanban.Models;
using DemoKanban.Models.Dto;

namespace KanbanBlazorClient.Services
{
    public record IssueViewDto(int Id, string Title, IssueState State, string Notes, string AssignedToDisplayName, bool IsUrgent);

    public interface IKanbanDemoService
    {
        Task<IEnumerable<IssueViewDto>> GetIssues(string? query);
        Task<EditIssueDto> GetIssue(int id);
        Task<bool> UpdateIssue(EditIssueDto issue);
    }
}
