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
    /// <summary>
    /// FluentAssertions extensions
    /// </summary>
    public static class AssertionExtensions
    {
        /// <summary>
        /// Asserts that the predicate in expression is true
        /// </summary>
        /// <param name="o"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static AndConstraint<BooleanAssertions> Should<T>(this T o, Expression<Predicate<T>> predicate)
        {
            return predicate.Compile()(o).Should().BeTrue(ExampleBase.Parse(predicate.Body));
        }

        /// <summary>
        /// Asserts that the object is assignable to a variable of type <typeparamref name="T"/>.
        /// </summary>
        public static T CastTo<T>(this ObjectAssertions assertions)
        {
            return assertions.BeAssignableTo<T>().Subject;
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
        /// <param name="because"></param>
        /// <param name="reasonArgs"></param>
        public static AndConstraint<NumericAssertions<float>> BeCloseTo(this NumericAssertions<float> assertion, float expected, float tolerance, string because = "", params object[] reasonArgs)
        {
            Execute.Assertion.ForCondition(Math.Abs((float)assertion.Subject - expected) <= tolerance)
                .BecauseOf(because, reasonArgs).FailWith("Expected a value which is close to {0} with tolerance {1}{reason}, but found {2}.", (object)expected, (object)tolerance, (object)assertion.Subject);
            return new AndConstraint<NumericAssertions<float>>(assertion);
        }

        /// <summary>
        /// Asserts that a float value is close to expected with tolerance
        /// </summary>
        /// <param name="assertion"></param>
        /// <param name="expected"></param>
        /// <param name="because"></param>
        /// <param name="reasonArgs"></param>
        /// <returns></returns>
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
        public static AndConstraint<NumericAssertions<double>> BeCloseTo(this NumericAssertions<double> assertion, double expected, double tolerance, string because = "")
        {
            Execute.Assertion.ForCondition(Math.Abs((double)assertion.Subject - expected) <= tolerance)
                .BecauseOf(because).FailWith("Expected a value which is close to {0} with tolerance {1}{reason}, but found {2}.", (object)expected, (object)tolerance, (object)assertion.Subject);
            return new AndConstraint<NumericAssertions<double>>(assertion);
        }

        /// <summary>
        /// Asserts that a double value is close to expected with tolerance
        /// </summary>
        /// <param name="assertion"></param>
        /// <param name="expected"></param>
        /// <param name="because"></param>
        /// <returns></returns>
        public static AndConstraint<NumericAssertions<double>> BeCloseTo(this NumericAssertions<double> assertion, double expected, string because = "")
        {
            return BeCloseTo(assertion, expected, double.Epsilon, because);
        }
    }
}