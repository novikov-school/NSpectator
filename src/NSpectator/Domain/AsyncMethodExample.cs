using System;
using System.Reflection;
using System.Threading.Tasks;

namespace NSpectator.Domain
{
    public class AsyncMethodExample : MethodExampleBase
    {
        public AsyncMethodExample(MethodInfo method, string tags)
            : base(method, tags)
        {
            runner = new AsyncMethodRunner(method, "example");
        }

        public override void Run(Spec spec)
        {
            runner.Run(spec);
        }

        readonly AsyncMethodRunner runner;
    }

    public class AsyncMethodRunner
    {
        public AsyncMethodRunner(MethodInfo method, string hookName)
        {
            this.method = method;
            this.hookName = hookName;
        }

        public void Run(Spec spec)
        {
            if (method.ReturnType == typeof(void))
            {
                throw new ArgumentException("'async void' method-level {0} is not supported, please use 'async Task' instead", hookName);
            }

            if (method.ReturnType.IsGenericType)
            {
                throw new ArgumentException("'async Task<T>' method-level {0} is not supported, please use 'async Task' instead", hookName);
            }

            Func<Task> asyncWork = () => (Task)method.Invoke(spec, null);

            asyncWork.Offload();
        }

        readonly MethodInfo method;
        readonly string hookName;
    }
}
