using Gallio.Model;
using Gallio.Model.Tree;
using NSpectator.Domain;
using Reflector = Gallio.Common.Reflection.Reflector;

namespace NSpectator.GallioPlugin.Model
{
    public class NSpecContextTest : Test
    {
        readonly Context _context;

        public Context Context
        {
            get { return _context; }
        }

        public NSpecContextTest( Context context )
            : base( context.Name, null )
        {
            this.Kind = TestKinds.Fixture;
            this._context = context;
        }
    }
}
