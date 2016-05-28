using System.Collections.Generic;
using System.Linq;
using NSpectator.Domain.Formatters;

namespace NSpectator.Domain
{
    public class ContextCollection : List<Context>
    {
        public IEnumerable<ExampleBase> Examples => this.SelectMany(c => c.AllExamples());
        
        public IEnumerable<ExampleBase> Failures => Examples.Where(e => e.Exception != null);
        

        public IEnumerable<ExampleBase> Pendings => Examples.Where(e => e.Pending);
        

        public ContextCollection Build()
        {
            this.DoIsolate(c => c.Build());

            return this;
        }

        public void Run(bool failFast = false)
        {
            Run(new SilentLiveFormatter(), failFast);
        }

        public void Run(ILiveFormatter formatter, bool failFast)
        {
            this.DoIsolate(c => c.Run(formatter, failFast: failFast));

            this.DoIsolate(c => c.AssignExceptions());
        }

        public void TrimSkippedContexts()
        {
            this.DoIsolate(c => c.TrimSkippedDescendants());

            this.RemoveAll(c => !c.HasAnyExecutedExample());
        }

        public IEnumerable<Context> AllContexts()
        {
            return this.SelectMany(c => c.AllContexts());
        }

        public Context Find(string name)
        {
            return AllContexts().FirstOrDefault(c => c.Name == name);
        }

        public ExampleBase FindExample(string name)
        {
            return Examples.FirstOrDefault(e => e.Spec == name);
        }

        public ContextCollection(IEnumerable<Context> contexts) : base(contexts) {}

        public ContextCollection() {}

        public bool AnyTaggedWithFocus()
        {
            return AnyTaggedWith(Tags.Focus);
        }

        bool AnyTaggedWith(string tag)
        {
            return AnyExamplesTaggedWith(tag) || AnyContextsTaggedWith(tag);
        }

        bool AnyContextsTaggedWith(string tag)
        {
            return AllContexts().Any(s => s.Tags.Contains(tag));
        }

        bool AnyExamplesTaggedWith(string tag)
        {
            return AllContexts().SelectMany(s => s.AllExamples()).Any(s => s.Tags.Contains(tag));
        }
    }
}