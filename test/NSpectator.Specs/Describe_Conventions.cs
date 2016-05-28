#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using NSpectator.Domain;
using NUnit.Framework;

namespace NSpectator.Specs
{
    [TestFixture]
    [Category("Conventions")]
    public class When_find_before
    {
        private Conventions conventions;

        public class class_with_before : Spec
        {
            void before_each()
            {

            }
        }

        [Test]
        public void Should_have_valid_ctor_execution()
        {
            conventions = new DefaultConventions();
        }
    }

    [TestFixture]
    [Category("Conventions")]
    public class Specifying_new_before_convension
    {
        public class ClassWithBefore : Spec
        {
            void beforeEach()
            {

            }
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Should_do_nothing_but_works()
        {
            
        }
    }
}
