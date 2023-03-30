using Reinforced.Typings.Attributes;

namespace KanbanDemo.Dtos
{
    [TsInterface(AutoI = true)]
    public class LoginDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
