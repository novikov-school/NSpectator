using System;
using System.Collections.Generic;
using System.Diagnostics;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace NSpectator
{
    [DebuggerNonUserCode]
    public static class Expectations
    {
        /// <summary>
        /// Returns an <see cref="T:NSpectator.ObjectExpectations"/> object that can be used to assert the
        /// expectations of the current <see cref="T:System.Object"/>.
        /// 
        /// </summary>
        public static ObjectExpectations Expected(this object actualValue)
        {
            return new ObjectExpectations(actualValue);
        }

        public static AndConstraint<ObjectAssertions> Expect<T>(this Exception obj) where T : Exception
        {
            return new ObjectAssertions(obj).BeOfType<T>();
        }

        public static StringExpectations Expected(this string actualValue)
        {
            return new StringExpectations(actualValue);
        }

        public static BooleanExpectations Expected(this bool actualValue)
        {
            return new BooleanExpectations(actualValue);
        }

        public static DateTimeExpectations Expected(this DateTime actualValue)
        {
            return new DateTimeExpectations(actualValue);
        }

        public static SimpleTimeSpanExpectations Expected(this TimeSpan actualValue)
        {
            return new SimpleTimeSpanExpectations(actualValue);
        }

        /// <summary>
        /// Returns an <see cref="T:NSpectator.GenericCollectionExpectations`1"/> object that can be used to assert the
        /// current <see cref="T:System.Collections.Generic.IEnumerable`1"/>.
        /// 
        /// </summary>
        public static GenericCollectionExpectations<T> Expected<T>(this IEnumerable<T> actualValue)
        {
            return new GenericCollectionExpectations<T>(actualValue);
        }
    }
}