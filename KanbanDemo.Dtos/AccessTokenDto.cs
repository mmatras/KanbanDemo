using Reinforced.Typings.Attributes;

namespace KanbanDemo.Dtos
{
    [TsInterface(AutoI = true)]
    public record AccessTokenDto(string Value, DateTime Expires);
}
