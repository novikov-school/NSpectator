using System;
using System.Reflection;

namespace NSpectator.Domain
{
    public class MethodContext : Context
    {
        public override void Build(Spec instance)
        {
            base.Build(instance);

            try
            {
                method.Invoke(instance, null);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception executing context: {0}".With(FullContext));

                throw e;
            }
        }

        public MethodContext(MethodInfo method, string tags = null)
            : base(method.Name, tags)
        {
            this.method = method;
        }

        private MethodInfo method;
    }
}