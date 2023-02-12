using Reinforced.Typings.Attributes;

namespace DemoKanban.Models.Dto
{
    [TsInterface(AutoI = true)]
    public class EditIssueDto
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; } = "";
        public string Notes { get; set; } = "";
        public IssueState State { get; set; }
        public bool IsUrgent { get; set; }
        public DateTime? Deadline { get; set; }
        public int? AssignedToId { get; set; }
    }
}
