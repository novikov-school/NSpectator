#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System.Text.RegularExpressions;
using NSpectator.Domain;
using FluentAssertions;

namespace NSpectator.Specs
{
    public static class SpecExtensions
    {
        public static void Should_have_passed(this ExampleBase example)
        {
            (example.HasRun && example.Exception == null).Should().BeTrue();
        }

        public static void Should_have_failed(this ExampleBase example)
        {
            (example.HasRun && example.Exception == null).Should().BeFalse();
        }

        public static string RegexReplace(this string input, string pattern, string replace)
        {
            return Regex.Replace(input, pattern, replace);
        }
    }
}