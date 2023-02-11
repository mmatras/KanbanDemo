using DemoKanban.Models;
using Reinforced.Typings.Ast.TypeNames;
using Reinforced.Typings.Fluent;

namespace DemoKanban.Configs
{
    public class ReinforcementTypingsConfig
    {
        public static void Configure(Reinforced.Typings.Fluent.ConfigurationBuilder builder)
        {
            builder.Global(g => g.UseModules());
            builder.Global(g => g.CamelCaseForProperties());

            builder.Substitute(typeof(DateTime), new RtSimpleTypeName("string"));

            builder.ExportAsEnum<IssueState>();
        }
    }
}
