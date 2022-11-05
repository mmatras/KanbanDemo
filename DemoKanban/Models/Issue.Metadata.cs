using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DemoKanban.Models
{
    [ModelMetadataType(typeof(IssueMetadata))]
    public partial class Issue
    {
        public class IssueMetadata
        {
            [Required]
            [MaxLength(255)]
            public string Title { get; set; } = "";

            [Required]
            public string Notes { get; set; } = "";
        }
    }
}
