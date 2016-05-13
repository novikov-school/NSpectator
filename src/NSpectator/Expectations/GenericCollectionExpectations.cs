using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Collections;
using FluentAssertions.Execution;

// ReSharper disable CheckNamespace

namespace NSpectator
{
    [DebuggerNonUserCode]
    public class GenericCollectionExpectations<T>
    {
        public GenericCollectionAssertions<T> To { get; }

        public IEnumerable<T> Subject => To.Subject;

        public GenericCollectionExpectations(IEnumerable<T> actualValue)
        {
            To = new GenericCollectionAssertions<T>(actualValue);
        }

        public AndConstraint<GenericCollectionAssertions<T>> ToBeEmpty()
        {
            return To.BeEmpty();
        }
    }
}