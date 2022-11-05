using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DemoKanban.Models
{
    [ModelMetadataType(typeof(IssueMetadata))]
    public partial class Issue
    {
        public class IssueMetadata
        {
            [Display(Name = "Id")]
            public int Id { get; set; }

            [Required]
            [MaxLength(255)]
            [Display(Name = "Tytuł")]
            public string Title { get; set; } = "";

            [Required]
            [Display(Name = "Notatka")]
            public string Notes { get; set; } = "";

            [Display(Name = "Status")]
            public IssueState State { get; set; }

            [Display(Name = "Pilne?")]
            public bool IsUrgent { get; set; }

            [Display(Name = "Ostateczny termin")]
            public DateTime? Deadline { get; set; }

            //public int? AssignedToId { get; set; }
            
            [Display(Name = "Osoba")]
            public Person AssignedTo { get; set; } = Person.Empty;

        }
    }
}
