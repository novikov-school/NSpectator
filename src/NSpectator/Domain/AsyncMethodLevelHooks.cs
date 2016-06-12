using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NSpectator.Domain
{
    /// <summary>
    /// AsyncMethodLevelHook
    /// </summary>
    public abstract class AsyncMethodLevelHook
    {
        /// <summary>
        /// Initiailize new AsyncMethodLevelHook
        /// </summary>
        /// <param name="method"></param>
        /// <param name="hookName"></param>
        protected AsyncMethodLevelHook(MethodInfo method, string hookName)
        {
            runner = new AsyncMethodRunner(method, hookName);
        }

        /// <summary>
        /// Run 
        /// </summary>
        /// <param name="spec"></param>
        public virtual void Run(Spec spec)
        {
            runner.Run(spec);
        }

        private readonly AsyncMethodRunner runner;
    }

    internal class AsyncMethodLevelBefore : AsyncMethodLevelHook
    {
        public AsyncMethodLevelBefore(MethodInfo method) : base(method, "before_each") {}
    }

    internal class AsyncMethodLevelAct : AsyncMethodLevelHook
    {
        public AsyncMethodLevelAct(MethodInfo method) : base(method, "act_each") {}
    }

    internal class AsyncMethodLevelAfter : AsyncMethodLevelHook
    {
        public AsyncMethodLevelAfter(MethodInfo method) : base(method, "after_each") {}
    }

    internal class AsyncMethodLevelBeforeAll : AsyncMethodLevelHook
    {
        public AsyncMethodLevelBeforeAll(MethodInfo method) : base(method, "before_all") {}
    }

    internal class AsyncMethodLevelAfterAll : AsyncMethodLevelHook
    {
        public AsyncMethodLevelAfterAll(MethodInfo method) : base(method, "after_all") {}
    }
}