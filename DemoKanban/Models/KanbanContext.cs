namespace DemoKanban.Models
{
    public class KanbanContext
    {
        public List<Issue> Issues { get; set; } = new List<Issue>();
        public List<Person> People { get; set; } = new List<Person>();
        public List<AuditLog> AuditLog { get; set; } = new List<AuditLog>();

        public static KanbanContext Data = new KanbanContext
        {
            People = 
            {
                new Person() { Id = 1, Name = "Daniel", Surname = "Matras", DisplayName = "matras"},
                new Person() { Id = 2, Name = "Marcin", Surname = "Nowak" },
                new Person() { Id = 3, Name = "Jan", Surname = "Opolski", DisplayName = "opolski" },
                new Person() { Id = 4, Name = "Magdalena", Surname = "Dąbrowska", DisplayName = "jdąb" }
            },
            Issues = {
                new Issue
                {
                    Id = 1,
                    Title = "Nauczyć się C# oraz .NET",
                    IsUrgent = true,
                    Deadline = DateTime.Now.AddMonths(3),
                    State = IssueState.Todo,
                    Notes = "Ten temat musi być bardzo dobrze opanowany"
                },
                new Issue
                {
                    Id = 2,
                    Title = "Nauczyć się ASP.NET MVC",
                    IsUrgent = false,
                    Deadline = DateTime.Now.AddDays(3),
                    State = IssueState.Doing,
                    Notes = "Rwównież Razor Pages"
                },
                new Issue
                {
                    Id = 3,
                    Title = "Zrobić samodzielny proejkt",
                    Deadline = DateTime.Now.AddDays(50),
                    State = IssueState.Todo,
                    Notes = "Mamy dwa projekty do wyboru lub można wybrać swój"
                },
                new Issue
                {
                    Id = 4,
                    Title = "Nauczyć się RDBMS",
                    State = IssueState.Done,
                    Notes = "Język SQL jest językiem deklaratywnym"
                }
            }
        };

        static KanbanContext()
        {
            Data.Issues[0].AssignedTo = Data.People[0];
            Data.Issues[1].AssignedTo = Data.People[2];
        }
    }
}
