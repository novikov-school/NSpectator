using System.Linq;
using System.Reflection;
using NSpectator;
using NSpectator.Domain;
using NSpectator.Domain.Formatters;
using FluentAssertions;

/*
 * Howdy,
 * 
 * This is NSpectator's DebuggerShim.  It will allow you to use Resharper's test runner to run
 * NSpectator tests that are in the same Assembly as this class.
 */

//[TestFixture]
public class DebuggerShim
{
    //[Test]
    public void Debug()
    {
        var tagOrClassName = "class_or_tag_you_want_to_debug";

        var types = GetType().Assembly.GetTypes(); 
        // OR
        // var types = new Type[]{typeof(Some_Type_Containg_some_Specs)};

        var finder = new SpecFinder(types, "");

        var tagsFilter = new Tags().Parse(tagOrClassName);

        var builder = new ContextBuilder(finder, tagsFilter, new DefaultConventions());

        var runner = new ContextRunner(tagsFilter, new ConsoleFormatter(), false);

        var results = runner.Run(builder.Contexts().Build());

        //assert that there aren't any failures
        results.Failures().Count().Should().Be(0);
    }
}
