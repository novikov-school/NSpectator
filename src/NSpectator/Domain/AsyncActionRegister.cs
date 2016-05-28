using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSpectator.Domain
{
    /// <summary>
    /// Async example registrator
    /// </summary>
    public class AsyncActionRegister
    {
        /// <summary>
        /// Initialize a new example registrator using specified action to add 
        /// async examples which returns Task
        /// </summary>
        /// <param name="asyncActionSetter"></param>
        public AsyncActionRegister(Action<string, string, Func<Task>> asyncActionSetter)
        {
            this.AsyncActionSetter = asyncActionSetter;
        }

        private Action<string, string, Func<Task>> AsyncActionSetter { get; }

        /// <summary>
        /// Async example declaration
        /// <para>itAsync["do something"] = async () => { ... }</para>
        /// </summary>
        /// <param name="key"></param>
        public Func<Task> this[string key]
        {
            set { AsyncActionSetter(key, null, value); }
        }

        /// <summary>
        /// Async example declaration with addition tags
        /// <para>itAsync["do something"] = async () => { ... }</para>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="tags"></param>
        public Func<Task> this[string key, string tags]
        {
            set { AsyncActionSetter(key, tags, value); }
        }
    }
}