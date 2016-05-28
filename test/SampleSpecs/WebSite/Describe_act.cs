#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion

using NSpectator;
using FluentAssertions;

namespace SampleSpecs.WebSite
{
    [Tag("describe_act")]
    public class Describe_batman_sound_effects_as_text : Spec
    {
        string sound;

        void they_are_loud_and_emphatic()
        {
            //act runs after all the befores, and before each spec
            //declares a common act (arrange, act, assert) for all subcontexts
            Act = () => sound = sound.ToUpper() + "!!!";
            Context["given bam"] = () =>
            {
                Before = () => sound = "bam";
                It["should be BAM!!!"] =
                    () => sound.Should().Be("BAM!!!");
            };
            Context["given whack"] = () =>
            {
                Before = () => sound = "whack";
                It["should be WHACK!!!"] =
                    () => sound.Should().Be("WHACK!!!");
            };
        }
    }

    public static class Describe_batman_sound_effects_as_text_output
    {
        public static string Output = @"
describe batman sound effects as text
  they are loud and emphatic
    given bam
      should be BAM!!!
    given whack
      should be WHACK!!!

2 Examples, 0 Failed, 0 Pending
";
        public static int ExitCode = 0;
    }
}