using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NSpectator.Domain;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Numeric;
using FluentAssertions.Primitives;

namespace NSpectator
{
    public static class AssertionExtensions
    {
        public static void Should<T>(this T o, Expression<Predicate<T>> predicate)
        {
            predicate.Compile()(o).Should().BeTrue(ExampleBase.Parse(predicate.Body));
        }
        
        public static void is_true(this bool actual) { actual.should_be_true(); }

        public static void should_be_true(this bool actual)
        {
            actual.Should().BeTrue();
        }

        public static void should_be_false(this bool actual)
        {
            actual.Should().BeFalse();
        }

        public static void should_be(this object actual, object expected)
        {
            actual.Is(expected);
        }

        public static void Is(this object actual, object expected)
        {
            actual.Should().Be(expected);
        }

        public static void should_be(this string actual, string expected)
        {
            actual.Should().Be(expected);
        }

        public static void should_start_with(this string actual, string start)
        {
            actual.Should().StartWith(start);
        }

        public static IEnumerable<T> should_not_contain<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            collection.Should().NotContain(predicate);
            return collection;
        }

        public static IEnumerable<T> should_contain<T>(this IEnumerable<T> collection, Expression<Func<T, bool>> predicate)
        {
            collection.Should().Contain(predicate);
            return collection;
        }

        public static IEnumerable<T> should_contain<T>(this IEnumerable<T> collection, T t)
        {
            collection.Should().Contain(t);
            return collection;
        }
        

        public static IEnumerable<T> should_be_empty<T>(this IEnumerable<T> collection)
        {
            collection.Should().BeEmpty();
            return collection;
        }
        
        public static T ShouldCastTo<T>(this object value)
        {
            value.Should().BeOfType<T>();
            return (T)value;
        }

        /// <summary>
        /// Asserts that the integral number value is not default value.
        /// 
        /// </summary>
        /// <param name="assertions"></param>
        /// <param name="because"></param>
        /// <param name="reasonArgs"></param>
        public static AndConstraint<NumericAssertions<T>> NotBeDefault<T>(this NumericAssertions<T> assertions, string because = "", params object[] reasonArgs) where T : struct 
        {
            Execute.Assertion.ForCondition(!assertions.Subject.Equals(default(T))).BecauseOf(because, reasonArgs).FailWith("Did not expect default value {0}{reason}.", (object)default(T));
            return new AndConstraint<NumericAssertions<T>>(assertions);
        }

        /// <summary>
        /// Asserts that a string is exactly the same as another string, including the casing and any leading or trailing whitespace.
        /// 
        /// </summary>
        /// <param name="assertions"></param>
        /// <param name="expected">The expected string.</param>
        /// <param name="stringComparison"></param>
        /// <param name="because">A formatted phrase as is supported by <see cref="M:System.String.Format(System.String,System.Object[])"/> explaining why the assertion
        ///             is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        ///             </param><param name="reasonArgs">Zero or more objects to format using the placeholders in <see cref="!:because"/>.
        ///             </param>
        public static AndConstraint<StringAssertions> BeEquals(this StringAssertions assertions, string expected, StringComparison stringComparison, string because = "", params object[] reasonArgs)
        {
            Execute.Assertion.ForCondition(assertions.Subject.Equals(expected, stringComparison))
                .BecauseOf(because, reasonArgs).FailWith("Expected: {0} {reason}, but found {1}.", (object)expected, (object)assertions.Subject);
            return new AndConstraint<StringAssertions>(assertions);
        }

        /// <summary>
        /// Asserts that a float value is close to expected with tolerance
        /// </summary>
        /// <param name="assertion"></param>
        /// <param name="expected"></param>
        /// <param name="tolerance"></param>
        public static AndConstraint<NumericAssertions<float>> BeCloseTo(this NumericAssertions<float> assertion, float expected, float tolerance, string because = "", params object[] reasonArgs)
        {
            Execute.Assertion.ForCondition(Math.Abs((float)assertion.Subject - expected) <= tolerance)
                             .BecauseOf(because, reasonArgs).FailWith("Expected a value which is close to {0} with tolerance {1}{reason}, but found {2}.", (object)expected, (object)tolerance, (object)assertion.Subject);
            return new AndConstraint<NumericAssertions<float>>(assertion);
        }

        public static AndConstraint<NumericAssertions<float>> BeCloseTo(this NumericAssertions<float> assertion, float expected, string because = "", params object[] reasonArgs)
        {
            return BeCloseTo(assertion, expected, float.Epsilon, because, reasonArgs);
        }

        /// <summary>
        /// Asserts that a double value is close to expected with tolerance
        /// </summary>
        /// <param name="assertion"></param>
        /// <param name="expected"></param>
        /// <param name="tolerance"></param>
        public static AndConstraint<NumericAssertions<double>> BeCloseTo(this NumericAssertions<double> assertion, double expected, double tolerance, string because = "", params object[] reasonArgs)
        {
            Execute.Assertion.ForCondition(Math.Abs((double)assertion.Subject - expected) <= tolerance)
                             .BecauseOf(because, reasonArgs).FailWith("Expected a value which is close to {0} with tolerance {1}{reason}, but found {2}.", (object)expected, (object)tolerance, (object)assertion.Subject);
            return new AndConstraint<NumericAssertions<double>>(assertion);
        }

        public static AndConstraint<NumericAssertions<double>> BeCloseTo(this NumericAssertions<double> assertion, double expected, string because = "", params object[] reasonArgs)
        {
            return BeCloseTo(assertion, expected, double.Epsilon, because, reasonArgs);
        }

        public static void ShouldBeCloseTo(this double actual, double expected, double tolerance)
        {
            Math.Abs(actual - expected).Should().BeLessOrEqualTo(tolerance,
                $"should be close to {tolerance} of {expected} but was {actual} ");
        }

        public static void ShouldBeCloseTo(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance)
        {
            Math.Abs((actual - expected).Ticks).Should().BeLessOrEqualTo(tolerance.Ticks,
                $"should be close to {tolerance} of {expected} but was {actual} ");
        }

        public static void ShouldBeCloseTo(this DateTime actual, DateTime expected, DateTime tolerance)
        {
            Math.Abs((actual - expected).Ticks).Should().BeLessOrEqualTo(tolerance.Ticks,
                $"should be close to {tolerance} of {expected} but was {actual} ");
        }
    }
}