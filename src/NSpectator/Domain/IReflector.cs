using System;
using System.Reflection;

namespace NSpectator.Domain
{
    /// <summary>
    /// Default IReflector implementation
    /// </summary>
    public class Reflector : IReflector
    {
        private readonly string dll;

        /// <summary>
        /// Construct reflector from assembly file name
        /// </summary>
        /// <param name="dll"></param>
        public Reflector(string dll)
        {
            this.dll = dll;
        }

        public Type[] GetTypesFrom()
        {
            return Assembly.LoadFrom(dll).GetTypes();
        }

        public Type[] GetTypesFrom(Assembly assembly)
        {
            return assembly.GetTypes();
        }
    }

    /// <summary>
    /// Reflector is used to find types in the assembly
    /// </summary>
    public interface IReflector
    {
        Type[] GetTypesFrom();

        Type[] GetTypesFrom(Assembly assembly);
    }
}