using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DemoKanban.Models
{
    [ModelMetadataType(typeof(IssueMetadata))]
    public partial class Issue : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errorList = new List<ValidationResult>();

            if (KanbanContext.Data.Issues.Any(m => m.Title == Title))
            {
                errorList.Add(new ValidationResult("Takie zadanie już istnieje", new[] { "Title" }));
            }

            return errorList;
        }

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
