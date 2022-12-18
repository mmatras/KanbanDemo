using Microsoft.AspNetCore.Authorization;

namespace DemoKanban.Policy
{
    public class IsOverRequirement : IAuthorizationRequirement
    {
        public int Age{ get; }

        public IsOverRequirement(int age)
        {
            Age = age;
        }
    }
}
