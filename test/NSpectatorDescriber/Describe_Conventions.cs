using NSpectator;
using NSpectator.Domain;
using NUnit.Framework;

namespace NSpectator.Describer
{
    [TestFixture]
    [Category("Conventions")]
    public class when_find_before
    {
        private Conventions conventions;

        public class class_with_before : Spec
        {
            void before_each()
            {

            }
        }

        [SetUp]
        public void Setup()
        {
            conventions = new DefaultConventions();
        }
    }

    [TestFixture]
    [Category("Conventions")]
    public class specifying_new_before_convension
    {
        public class ClassWithBefore : Spec
        {
            void BeforeEach()
            {

            }
        }

        [SetUp]
        public void Setup()
        {

        }
    }
}
