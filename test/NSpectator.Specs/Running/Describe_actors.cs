#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion

using System;
using NUnit.Framework;
using FluentAssertions;

namespace NSpectator.Specs.Running
{
    [TestFixture]
    [Category("RunningSpecs")]
    public class Describe_actors : Describe_todo
    {
        public class ActorRegister
        {
            public ActorRegister(Action<string, string, Action> actionSetter)
            {
                this.actionSetter = actionSetter;
            }

            Action<string, string, Action> actionSetter;

            public Action<string> this[string key]
            {
                set { actionSetter(key, null, () => value("_")); }
            }

            public Action<string> this[string key, string tags]
            {
                set { actionSetter(key, tags, () => value("_")); }
            }

            public class Actor
            {
                private ActorRegister _register;

                public Actor(ActorRegister register)
                {
                    _register = register;
                }

                public static bool operator >(Actor lhs, Action<string> rhs)
                {
                    lhs._register[">"] = rhs;
                    return true;
                }

                public static bool operator <(Actor lhs, Action<string> rhs)
                {
                    lhs._register[">"] = rhs;
                    return false;
                }
            }

            public Actor like => new Actor(this);
        }

        class ActsSpec : Spec
        {
            private ActorRegister acts;

            public ActsSpec()
            {
                acts = new ActorRegister((s, s1, arg3) => {});    
            }

            void method_level_actor()
            {
                acts["call method"] = _=>
                {
                    
                };
            }
        }

        [Test]
        public void example_should_be_pending()
        {
            ExampleFrom(typeof(ActsSpec)).Pending.Should().BeTrue();
        }
    }
}