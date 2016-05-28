using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NSpectator.Domain.Extensions;

namespace NSpectator.Domain
{
    public abstract class MethodExampleBase : ExampleBase
    {
        protected MethodExampleBase(MethodInfo method, string tags)
            : base(method.Name.Replace("_", " "), tags)
        {
            this.method = method;
        }

        protected MethodInfo method;

        /// <summary>
        /// Determine if the method is async
        /// </summary>
        public override bool IsAsync => method.IsAsync();
    }
}