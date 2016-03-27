using Gallio.Model.Helpers;
using NSpectator.GallioPlugin.Model;

namespace NSpectator.GallioPlugin.Services
{
    class NSpectatorTestDriver : SimpleTestDriver 
    {
        protected override string FrameworkName => "NSpectator";

        protected override TestExplorer CreateTestExplorer()
        {
            return new NSpectatorTestExplorer();
        }

        protected override TestController CreateTestController()
        {
            return new DelegatingTestController( test => new NSpectatorController() );
        }
    }
}