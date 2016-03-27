using System.Collections.Generic;
using Gallio.Model;
using Gallio.Runtime.Extensibility;
using Gallio.Runtime.Logging;
using NSpectator.GallioAdapter.Services;

namespace NSpectator.GallioAdapter
{
    /// <summary>
    /// Builds a test object model based on reflection against NUnit framework attributes.
    /// </summary>
    public class NSpecTestFramework : BaseTestFramework
    {
        /// <inheritdoc />
        sealed public override TestDriverFactory GetTestDriverFactory()
        {
            return CreateTestDriver;
        }

        private static ITestDriver CreateTestDriver(
            IList<ComponentHandle<ITestFramework, TestFrameworkTraits>> testFrameworkHandles,
            TestFrameworkOptions testFrameworkOptions,
            ILogger logger)
        {
            return new NSpecTestDriver();
        }
    }
}
