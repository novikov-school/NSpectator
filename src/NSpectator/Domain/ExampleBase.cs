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

            var cut = body.IndexOf(").");

            var sentence = body.Substring(cut + 1, body.Length - cut - 1)
                .Replace(")", " ")
                .Replace(".", " ")
                .Replace("(", " ")
                .Replace("  ", " ")
                .Trim()
                .Replace("_", " ")
                .Replace("\"", " ");

            while (sentence.Contains("  ")) sentence = sentence.Replace("  ", " ");

            return sentence.Trim();
        }

        public static string Parse(Expression<Action> exp)
        {
            return Parse(exp.Body);
        }

        public abstract void Run(Spec spec);

        /// <summary>
        /// Determine if the method is async
        /// </summary>
        public abstract bool IsAsync { get; }

        public string FullName => Context.FullContext + ". " + Spec + ".";

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
            // TODO remove side effects from here (HasRun = true)

            return tagsFilter.ShouldSkip(Tags) || ((HasRun = true) && Pending);
        }

        public bool ShouldNotSkip(Tags tagsFilter)
        {
            // really should be the opposite of ShouldSkip.
            // but unfortunately calling ShouldSkip has side effects
            // see the HasRun assignment. calling ShouldSkip here thus
            // has side effects that fail some tests.
            return !tagsFilter.ShouldSkip(Tags);
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

        public bool Pending { get; set; }

        public bool HasRun { get; set; }

        public string Spec { get; }

        public List<string> Tags { get; }

        public Exception Exception;

        public Context Context { get; internal set; }

    }
}