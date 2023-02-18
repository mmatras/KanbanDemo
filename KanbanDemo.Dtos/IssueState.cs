using Reinforced.Typings.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DemoKanban.Models
{
    public enum IssueState 
    {
        [Display(Name = "Do zrobienia")]
        Todo,

        [Display(Name = "W trakcie")]
        Doing,

        [Display(Name = "Zrobione")]
        Done 
    }
}
