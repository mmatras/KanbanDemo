namespace DemoKanban.Models
{
    public partial class Person
    {
        public static Person Empty = new Person();

        public int Id { get; set; }
        public string Name { get; set; } = "";

        //public List<Issue> Tasks { get; set; }
    }
}
