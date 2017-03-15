using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NSpectator.Domain;

// ReSharper disable InconsistentNaming

namespace NSpectator
{
    /// <summary>
    /// Inherit from this class to create your own specifications.  
    /// SpecRunner will look through your project for
    /// classes that derive from this class (inheritance chain is taken into consideration).
    /// </summary>
    public class Spec
    {
        /// <summary>
        /// ctor
        /// </summary>
        public Spec()
        {
            Context = new ActionRegister(AddContext);
            xContext = new ActionRegister(AddIgnoredContext);
            Describe = new ActionRegister(AddContext);
            xDescribe = new ActionRegister(AddIgnoredContext);

            It = new ActionRegister((name, tags, action) => AddExample(new Example(name, tags, action, pending: action == Todo)));
            xIt = new ActionRegister((name, tags, action) => AddExample(new Example(name, tags, action, pending: true)));

            ItAsync = new AsyncActionRegister((name, tags, asyncAction) => AddExample(new AsyncExample(name, tags, asyncAction, pending: asyncAction == TodoAsync)));
            xItAsync = new AsyncActionRegister((name, tags, asyncAction) => AddExample(new AsyncExample(name, tags, asyncAction, pending: true)));
        }

        /// <summary>
        /// Create a specification/example using a single line lambda with an assertion/expectation
        /// The name of the specification will be parsed from the Expression
        /// <para>For Example:</para>
        /// <para>specify = () => _controller.should_be(false);</para>
        /// </summary>
        public virtual Expression<Action> Specify
        {
            set { AddExample(new Example(value)); }
        }

        /* No need for the following, as async lambda expressions cannot be converted to expression trees:

        public virtual Expression<Func<Task>> specifyAsync { ... }

         */

        /// <summary>
        /// Mark a spec as pending
        /// <para>For Example:</para>
        /// <para>xspecify = () => _controller.Expected().False();</para>
        /// <para>(the example will be marked as pending any lambda provided will not be executed)</para>
        /// </summary>
        public virtual Expression<Action> xSpecify
        {
            set { AddExample(new Example(value, pending: true)); }
        }

        /* No need for the following, as async lambda expressions cannot be converted to expression trees:

        public virtual Expression<Func<Task>> xspecifyAsync { ... }

        */

        /// <summary>
        /// This Action gets executed before each example is run.
        /// <para>For Example:</para>
        /// <para>before = () => someList = new List&lt;int&gt;();</para>
        /// <para>The before can be a multi-line lambda.  Setting the member multiple times through out sub-contexts will not override the action, but instead will append to your setup (this is a good thing).  
        /// For more information visit https://github.com/nspectator/NSpectator/wiki </para>
        /// </summary>
        public virtual Action Before
        {
            get { return InnerContext.Before; }
            set { InnerContext.Before = value; }
        }

        /// <summary>
        /// This Function gets executed asynchronously before each example is run.
        /// <para>For Example:</para>
        /// <para>beforeAsync = async () => someList = await GetListAsync();</para>
        /// <para>The beforeAsync can be a multi-line lambda.  Setting the member multiple times through out sub-contexts will not override the action, but instead will append to your setup (this is a good thing).</para>
        /// <para>For more information visit https://github.com/nspectator/NSpectator/wiki </para>
        /// </summary>
        public virtual Func<Task> BeforeAsync
        {
            get { return InnerContext.BeforeAsync; }
            set { InnerContext.BeforeAsync = value; }
        }

        /// <summary>
        /// This Action is an alias of before. This Action gets executed before each example is run.
        /// <para>For Example:</para>
        /// <para>beforeEach = () => someList = new List&lt;int&gt;();</para>
        /// <para>The beforeEach can be a multi-line lambda.  Setting the member multiple times through out sub-contexts will not override the action, but instead will append to your setup (this is a good thing).</para>  
        /// <para>For more information visit https://github.com/nspectator/NSpectator/wiki </para>
        /// </summary>
        public virtual Action BeforeEach
        {
            get { return InnerContext.Before; }
            set { InnerContext.Before = value; }
        }

        /// <summary>
        /// This Function is an alias of beforeAsync. It gets executed asynchronously before each example is run.
        /// <para>For Example:</para>
        /// <para>beforeEachAsync = async () => someList = await GetListAsync();</para>
        /// <para>The beforeEachAsync can be a multi-line lambda.  Setting the member multiple times through out sub-contexts will not override the action, but instead will append to your setup (this is a good thing). </para>
        /// <para> For more information visit https://github.com/nspectator/NSpectator/wiki </para>
        /// </summary>
        public virtual Func<Task> BeforeEachAsync
        {
            get { return InnerContext.BeforeAsync; }
            set { InnerContext.BeforeAsync = value; }
        }

        /// <summary>
        /// This Action gets executed before all examples in a context.
        /// <para>For Example:</para>
        /// <para>beforeAll = () => someList = new List&lt;int&gt;();</para>
        /// <para>The beforeAll can be a multi-line lambda.  Setting the member multiple times through out sub-contexts will not override the action, but instead will append to your setup (this is a good thing). </para>
        /// <para>For more information visit https://github.com/nspectator/NSpectator/wiki </para>
        /// </summary>
        public virtual Action BeforeAll
        {
            get { return InnerContext.BeforeAll; }
            set { InnerContext.BeforeAll = value; }
        }

        /// <summary>
        /// This Function gets executed asynchronously before all examples in a context.
        /// <para>For Example:</para>
        /// <para>beforeAllAsync = async () => someList = await GetListAsync();</para>
        /// <para>The beforeAllAsync can be a multi-line lambda.  Setting the member multiple times through out sub-contexts will not override the action, but instead will append to your setup (this is a good thing).</para>
        /// <para>For more information visit https://github.com/nspectator/NSpectator/wiki </para>
        /// </summary>
        public virtual Func<Task> BeforeAllAsync
        {
            get { return InnerContext.BeforeAllAsync; }
            set { InnerContext.BeforeAllAsync = value; }
        }

        /// <summary>
        /// This Action gets executed after each example is run.
        /// <para>For Example:</para>
        /// <para>after = () => someList = new List&lt;int&gt;();</para>
        /// <para>The after can be a multi-line lambda.  Setting the member multiple times through out sub-contexts will not override the action, but instead will append to your setup (this is a good thing). </para>
        /// <para>For more information visit https://github.com/nspectator/NSpectator/wiki </para>
        /// </summary>
        public virtual Action After
        {
            get { return InnerContext.After; }
            set { InnerContext.After = value; }
        }

        /// <summary>
        /// This Function gets executed asynchronously after each example is run.
        /// <para>For Example:</para>
        /// <para>afterAsync = async () => someList = await GetListAsync();</para>
        /// <para>The after can be a multi-line lambda.  Setting the member multiple times through out sub-contexts will not override the action, but instead will append to your setup (this is a good thing). </para>
        /// <para>For more information visit https://github.com/nspectator/NSpectator/wiki </para>
        /// </summary>
        public virtual Func<Task> AfterAsync
        {
            get { return InnerContext.AfterAsync; }
            set { InnerContext.AfterAsync = value; }
        }

        /// <summary>
        /// This Action is an alias of after. This Action gets executed after each example is run.
        /// <para>For Example:</para>
        /// <para>afterEach = () => someList = new List&lt;int&gt;();</para>
        /// <para>The afterEach can be a multi-line lambda.  Setting the member multiple times through out sub-contexts will not override the action, but instead will append to your setup (this is a good thing). </para>
        /// <para>For more information visit https://github.com/nspectator/NSpectator/wiki </para>
        /// </summary>
        public virtual Action AfterEach
        {
            get { return InnerContext.After; }
            set { InnerContext.After = value; }
        }

        /// <summary>
        /// This Action is an alias of afterAsync. This Function gets executed asynchronously after each example is run.
        /// <para>For Example:</para>
        /// <para>afterEachAsync = async () => someList = await GetListAsync();</para>
        /// <para>The after can be a multi-line lambda.  Setting the member multiple times through out sub-contexts will not override the action, but instead will append to your setup (this is a good thing). </para>
        /// <para>For more information visit https://github.com/nspectator/NSpectator/wiki </para>
        /// </summary>
        public virtual Func<Task> AfterEachAsync
        {
            get { return InnerContext.AfterAsync; }
            set { InnerContext.AfterAsync = value; }
        }

        /// <summary>
        /// This Action gets executed after all examples in a context.
        /// <para>For Example:</para>
        /// <para>afterAll = () => someList = new List&lt;int&gt;();</para>
        /// <para>The afterAll can be a multi-line lambda.  Setting the member multiple times through out sub-contexts will not override the action, but instead will append to your setup (this is a good thing). </para>
        /// <para>For more information visit https://github.com/nspectator/NSpectator/wiki </para>
        /// </summary>
        public virtual Action AfterAll
        {
            get { return InnerContext.AfterAll; }
            set { InnerContext.AfterAll = value; }
        }

        /// <summary>
        /// This Function gets executed asynchronously after all examples in a context.
        /// <para>For Example:</para>
        /// <para>afterAllAsync = async () => someList = await GetListAsync();</para>
        /// <para>The afterAllAsync can be a multi-line lambda.  Setting the member multiple times through out sub-contexts will not override the action, but instead will append to your setup (this is a good thing).</para>
        /// <para>For more information visit https://github.com/nspectator/NSpectator/wiki </para>
        /// </summary>
        public virtual Func<Task> AfterAllAsync
        {
            get { return InnerContext.AfterAllAsync; }
            set { InnerContext.AfterAllAsync = value; }
        }

        /// <summary>
        /// Assign this member within your context.  The Action assigned will gets executed
        /// with every example in scope.  Befores will run first, then acts, then your examples.  
        /// <para>It's a way for you to define once a common Act in Arrange-Act-Assert for all subcontexts. </para>
        /// <para>For more information visit https://github.com/nspectator/NSpectator/wiki </para>
        /// </summary>
        public virtual Action Act
        {
            get { return InnerContext.Act; }
            set { InnerContext.Act = value; }
        }

        /// <summary>
        /// Assign this member within your context.  The Function assigned will gets executed asynchronously
        /// with every example in scope.  Befores will run first, then acts, then your examples.  
        /// It's a way for you to define once a common Act in Arrange-Act-Assert for all subcontexts.  
        /// For more information visit https://github.com/nspectator/NSpectator/wiki
        /// </summary>
        public virtual Func<Task> ActAsync
        {
            get { return InnerContext.ActAsync; }
            set { InnerContext.ActAsync = value; }
        }

        /// <summary>
        /// Create a subcontext.
        /// <para>For Examples see https://github.com/nspectator/NSpectator/wiki </para>
        /// </summary>
        public ActionRegister Context { get; }

        /// <summary>
        /// Mark a subcontext as pending (add all child contexts as pending)
        /// </summary>
        public ActionRegister xContext { get; }

        /// <summary>
        /// This is an alias for creating a subcontext.  Use this to create sub contexts within your methods.
        /// <para>For Examples see https://github.com/nspectator/NSpectator/wiki </para>
        /// </summary>
        public ActionRegister Describe { get; }

        /// <summary>
        /// This is an alias for creating a xcontext.
        /// <para>For Examples see https://github.com/nspectator/NSpectator/wiki </para>
        /// </summary>
        public ActionRegister xDescribe { get; }

        /// <summary>
        /// Create a specification/example using a name and a lambda with an assertion(should).
        /// <para>For Example:</para>
        /// <para>it["should return false"] = () => _controller.Should().BeFalse();</para>
        /// </summary>
        public ActionRegister It { get; }

        /// <summary>
        /// Create an asynchronous specification/example using a name and an async lambda with an assertion(should).
        /// <para>For Example:</para>
        /// <para>itAsync["should return false"] = async () => (await GetResultAsync()).Should().BeFalse();</para>
        /// </summary>
        public AsyncActionRegister ItAsync { get; }

        /// <summary>
        /// Mark a spec as pending
        /// <para>For Example:</para>
        /// <para>xit["should return false"] = () => _controller.should_be(false);</para>
        /// <para>(the example will be marked as pending, any lambda provided will not be executed)</para>
        /// </summary>
        public ActionRegister xIt { get; }

        /// <summary>
        /// Mark an asynchronous spec as pending
        /// <para>For Example:</para>
        /// <para>xitAsync["should return false"] = async () => (await GetResultAsync()).should_be(false);</para>
        /// <para>(the example will be marked as pending, any lambda provided will not be executed)</para>
        /// </summary>
        public AsyncActionRegister xItAsync { get; }

        /// <summary>
        /// Set up a pending spec.
        /// <para>For Example:</para>
        /// <para>it["a test i haven't flushed out yet, but need to"] = todo;</para>
        /// </summary>
        public readonly Action Todo = () => { };

        /// <summary>
        /// Set up a pending asynchronous spec.
        /// <para>For Example:</para>
        /// <para>itAsync["a test i haven't flushed out yet, but need to"] = todoAsync;</para>
        /// </summary>
        public readonly Func<Task> TodoAsync = () => Task.Run(() => { });

        /// <summary>
        /// Set up an expectation for a particular exception type to be thrown before expectation.
        /// <para>For Example:</para>
        /// <para>it["should throw exception"] = expect&lt;InvalidOperationException&gt;();</para>
        /// </summary>
        public virtual Action Expect<T>() where T : Exception
        {
            return Expect<T>(expectedMessage: null);
        }

        /// <summary>
        /// Set up an expectation for a particular exception type to be thrown before expectation, with an expected message.
        /// <para>For Example:</para>
        /// <para>it["should throw exception"] = expect&lt;InvalidOperationException&gt;();</para>
        /// </summary>
        public virtual Action Expect<T>(string expectedMessage) where T : Exception
        {
            var specContext = InnerContext;

            return () =>
            {
                if (specContext.Exception == null)
                    throw new ExceptionNotThrown(IncorrectType<T>());

                AssertExpectedException<T>(specContext.Exception, expectedMessage);

                // do not clear exception right now, during first phase, but leave a note for second phase
                specContext.ClearExpectedException = true;
            };
        }

        /// <summary>
        /// Set up an expectation for a particular exception type to be thrown inside passed action.
        /// <para>For Example:</para>
        /// <para>it["should throw exception"] = expect&lt;InvalidOperationException&gt;(() => SomeMethodThatThrowsException());</para>
        /// </summary>
        public virtual Action Expect<T>(Action action) where T : Exception
        {
            return Expect<T>(null, action);
        }

        /// <summary>
        /// Set up an expectation for a particular exception type to be thrown inside passed action, with an expected message.
        /// <para>For Example:</para>
        /// <para>it["should throw exception with message Error"] = expect&lt;InvalidOperationException&gt;("Error", () => SomeMethodThatThrowsException());</para>
        /// </summary>
        public virtual Action Expect<T>(string expectedMessage, Action action) where T : Exception
        {
            return () =>
            {
                var closureType = typeof(T);

                try
                {
                    action();

                    throw new ExceptionNotThrown(IncorrectType<T>());
                }
                catch (ExceptionNotThrown)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    AssertExpectedException<T>(ex, expectedMessage);
                }
            };
        }

        /// <summary>
        /// Set up an asynchronous expectation for a particular exception type to be thrown inside passed asynchronous action.
        /// <para>For Example:</para>
        /// <para>itAsync["should throw exception"] = expectAsync&lt;InvalidOperationException&gt;(async () => await SomeAsyncMethodThatThrowsException());</para>
        /// </summary>
        public virtual Func<Task> ExpectAsync<T>(Func<Task> asyncAction) where T : Exception
        {
            return ExpectAsync<T>(null, asyncAction);
        }

        /// <summary>
        /// Set up an asynchronous expectation for a particular exception type to be thrown inside passed asynchronous action, with an expected message.
        /// <para>For Example:</para>
        /// <para>itAsync["should throw exception with message Error"] = expectAsync&lt;InvalidOperationException&gt;("Error", async () => await SomeAsyncMethodThatThrowsException());</para>
        /// </summary>
        public virtual Func<Task> ExpectAsync<T>(string expectedMessage, Func<Task> asyncAction) where T : Exception
        {
            return async () =>
            {
                var closureType = typeof(T);

                try
                {
                    await asyncAction();

                    throw new ExceptionNotThrown(IncorrectType<T>());
                }
                catch (ExceptionNotThrown)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    AssertExpectedException<T>(ex, expectedMessage);
                }
            };
        }

        /// <summary>
        /// Override this method to alter the stack trace that NSpectator prints. 
        /// This is useful to override if you want to provide additional information 
        /// (eg. information from a log that is generated out of proc).
        /// </summary>
        /// <param name="flattenedStackTrace">A clean stack trace that excludes NSpectator specific namespaces</param>
        /// <returns></returns>
        public virtual string StackTraceToPrint(string flattenedStackTrace)
        {
            return flattenedStackTrace;
        }

        /// <summary>
        /// Override this method to return another exception in the event of a failure of a test.  This is useful to override
        /// when catching for specific exceptions and returning a more meaningful exception to the developer.
        /// </summary>
        /// <param name="originalException">Original exception that was thrown.</param>
        /// <returns></returns>
        public virtual Exception ExceptionToReturn(Exception originalException)
        {
            return originalException;
        }

        private static string IncorrectType<T>() where T : Exception
        {
            return $"Exception of type {typeof(T).Name} was not thrown.";
        }

        private static string IncorrectMessage(string expected, string actual)
        {
            return $@"Expected message: ""{expected}"" But was: ""{actual}""";
        }

        private void AddExample(ExampleBase example)
        {
            InnerContext.AddExample(example);
        }

        private void AddContext(string name, string tags, Action action)
        {
            var childContext = new Context(name, tags);

            RunContext(childContext, action);
        }

        private void AddIgnoredContext(string name, string tags, Action action)
        {
            var ignored = new Context(name, tags, isPending: true);

            RunContext(ignored, action);
        }

        private void RunContext(Context ctx, Action action)
        {
            InnerContext.AddContext(ctx);

            var beforeContext = InnerContext;

            InnerContext = ctx;

            action();

            InnerContext = beforeContext;
        }

        private static void AssertExpectedException<T>(Exception actualException, string expectedMessage) where T : Exception
        {
            var expectedType = typeof(T);
            Exception matchingException = null;

            if (actualException.GetType() == expectedType)
            {
                matchingException = actualException;
            }
            else
            {
                var aggregateException = actualException as AggregateException;
                if (aggregateException != null)
                {
                    foreach (var innerException in aggregateException.InnerExceptions)
                    {
                        if (innerException.GetType() != expectedType) continue;
                        matchingException = innerException;
                        break;
                    }
                }
            }

            if (matchingException == null)
            {
                throw new ExceptionNotThrown(IncorrectType<T>());
            }

            if (expectedMessage != null && expectedMessage != matchingException.Message)
            {
                throw new ExceptionNotThrown(IncorrectMessage(expectedMessage, matchingException.Message));
            }
        }

        /// <summary>
        /// Error handler
        /// </summary>
        /// <param name="flattenedStackTrace"></param>
        /// <returns></returns>
        protected virtual string OnError(string flattenedStackTrace)
        {
            return flattenedStackTrace;
        }

        internal Context InnerContext { get; set; }

        /// <summary>Tags required to be present or not present in context or example</summary>
        /// <remarks>
        /// Currently, multiple tags indicates any of the tags must be present to be included/excluded.  In other words, they are OR'd, not AND'd.
        /// NOTE: Cucumber's tags wiki offers ideas for handling tags: https://github.com/cucumber/cucumber/wiki/tags
        /// </remarks>
        internal Tags TagsFilter = new Tags();
    }
}