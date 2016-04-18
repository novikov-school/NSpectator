using Bowling.Specs;
using NUnit.Framework;

namespace Samples.Test
{
    [TestFixture]
    public class SamplesFixture : DebuggerShim
    {
        [Test]
        public void Spectate()
        {
            Debug(typeof(Score_calculation).Assembly);
        }
    }
}