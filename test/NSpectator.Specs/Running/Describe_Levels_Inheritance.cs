using NSpectator;
using NSpectator.Specs.Running;
using NUnit.Framework;

namespace NSpectator.Specs.Running
{
    public class describe_Levels_Inheritance : When_running_specs
    {
        class parent_context : Spec { }

        class child_context : parent_context
        {
            void it_is()
            {
                "is".Expected().ToBe("is");
            }
        }

        [SetUp]
        public void Setup()
        {
            Run(new[] { typeof(parent_context), typeof(child_context) });
        }

        [Test]
        public void parent_class_is_level_1()
        {
            TheContext("parent context").Level.Expected().ToBe(1);
        }

        [Test]
        public void child_class_is_level_2()
        {
            TheContext("child context").Level.Expected().ToBe(2);
        }
    }
}