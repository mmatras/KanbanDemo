using System.ComponentModel.DataAnnotations;

namespace DemoKanban.Models
{
    public partial class Issue
    {
        public int Id { get; set; } = 0;

        public string Title { get; set; } = ""; 
        
        public string Notes { get; set; } = "";
        public IssueState State { get; set; }
        public bool IsUrgent { get; set; }
        public DateTime? Deadline { get; set; }

        public int? AssignedToId { get; set; } 
        public Person AssignedTo { get; set; }

        //public Issue(string title)
        //{
        //    Title = title;
        //}
    }
}
