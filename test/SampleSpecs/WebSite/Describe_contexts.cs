#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using FluentAssertions;
using NSpectator;
using SampleSpecs.Model;

namespace SampleSpecs.WebSite
{
    public class Describe_contexts : Spec
    {
        private Account account;

        //context methods require an underscore. For more info see DefaultConventions.cs.
        void describe_Account()
        {
            //contexts can be nested n-deep and contain befores and specifications
            Context["when withdrawing cash"] = () =>
            {
                Before = () => account = new Account();
                Context["account is in credit"] = () =>
                {
                    Before = () => account.Balance = 500;
                    It["the Account dispenses cash"] = () => account.CanWithdraw(60).Should().BeTrue();
                };
                Context["account is overdrawn"] = () =>
                {
                    Before = () => account.Balance = -500;
                    It["the Account does not dispense cash"] = () => account.CanWithdraw(60).Should().BeFalse();
                };
            };
        }
    }

    public static class Describe_contexts_output
    {
        public static string Output = @"
describe contexts
  describe Account
    when withdrawing cash
      account is in credit
        the Account dispenses cash
      account is overdrawn
        the Account does not dispense cash

2 Examples, 0 Failed, 0 Pending
";
        public static int ExitCode = 0;
    }
}