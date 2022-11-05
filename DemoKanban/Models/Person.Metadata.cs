using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DemoKanban.Models
{
    [ModelMetadataType(typeof(PersonMetadata))]
    public partial class Person
    {
        public class PersonMetadata
        {
            [Required]
            [MaxLength(100)]
            public string Name { get; set; } = "<";

            [Required]
            [MaxLength(100)]
            public string Surname { get; set; } = "<empty>";
            public string DisplayName { get; set; } = "";
        }
    }
}
