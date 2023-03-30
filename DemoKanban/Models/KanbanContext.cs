using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoKanban.Models
{
    public class KanbanContext : IdentityDbContext
    {
        public KanbanContext(DbContextOptions<KanbanContext> options)
            : base(options)
        {

        }

        //public DbSet<Issue> Issues { get; set; }
        public DbSet<Issue> Issues => Set<Issue>();
        public DbSet<Person> People => Set<Person>(); 
        public DbSet<AuditLog> AuditLog=> Set<AuditLog>();
        public DbSet<File> Files => Set<File>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            //modelBuilder.Entity<Person>().HasIndex();

            modelBuilder.Entity<Person>().HasData(
                new Person() { Id = 1, Name = "Daniel", Surname = "Matras", DisplayName = "matras" },
                new Person() { Id = 2, Name = "Marcin", Surname = "Nowak" },
                new Person() { Id = 3, Name = "Jan", Surname = "Opolski", DisplayName = "opolski" },
                new Person() { Id = 4, Name = "Magdalena", Surname = "Dąbrowska", DisplayName = "jdąb" }
            );

            modelBuilder.Entity<Issue>().HasData(
                new Issue
                {
                    Id = 1,
                    Title = "Nauczyć się C# oraz .NET",
                    IsUrgent = true,
                    Deadline = DateTime.Now.AddMonths(3),
                    State = IssueState.Todo,
                    Notes = "Ten temat musi być bardzo dobrze opanowany",
                    AssignedToId = 1,
                },
                new Issue
                {
                    Id = 2,
                    Title = "Nauczyć się ASP.NET MVC",
                    IsUrgent = false,
                    Deadline = DateTime.Now.AddDays(3),
                    State = IssueState.Doing,
                    Notes = "Rwównież Razor Pages",
                    AssignedToId = 3
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
            );
        }
    }
}
