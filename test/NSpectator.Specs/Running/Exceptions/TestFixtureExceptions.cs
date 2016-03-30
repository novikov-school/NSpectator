#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSpectator.Specs.Running.Exceptions
{
    class BeforeAllException : Exception
    {
        public BeforeAllException() : base("BeforeAllException") { }
    }

    class BeforeException : Exception
    {
        public BeforeException() : base("BeforeException") { }
    }

    class NestedBeforeException : Exception
    {
        public NestedBeforeException() : base("NestedBeforeException") { }
    }

    class ActException : Exception
    {
        public ActException() : base("ActException") { }
    }

    class NestedActException : Exception
    {
        public NestedActException() : base("NestedActException") { }
    }

    class ItException : Exception
    {
        public ItException() : base("ItException") { }
    }

    class AfterException : Exception
    {
        public AfterException() : base("AfterException") { }
    }

    class NestedAfterException : Exception
    {
        public NestedAfterException() : base("NestedAfterException") { }
    }

    class AfterAllException : Exception
    {
        public AfterAllException() : base("AfterAllException") { }
    }

    class KnownException : Exception
    {
        public KnownException() : base() { }
        public KnownException(string message) : base(message) { }
    }

    class SomeOtherException : Exception
    {
        public SomeOtherException() : base() { }
    }
}
