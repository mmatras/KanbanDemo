namespace DemoKanban.Models
{
    public partial class Person
    {
        public static Person Empty = new Person();

        public int Id { get; set; }
        public string Name { get; set; } = "<empty>";
        public string Surname { get; set; } = "<empty>";
        public string DisplayName { get; set; } = "";
        //public string Pesel { get; set; } = "";
        public DateTime DateOfBirth { get; set; }


        //public List<Issue> Tasks { get; set; }
    }
}
