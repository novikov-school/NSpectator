#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion

using System.Linq;
using NUnit.Framework;
using System;
using NSpectator.Domain;
using NSpectator.Domain.Formatters;
using SampleSpecs.WebSite;
using SampleSpecsFocus;

namespace NSpectator.Specs
{
    [TestFixture]
    public class Describe_Output
    {
        [Test,
         TestCase(typeof(My_first_spec_output),
                  new [] { typeof(My_first_spec) },
                  ""),
         //TestCase(typeof(Describe_specifications_output),
         //         new [] { typeof(Describe_specifications) },
         //         ""),
         TestCase(typeof(Describe_before_output),
                  new [] { typeof(Describe_before) },
                  ""),
         TestCase(typeof(Describe_contexts_output),
                  new [] { typeof(Describe_contexts) },
                  ""),
         TestCase(typeof(Describe_pending_output),
                  new [] { typeof(Describe_pending) },
                  ""),
         TestCase(typeof(Describe_helpers_output),
                  new [] { typeof(Describe_helpers) },
                  ""),
         TestCase(typeof(Describe_async_helpers_output),
                  new [] { typeof(Describe_async_helpers) },
                  ""),
         TestCase(typeof(Describe_batman_sound_effects_as_text_output),
                  new [] { typeof(Describe_batman_sound_effects_as_text) },
                  ""),
         TestCase(typeof(Describe_class_level_output),
                  new [] { typeof(Describe_class_level) },
                  ""),
         TestCase(typeof(Given_the_sequence_continues_with_2_output),
                  new []
                  {
                      typeof(Given_the_sequence_continues_with_2),
                      typeof(Given_the_sequence_starts_with_1)
                  },
                  ""),
         TestCase(typeof(Describe_exception_output),
                  new [] { typeof(Describe_exception) },
                  ""),
         //TestCase(typeof(Describe_context_stack_trace_output),
         //         new [] { typeof(Describe_context_stack_trace) },
         //         ""),
         TestCase(typeof(Describe_ICollection_output),
                  new []
                  {
                      typeof(Describe_ICollection),
                      typeof(Describe_LinkedList),
                      typeof(Describe_List)
                  },
                  ""),
         TestCase(typeof(Describe_changing_stacktrace_message_output),
                  new [] { typeof(Describe_changing_stacktrace_message) },
                  ""),
         TestCase(typeof(Describe_changing_failure_exception_output),
                  new [] { typeof(Describe_changing_failure_exception) },
                  ""),
         TestCase(typeof(Describe_focus_output),
                  new [] { typeof(Describe_focus) },
                  "focus")]
        
        public void Output_verification(Type output, Type []testClasses, string tags)
        {
            var finder = new SpecFinder(testClasses, "");
            var tagsFilter = new Tags().Parse(tags);
            var builder = new ContextBuilder(finder, tagsFilter, new DefaultConventions());
            var consoleFormatter = new ConsoleFormatter();

            var actual = new System.Collections.Generic.List<string>();
            consoleFormatter.WriteLineDelegate = actual.Add;

            var runner = new ContextRunner(tagsFilter, consoleFormatter, false);
            runner.Run(builder.Contexts().Build());

            var expectedString = ScrubStackTrace(ScrubNewLines(output.GetField("Output").GetValue(null) as string));
            var actualString = ScrubStackTrace(string.Join("\n", actual)).Trim();
            actualString.should_be(expectedString);

            var guid = Guid.NewGuid();
        }

        static string ScrubNewLines(string s)
        {
            return s.Trim().Replace("\r\n", "\n").Replace("\r", "");
        }

        static string ScrubStackTrace(string s)
        {
            // Sort of a patch here: it could actually be generalized to more languages

            var withoutStackTrace = s.Split('\n')
                .Where(a => !a.Trim().StartsWith("at "))      // English OS
                .Where(a => !a.Trim().StartsWith("in "));     // Italian OS

            return string.Join("\n", withoutStackTrace).Replace("\r", "");
        }
    }
}
