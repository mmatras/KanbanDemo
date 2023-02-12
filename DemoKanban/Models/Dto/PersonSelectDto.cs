using Reinforced.Typings.Attributes;

namespace DemoKanban.Models.Dto
{
    [TsInterface(AutoI = true)]
    public record PersonSelectDto(int Id, string DisplayName);
}
