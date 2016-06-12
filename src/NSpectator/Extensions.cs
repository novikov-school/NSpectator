using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace NSpectator
{
    /// <summary>
    /// Extension methods class
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// string will be repeated n number of times.
        /// </summary>
        [DebuggerNonUserCode]
        public static string Times(this string source, int times)
        {
            if (times == 0) return string.Empty;

            var s = string.Empty;

            for (int i = 0; i < times; i++)
                s += source;

            return s;
        }

        /// <summary>
        /// Action(T) will get executed for each item in the list.  
        /// You can use this to specify a suite of data that needs to be executed across a common set of examples.
        /// </summary>
        [DebuggerNonUserCode]
        public static void Do<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var t in source)
            {
                action(t);
            }
        }

        /// <summary>
        /// Action(T) will get executed for each item in the list.  
        /// You can use this to specify a suite of data that needs to be executed across a common set of examples.
        /// </summary>
        [DebuggerNonUserCode]
        internal static List<T> DoIsolate<T>(this IEnumerable<T> source, Action<T> action)
        {
            var copy = new List<T>(source);
            foreach (var t in copy)
            {
                action(t);
            }
            return copy;
        }

        /// <summary>
        /// Action(T, T) will get executed for each consecutive 2 items in a list.  You can use this to specify a suite of data that needs to be executed across a common set of examples.
        /// </summary>
        /// <returns></returns>
        [DebuggerNonUserCode]
        public static IEnumerable<T> EachConsecutive2<T>(this IEnumerable<T> source, Action<T, T> action)
        {
            var array = source.ToArray();
            for (int i = 0; i < array.Length - 1; i++)
            {
                action(array[i], array[i + 1]);
            }
            return array;
        }

        /// <summary>
        /// Action(T, U) will get executed for each item in the list.  You can use this to specify a suite of data that needs to be executed across a common set of examples.
        /// </summary>
        [DebuggerNonUserCode]
        public static void Do<T, U>(this Each<T, U> source, Action<T, U> action)
        {
            foreach (var tup in source)
                action(tup.Item1, tup.Item2);
        }

        /// <summary>
        /// Action(T, U) will get executed for each item in the list.  You can use this to specify a suite of data that needs to be executed across a common set of examples.
        /// </summary>
        [DebuggerNonUserCode]
        public static void Do<T, U>(this Dictionary<T, U> source, Action<T, U> action)
        {
            foreach (var kvp in source)
                action(kvp.Key, kvp.Value);
        }

        /// <summary>
        /// Action(T, U, V) will get executed for each item in the list.  You can use this to specify a suite of data that needs to be executed across a common set of examples.
        /// </summary>
        [DebuggerNonUserCode]
        public static void Do<T, U, V>(this Each<T, U, V> source, Action<T, U, V> action)
        {
            foreach (var tup in source)
                action(tup.Item1, tup.Item2, tup.Item3);
        }

        /// <summary>
        /// Action(T, U, V, W) will get executed for each item in the list.  You can use this to specify a suite of data that needs to be executed across a common set of examples.
        /// </summary>
        [DebuggerNonUserCode]
        public static void Do<T, U, V, W>(this Each<T, U, V, W> source, Action<T, U, V, W> action)
        {
            foreach (var tup in source)
                action(tup.Item1, tup.Item2, tup.Item3, tup.Item4);
        }

        /// <summary>
        /// Action will be executed n number of times.
        /// </summary>
        [DebuggerNonUserCode]
        public static void Times(this int number, Action action)
        {
            for (int i = 1; i <= number; i++)
            {
                action();
            }
        }

        /// <summary>
        /// Execute action on enumerable
        /// </summary>
        /// <param name="list"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        [DebuggerNonUserCode]
        public static void Each<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (T t in list)
            {
                action(t);
            }
        }

        /// <summary>
        /// Action will be executed n number of times.
        /// </summary>
        [DebuggerNonUserCode]
        public static void Times(this int number, Action<int> action)
        {
            for (int i = 1; i <= number; i++)
            {
                action(i);
            }
        }

        /// <summary>
        /// Flattens an Enumerable&lt;string&gt; into one string with optional separator
        /// </summary>
        [DebuggerNonUserCode]
        public static string Flatten(this IEnumerable<string> source, string separator = "")
        {
            return string.Join(separator, source);
        }

        /// <summary>
        /// Flattens an Enumerable&lt;T&gt; into one string with optional separator
        /// </summary>
        [DebuggerNonUserCode]
        public static string Flatten<T>(this IEnumerable<T> source, string separator = "")
        {
            return string.Join(separator, source.Select(o => o.ToString()));
        }

        /// <summary>
        /// Safely access a property or method of type T. If it is null or throws
        /// the fallback will be used instead
        /// </summary>
        [DebuggerNonUserCode]
        public static U GetOrFallback<T, U>(this T t, Func<T, U> func, U fallback)
        {
            try
            {
                if (func(t) == null)
                    return fallback;

                return func(t);
            }
            catch
            {
                return default(U);
            }
        }

        /// <summary>
        /// Create an IEnumerable&lt;int&gt; range from x to y
        /// eg. 1.To(3) would be [1,2,3]
        /// </summary>
        [DebuggerNonUserCode]
        public static IEnumerable<int> To(this int start, int end)
        {
            for (int i = start; i <= end; i++)
                yield return i;
        }

        public static void To(this int start, int end, Action<int> action)
        {
            To(start, end).Do(action);
        }

        /// <summary>
        /// Extension method that wraps String.Format.
        /// <para>Usage: string result = "{0} {1}".With("hello", "world");</para>
        /// </summary>
        [DebuggerNonUserCode]
        public static string With(this string source, params object[] objects)
        {
            var o = Sanitize(objects);
            return string.Format(source, o);
        }

        public static void SafeInvoke<T>(this Action<T> action, T t)
        {
            action?.Invoke(t);
        }

        public static void SafeInvoke(this Action action)
        {
            action?.Invoke();
        }

        public static void SafeInvoke<T>(this Func<T, Task> asyncAction, T t)
        {
            if (asyncAction != null)
            {
                Func<Task> asyncWork = () => asyncAction(t);

                asyncWork.Offload();
            }
        }

        public static void SafeInvoke(this Func<Task> asyncAction)
        {
            asyncAction?.Offload();
        }

        public static void Offload(this Func<Task> asyncWork)
        {
            try
            {
                Task offloadedWork = Task.Run(asyncWork);

                offloadedWork.Wait();
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.Flatten().InnerExceptions.First()).Throw();
            }
        }

        public static string[] Sanitize(this object[] source)
        {
            return source.ToList().Select(o =>
            {
                if (o.GetType() == typeof(int[]))
                {
                    var s = "";

                    (o as int[]).Do(i => s += i + ",");

                    if (s == "")
                        return "[]";

                    return "[" + s.Remove(s.Length - 1, 1) + "]";
                }

                return o.ToString();
            }).ToArray();
        }
    }
}