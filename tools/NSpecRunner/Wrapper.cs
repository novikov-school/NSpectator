using System;
using NSpectator.Domain;

namespace NSpecRunner
{
    public class Wrapper : MarshalByRefObject
    {
        public int Execute(RunnerInvocation invocation, Func<RunnerInvocation, int> action)
        {
            return action(invocation);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}