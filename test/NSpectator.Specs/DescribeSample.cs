using NUnit.Framework;
using SampleSpecs.Demo;

namespace NSpectator.Specs
{
    [TestFixture]
    public class DescribeSample : DebuggerShim
    {
        [Test]
        public void Spectate() => Debug(typeof(Describe_Extensions));
    }
}