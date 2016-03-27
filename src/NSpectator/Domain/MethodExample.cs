using System;
using System.Reflection;

namespace NSpectator.Domain
{
    public class MethodExample : MethodExampleBase
    {
        public MethodExample(MethodInfo method, string tags) 
            : base(method, tags)
        {
        }

        public override void Run(nspec nspec)
        {
            method.Invoke(nspec, null);
        }
    }
}
