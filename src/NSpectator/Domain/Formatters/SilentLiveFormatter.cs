using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSpectator.Domain.Formatters
{
    public class SilentLiveFormatter : ILiveFormatter, IFormatter
    {
        public void Write(Context context) {}

        public void Write(ExampleBase example, int level) {}

        public void Write(ContextCollection contexts) {}
    }
}