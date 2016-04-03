using System;
using System.Diagnostics;
using FluentAssertions;
using FluentAssertions.Common;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
// ReSharper disable CheckNamespace

namespace NSpectator
{
    /// <summary>
    /// Contains a number of methods to assert that an <see cref="T:System.Object"/> is in the expected state.
    /// 
    /// </summary>
    [DebuggerNonUserCode]
    public class ObjectExpectations : ReferenceTypeAssertions<object, ObjectExpectations>
    {
        /// <summary>
        /// Returns the type of the subject the assertion applies on.
        /// 
        /// </summary>
        protected override string Context => "object";

        public ObjectExpectations(object value)
        {
            this.Subject = value;
        }

        public ObjectAssertions To => new ObjectAssertions(this.Subject);


        public AndConstraint<ObjectAssertions> Any()
        {
            return new AndConstraint<ObjectAssertions>(To);
        } 

        /// <summary>
        /// Asserts that the current object has not been initialized yet.
        /// 
        /// </summary>
        public AndConstraint<ObjectAssertions> ToBeNull()
        {
            return To.BeNull();
        }

        /// <summary>
        /// Asserts that the current object has not been initialized yet.
        /// 
        /// </summary>
        public AndConstraint<ObjectAssertions> NotNull()
        {
            return To.NotBeNull();
        }

        /// <summary>
        /// Asserts that an object equals another object using its <see cref="M:System.Object.Equals(System.Object)"/> implementation.
        /// 
        /// </summary>
        /// <param name="expected">The expected value</param><param name="because">A formatted phrase as is supported by <see cref="M:System.String.Format(System.String,System.Object[])"/> explaining why the assertion
        ///             is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        public AndConstraint<ObjectExpectations> ToBe(object expected, string because)
        {
            Execute.Assertion.BecauseOf(because).ForCondition(this.Subject.IsSameOrEqualTo(expected)).FailWith("Expected {context:object} to be {0}{reason}, but found {1}.", expected, this.Subject);
            return new AndConstraint<ObjectExpectations>(this);
        }

        public AndConstraint<ObjectExpectations> ToBe(object expected)
        {
            return ToBe(expected, string.Empty);
        }

        /// <summary>
        /// Asserts that an object does not equal another object using its <see cref="M:System.Object.Equals(System.Object)"/> method.
        /// 
        /// </summary>
        /// <param name="unexpected">The unexpected value</param><param name="because">A formatted phrase explaining why the assertion should be satisfied. If the phrase does not
        ///             start with the word <i>because</i>, it is prepended to the message.
        /// </param>
        public AndConstraint<ObjectExpectations> NotToBe(object unexpected, string because)
        {
            Execute.Assertion.ForCondition(!this.Subject.IsSameOrEqualTo(unexpected)).BecauseOf(because).FailWith("Did not expect {context:object} to be equal to {0}{reason}.", unexpected);
            return new AndConstraint<ObjectExpectations>(this);
        }

        public AndConstraint<ObjectExpectations> NotToBe(object unexpected)
        {
            return NotToBe(unexpected, string.Empty);
        }

        /// <summary>
        /// Asserts that an object is an enum and has a specified flag
        /// 
        /// </summary>
        /// <param name="expectedFlag">The expected flag.</param><param name="because">A formatted phrase explaining why the assertion should be satisfied. If the phrase does not
        ///             start with the word <i>because</i>, it is prepended to the message.
        /// </param>
        public AndConstraint<ObjectExpectations> ToHaveFlag(Enum expectedFlag, string because)
        {
            Execute.Assertion.BecauseOf(because).ForCondition(this.Subject != null).FailWith("Expected type to be {0}{reason}, but found <null>.", (object)expectedFlag.GetType()).Then.ForCondition(this.Subject.GetType() == expectedFlag.GetType()).FailWith("Expected the enum to be of type {0} type but found {1}{reason}.", (object)expectedFlag.GetType(), (object)this.Subject.GetType()).Then.Given<Enum>((Func<Enum>)(() => this.Subject as Enum)).ForCondition((Func<Enum, bool>)(@enum => @enum.HasFlag(expectedFlag))).FailWith("The enum was expected to have flag {0} but found {1}{reason}.", (Func<Enum, object>)(_ => (object)expectedFlag), (Func<Enum, object>)(@enum => (object)@enum));
            return new AndConstraint<ObjectExpectations>(this);
        }

        /// <summary>
        /// Asserts that an object is an enum and does not have a specified flag
        /// 
        /// </summary>
        /// <param name="unexpectedFlag">The unexpected flag.</param><param name="because">A formatted phrase explaining why the assertion should be satisfied. If the phrase does not
        ///             start with the word <i>because</i>, it is prepended to the message.
        /// </param>
        public AndConstraint<ObjectExpectations> NotToHaveFlag(Enum unexpectedFlag, string because)
        {
            Execute.Assertion.BecauseOf(because).ForCondition(this.Subject != null).FailWith("Expected type to be {0}{reason}, but found <null>.", (object)unexpectedFlag.GetType()).Then.ForCondition(this.Subject.GetType() == unexpectedFlag.GetType()).FailWith("Expected the enum to be of type {0} type but found {1}{reason}.", (object)unexpectedFlag.GetType(), (object)this.Subject.GetType()).Then.Given<Enum>((Func<Enum>)(() => this.Subject as Enum)).ForCondition((Func<Enum, bool>)(@enum => !@enum.HasFlag(unexpectedFlag))).FailWith("Did not expect the enum to have flag {0}{reason}.", new object[1]
            {
                (object) unexpectedFlag
            });
            return new AndConstraint<ObjectExpectations>(this);
        }
    }
}