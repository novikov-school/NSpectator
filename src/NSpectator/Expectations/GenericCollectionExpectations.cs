using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Collections;
using FluentAssertions.Execution;

namespace NSpectator
{
    public class GenericCollectionExpectations<T>
    {
        public GenericCollectionAssertions<T> To { get; }

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