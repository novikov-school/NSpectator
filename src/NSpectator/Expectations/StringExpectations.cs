using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace NSpectator
{
    /// <summary>
    /// Contains a number of methods to assert that a <see cref="T:System.String"/> is in the expected state.
    /// 
    /// </summary>
    [DebuggerNonUserCode]
    public class StringExpectations : ReferenceTypeExpectation<string, StringExpectations>
    {
        /// <summary>
        /// Returns the type of the subject the assertion applies on.
        /// 
        /// </summary>
        protected override string Context => "string";

        public StringAssertions To { get; }

        public StringExpectations(string value)
        {
            this.Subject = value;
            To = new StringAssertions(this.Subject);
        }
        
        /// <summary>
        /// Asserts that a string is exactly the same as another string, including the casing and any leading or trailing whitespace.
        /// 
        /// </summary>
        /// <param name="expected">The expected string.</param>
        public AndConstraint<StringAssertions> ToBe(string expected)
        {
            return To.Be(expected);
        }

        /// <summary>
        /// Asserts that a string is not exactly the same as the specified <paramref name="unexpected"/>,
        ///             including any leading or trailing whitespace, with the exception of the casing.
        /// 
        /// </summary>
        /// <param name="unexpected">The string that the subject is not expected to be equivalent to.</param><param name="because">A formatted phrase as is supported by <see cref="M:System.String.Format(System.String,System.Object[])"/> explaining why the assertion
        ///             is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        public AndConstraint<StringAssertions> NotToBe(string unexpected)
        {
            return To.NotBe(unexpected);
        }
        
        /// <summary>
        /// Asserts that a string is <see cref="F:System.String.Empty"/>.
        /// 
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="M:System.String.Format(System.String,System.Object[])"/> explaining why the assertion
        ///             is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        public AndConstraint<StringAssertions> ToBeEmpty(string because)
        {
            return To.BeEmpty(because);
        }

        public AndConstraint<StringAssertions> ToBeEmpty()
        {
            return To.BeEmpty();
        }

        /// <summary>
        /// Asserts that a string is not <see cref="F:System.String.Empty"/>.
        /// 
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="M:System.String.Format(System.String,System.Object[])"/> explaining why the assertion
        ///             is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        public AndConstraint<StringAssertions> NotToBeEmpty(string because)
        {
            return To.NotBeEmpty(because);
        }

        public AndConstraint<StringAssertions> NotToBeEmpty()
        {
            return To.NotBeEmpty();
        }

        /// <summary>
        /// Asserts that a string is neither <c>null</c> nor <see cref="F:System.String.Empty"/>.
        /// 
        /// </summary>
        /// <param name="because">A formatted phrase explaining why the assertion should be satisfied. If the phrase does not
        ///             start with the word <i>because</i>, it is prepended to the message.
        /// </param>
        public AndConstraint<StringAssertions> NotBeNullOrEmpty(string because)
        {
            return To.NotBeNullOrEmpty(because);
        }

        public AndConstraint<StringAssertions> NotBeNullOrEmpty()
        {
            return To.NotBeNullOrEmpty();
        }
    }
    
}