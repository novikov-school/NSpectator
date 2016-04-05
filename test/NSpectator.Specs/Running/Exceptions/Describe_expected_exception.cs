#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System.Linq;
using NSpectator.Domain;
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions;

namespace NSpectator.Specs.Running.Exceptions
{
    [TestFixture]
    [Category("RunningSpecs")]
    public class Describe_expected_exception : When_expecting_exception
    {
        private class SpecClass : Spec
        {
            void method_level_context()
            {
                before = () => { };

                it["throws expected exception"] = expect<KnownException>(() => { throw new KnownException(); });

                it["throws expected exception with expected error message"] = expect<KnownException>("Testing", () => { throw new KnownException("Testing"); });

                it["fails if expected exception does not throw"] = expect<KnownException>(() => { });

                it["fails if wrong exception thrown"] = expect<KnownException>(() => { throw new SomeOtherException(); });

                it["fails if wrong error message is returned"] = expect<KnownException>("Testing", () => { throw new KnownException("Blah"); });
            }
        }

        [SetUp]
        public void Setup()
        {
            Run(typeof(SpecClass));
        }
    }

    [TestFixture]
    [Category("RunningSpecs")]
    [Category("Async")]
    public class Describe_async_expected_exception_before_awaiting_a_task : When_expecting_exception
    {
        private class SpecClass : Spec
        {
            void method_level_context()
            {
                before = () => { };

                itAsync["throws expected exception"] = expectAsync<KnownException>(async () =>
                    await Task.Run(() =>
                    {
                        throw new KnownException();
                    }));

                itAsync["throws expected exception with expected error message"] = expectAsync<KnownException>("Testing", async () =>
                    await Task.Run(() => { throw new KnownException("Testing"); }));

                itAsync["fails if expected exception does not throw"] = expectAsync<KnownException>(async () =>
                    await Task.Run(() => { }));

                itAsync["fails if wrong exception thrown"] = expectAsync<KnownException>(async () =>
                    await Task.Run(() => { throw new SomeOtherException(); }));

                itAsync["fails if wrong error message is returned"] = expectAsync<KnownException>("Testing", async () =>
                    await Task.Run(() => { throw new KnownException("Blah"); }));
            }
        }

        [SetUp]
        public void Setup()
        {
            Run(typeof(SpecClass));
        }
    }

    [TestFixture]
    [Category("RunningSpecs")]
    [Category("Async")]
    public class Describe_async_expected_exception_after_awaiting_a_task : When_expecting_exception
    {
        private class SpecClass : Spec
        {
            void method_level_context()
            {
                before = () => { };

                itAsync["throws expected exception"] = expectAsync<KnownException>(async () =>
                {
                    await Task.Run(() => { });

                    throw new KnownException();
                });

                itAsync["throws expected exception with expected error message"] = expectAsync<KnownException>("Testing", async () =>
                {
                    await Task.Run(() => { } );

                    throw new KnownException("Testing");
                });

                itAsync["fails if expected exception does not throw"] = expectAsync<KnownException>(async () =>
                    await Task.Run(() => { }));

                itAsync["fails if wrong exception thrown"] = expectAsync<KnownException>(async () =>
                {
                    await Task.Run(() => { } );

                    throw new SomeOtherException();
                });

                itAsync["fails if wrong error message is returned"] = expectAsync<KnownException>("Testing", async () =>
                {
                    await Task.Run(() => {  } );

                    throw new KnownException("Blah");
                });
            }
        }

        [SetUp]
        public void Setup()
        {
            Run(typeof(SpecClass));
        }
    }

    public abstract class When_expecting_exception : When_running_specs
    {
        [Test]
        public void Should_be_three_failures()
        {
            classContext.Failures().Should().HaveCount(3);
        }

        [Test]
        public void throws_expected_exception()
        {
            TheExample("throws expected exception").Should().HavePassed();
        }

        [Test]
        public void throws_expected_exception_with_error_message_Testing()
        {
            TheExample("throws expected exception with expected error message").Should().HavePassed();
        }

        [Test]
        public void fails_if_expected_exception_not_thrown()
        {
            TheExample("fails if expected exception does not throw").Exception.Should().BeOfType<ExceptionNotThrown>();
        }

        [Test]
        public void fails_if_wrong_exception_thrown()
        {
            var exception = TheExample("fails if wrong exception thrown").Exception;

            exception.Should().BeOfType<ExceptionNotThrown>();
            exception.Message.Should().Be("Exception of type KnownException was not thrown.");
        }

        [Test]
        public void fails_if_wrong_error_message_is_returned()
        {
            var exception = TheExample("fails if wrong error message is returned").Exception;

            exception.Should().BeOfType<ExceptionNotThrown>();
            exception.Message.Should().Be("Expected message: \"Testing\" But was: \"Blah\"");
        }
    }
}