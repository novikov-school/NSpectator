using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NSpectator.Domain
{
    public abstract class ExampleBase
    {
        public static string Parse(Expression expressionBody)
        {
            var body = expressionBody.ToString();
            
            var sentence = body.Replace(".Expected(", ".expected(").Replace(".ToBe(", ".to_be(");
            sentence = sentence.Replace(")", " ");
            sentence = sentence.Replace(".", " ");
            sentence = sentence.Replace("(", " ").Replace("  ", " ").Trim().Replace("_", " ").Replace("\"", " ");

            while (sentence.Contains("  ")) sentence = sentence.Replace("  ", " ");

            return sentence.Trim();
        }

        public static string Parse(Expression<Action> exp)
        {
            return Parse(exp.Body);
        }

        public abstract void Run(Spec spec);

        public string FullName()
        {
            return Context.FullContext() + ". " + Spec + ".";
        }

        public bool Failed()
        {
            return Exception != null;
        }

        public void AssignProperException(Exception contextException)
        {
            if (contextException == null) return; //stick with whatever Exception may or may not be set on this Example

            if (Exception != null && Exception.GetType() != typeof(ExceptionNotThrown))
                Exception = new ExampleFailureException("Context Failure: " + contextException.Message + ", Example Failure: " + Exception.Message, contextException);

            if (Exception == null)
                Exception = new ExampleFailureException("Context Failure: " + contextException.Message, contextException);
        }

        public bool ShouldSkip(Tags tagsFilter)
        {
            return tagsFilter.ShouldSkip(Tags) || ((HasRun = true) && Pending);
        }

        public bool ShouldNotSkip(Tags tagsFilter)
        {
            // really should be the opposite of ShouldSkip.
            // but unfortunately calling ShouldSkip has side effects
            // see the HasRun assignment. calling ShouldSkip here thus
            // has side effects that fail some tests.
            return false == tagsFilter.ShouldSkip(Tags);
        }

        public override string ToString()
        {
            string pendingPrefix = (Pending ? "(x)" : string.Empty);
            string exceptionText = (Exception != null ? ", " + Exception.GetType().Name : string.Empty);

            return $"{pendingPrefix}{Spec}{exceptionText}";
        }

        protected ExampleBase(string name = "", string tags = "", bool pending = false)
        {
            Spec = name;

            Tags = Domain.Tags.ParseTags(tags);

            Pending = pending;
        }

        public bool Pending;
        public bool HasRun;
        public string Spec;
        public List<string> Tags;
        public Exception Exception;
        public Context Context;
    }
}