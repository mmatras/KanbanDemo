using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoKanban.Models
{
    public partial class Issue
    {
        public int Id { get; set; } = 0;

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = ""; 
        
        public string Notes { get; set; } = "";
        public IssueState State { get; set; }
        public bool IsUrgent { get; set; }
        public DateTime? Deadline { get; set; }

        //[ForeignKey("AssignedTo")]
        public int? AssignedToId { get; set; } 
        public virtual Person? AssignedTo { get; set; }

        //public Issue(string title)
        //{
        //    Title = title;
        //}
    }
}
