using NSpectator;
using NSpectator.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSpectator.Specs.Running;

namespace NSpectator.Specs.Running
{
    public abstract class When_describing_async_hooks : When_running_specs
    {
        protected class BaseSpecClass : Spec
        {
            public static int state = -2;
            public static int expected = 1;

            public BaseSpecClass()
            {
                state = 0;
            }

            protected async Task SetStateAsync() 
            {
                state = -1;

                await Task.Delay(50);

                await Task.Run(() => state = 1);
            }

            protected void SetAnotherState()
            {
                state = 2;
            }

            protected async Task FailAsync()
            {
                await Task.Run(() =>
                {
                    throw new InvalidCastException("Some error message");
                });
            }

            protected void ShouldHaveInitialState()
            {
                state.Should().Be(0);
            }

            protected void ShouldHaveFinalState()
            {
                state.Should().Be(1);
            }

            protected void PassAlways()
            {
                true.should_be_true();
            }
        }

        protected void ExampleRunsWithExpectedState(string name)
        {
            ExampleBase example = TheExample(name);

            example.HasRun.should_be_true();

            example.Exception.Should().BeNull();

            BaseSpecClass.state.Should().Be(BaseSpecClass.expected);
        }

        protected void ExampleRunsWithException(string name)
        {
            ExampleBase example = TheExample(name);

            example.HasRun.should_be_true();

            example.Exception.Should().NotBeNull();
        }
    }
}
