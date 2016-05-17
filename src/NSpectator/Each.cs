using System;
using System.Collections.Generic;

namespace NSpectator
{
    /// <summary>
    /// This is a way for you to specify a collection of test data that needs to be asserted over a common set of examples.  New one of these up inline and tack on the .Do extension method for some
    /// powerful ways to execute examples across the collection.  
    /// <para>For more information visit https://github.com/nspectator/NSpectator/wiki </para>
    /// </summary>
    public class Each<T> : List<T> {}

    /// <summary>
    /// This is a way for you to specify a collection of test data that needs to be asserted over a common set of examples.  New one of these up inline and tack on the .Do extension method for some
    /// powerful ways to execute examples across the collection.  
    /// <para>For more information visit https://github.com/nspectator/NSpectator/wiki </para>
    /// </summary>
    public class Each<T1, T2> : List<Tuple<T1, T2>>
    {
        /// <summary>
        /// Add an items to collection
        /// </summary>
        /// <param name="t"></param>
        /// <param name="u"></param>
        public void Add(T1 t, T2 u)
        {
            base.Add(Tuple.Create(t, u));
        }
    }

    /// <summary>
    /// This is a way for you to specify a collection of test data that needs to be asserted over a common set of examples.  New one of these up inline and tack on the .Do extension method for some
    /// powerful ways to execute examples across the collection.  
    /// <para>For more information visit https://github.com/nspectator/NSpectator/wiki </para>
    /// </summary>
    public class Each<T1, T2, T3> : List<Tuple<T1, T2, T3>>
    {
        /// <summary>
        /// Add an items to collection
        /// </summary>
        /// <param name="t"></param>
        /// <param name="u"></param>
        /// <param name="v"></param>
        public void Add(T1 t, T2 u, T3 v)
        {
            base.Add(Tuple.Create(t, u, v));
        }
    }

    /// <summary>
    /// This is a way for you to specify a collection of test data that needs to be asserted over a common set of examples.  New one of these up inline and tack on the .Do extension method for some
    /// powerful ways to execute examples across the collection.  
    /// <para>For more information visit https://github.com/nspectator/NSpectator/wiki </para>
    /// </summary>
    public class Each<T1, T2, T3, T4> : List<Tuple<T1, T2, T3, T4>>
    {
        /// <summary>
        /// Add an items to collection
        /// </summary>
        /// <param name="t"></param>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="w"></param>
        public void Add(T1 t, T2 u, T3 v, T4 w)
        {
            base.Add(Tuple.Create(t, u, v, w));
        }
    }
}