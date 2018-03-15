#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System;
using System.Linq;
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions;

namespace NSpectator.Specs.Running
{
    [TestFixture]
    [Category("RunningSpecs")]
    public class Describe_method_level_afters : When_running_specs
    {
        class SpecClass : Spec
        {
            public static Action ContextLevelAfter = () => { };
            public static Action SubContextAfter = () => { };
            public static Func<Task> AsyncSubContextAfter = async () => { await Task.Delay(0); };

            // method- (or class-) level after
            void after_each()
            {
            }

            void method_level_context()
            {
                After = ContextLevelAfter;

                Context["sub context"] = () => 
                {
                    After = SubContextAfter;

                    It["needs an example or it gets filtered"] = Todo;
                };

                Context["sub context with async after"] = () =>
                {
                    AfterAsync = AsyncSubContextAfter;

                    It["needs another example or it gets filtered"] = Todo;
                };
            }
        }

        [SetUp]
        public void setup()
        {
            Run(typeof(SpecClass));
        }

        [Test]
        public void it_should_set_method_level_after()
        {
            // Could not find a way to actually verify that deep inside 
            // 'AfterInstance' there is a reference to 'SpecClass.after_each()'

            classContext.AfterInstance.Should().NotBeNull();
        }

        [Test]
        [Category("Async")]
        public void it_should_not_set_async_method_level_after()
        {
            classContext.AfterInstanceAsync.Should().BeNull();
        }
    }
}
