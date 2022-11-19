using DemoKanban.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DemoKanban.Models
{
    [ModelMetadataType(typeof(IssueMetadata))]
    public partial class Issue : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var ctx = validationContext.GetService<KanbanContext>();

            var errorList = new List<ValidationResult>();

            if (ctx.Issues.Any(m => m.Title == Title))
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
            [Display(Name = "Title", ResourceType = typeof(Resource))]
            public string Title { get; set; } = "";

            [Required]
            [Display(Name = "Notes", ResourceType = typeof(Resource))]
            public string Notes { get; set; } = "";

            [Display(Name = "Status")]
            public IssueState State { get; set; }

            [Display(Name = "IsUrgent", ResourceType = typeof(Resource))]
            public bool IsUrgent { get; set; }

            [Display(Name = "Ostateczny termin")]
            public DateTime? Deadline { get; set; }

            //public int? AssignedToId { get; set; }
            
            [Display(Name = "Osoba")]
            public Person AssignedTo { get; set; } = Person.Empty;

        }
    }
}
