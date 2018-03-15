﻿#region [R# naming]
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
    public class Describe_method_level_befores : When_running_specs
    {
        class SpecClass : Spec
        {
            public static Action ContextLevelBefore = () => { };
            public static Action SubContextBefore = () => { };
            public static Func<Task> AsyncSubContextBefore = async () => { await Task.Delay(0); };

            // method- (or class-) level before
            void before_each() 
            { 
            }

            void method_level_context()
            {
                Before = ContextLevelBefore;

                Context["sub context"] = () => 
                {
                    Before = SubContextBefore;

                    It["needs an example or it gets filtered"] = Todo;
                };

                Context["sub context with async before"] = () =>
                {
                    BeforeAsync = AsyncSubContextBefore;

                    It["needs another example or it gets filtered"] = Todo;
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            Run(typeof(SpecClass));
        }

        [Test]
        public void it_should_set_method_level_before()
        {
            // Could not find a way to actually verify that deep inside 
            // 'BeforeInstance' there is a reference to 'SpecClass.before_each()'

            classContext.BeforeInstance.Should().NotBeNull();
        }

        [Test]
        [Category("Async")]
        public void it_should_not_set_async_method_level_before()
        {
            classContext.BeforeInstanceAsync.Should().BeNull();
        }

    }
}
