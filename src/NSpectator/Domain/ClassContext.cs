using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NSpectator.Domain.Extensions;

namespace NSpectator.Domain
{
    public class ClassContext : Context
    {
        public override void Build(Spec instance = null)
        {
            BuildMethodLevelBefore();

            BuildMethodLevelBeforeAll();

            BuildMethodLevelAct();

            BuildMethodLevelAfter();

            BuildMethodLevelAfterAll();

            var spec = type.Instance<Spec>();

            spec.tagsFilter = tagsFilter ?? new Tags();

            base.Build(spec);
        }

        public override bool IsSub(Type baseType)
        {
            while (baseType != null && baseType.IsAbstract)
            {
                baseType = baseType.BaseType;
            }

            return baseType == type;
        }

        IEnumerable<MethodInfo> GetMethodsFromHierarchy(Func<Type, MethodInfo> methodAccessor)
        {
            return classHierarchyToClass.Select(methodAccessor).Where(mi => mi != null);
        }

        void BuildMethodLevelBefore()
        {
            var befores = GetMethodsFromHierarchy(conventions.GetMethodLevelBefore).ToList();

            if (befores.Count > 0)
            {
                BeforeInstance = instance => befores.DoIsolate(b => b.Invoke(instance, null));
            }

            var asyncBefores = GetMethodsFromHierarchy(conventions.GetAsyncMethodLevelBefore).ToList();

            if (asyncBefores.Count > 0)
            {
                BeforeInstanceAsync = instance => asyncBefores.DoIsolate(b => new AsyncMethodLevelBefore(b).Run(instance));
            }
        }

        void BuildMethodLevelBeforeAll()
        {
            var beforeAlls = GetMethodsFromHierarchy(conventions.GetMethodLevelBeforeAll).ToList();

            if (beforeAlls.Count > 0)
            {
                BeforeAllInstance = instance => beforeAlls.DoIsolate(a => a.Invoke(instance, null));
            }

            var asyncBeforeAlls = GetMethodsFromHierarchy(conventions.GetAsyncMethodLevelBeforeAll).ToList();

            if (asyncBeforeAlls.Count > 0)
            {
                BeforeAllInstanceAsync = instance => asyncBeforeAlls.DoIsolate(b => new AsyncMethodLevelBeforeAll(b).Run(instance));
            }
        }

        void BuildMethodLevelAct()
        {
            var acts = GetMethodsFromHierarchy(conventions.GetMethodLevelAct).ToList();

            if (acts.Count > 0)
            {
                ActInstance = instance => acts.DoIsolate(a => a.Invoke(instance, null));
            }

            var asyncActs = GetMethodsFromHierarchy(conventions.GetAsyncMethodLevelAct).ToList();

            if (asyncActs.Count > 0)
            {
                ActInstanceAsync = instance => asyncActs.DoIsolate(a => new AsyncMethodLevelAct(a).Run(instance));
            }
        }

        void BuildMethodLevelAfter()
        {
            var afters = GetMethodsFromHierarchy(conventions.GetMethodLevelAfter).Reverse().ToList();

            if (afters.Count > 0)
            {
                AfterInstance = instance => afters.DoIsolate(a => a.Invoke(instance, null));
            }

            var asyncAfters = GetMethodsFromHierarchy(conventions.GetAsyncMethodLevelAfter).Reverse().ToList();

            if (asyncAfters.Count > 0)
            {
                AfterInstanceAsync = instance => asyncAfters.DoIsolate(a => new AsyncMethodLevelAfter(a).Run(instance));
            }
        }

        void BuildMethodLevelAfterAll()
        {
            var afterAlls = GetMethodsFromHierarchy(conventions.GetMethodLevelAfterAll).Reverse().ToList();

            if (afterAlls.Count > 0)
            {
                AfterAllInstance = instance => afterAlls.DoIsolate(a => a.Invoke(instance, null));
            }

            var asyncAfterAlls = GetMethodsFromHierarchy(conventions.GetAsyncMethodLevelAfterAll).Reverse().ToList();

            if (asyncAfterAlls.Count > 0)
            {
                AfterAllInstanceAsync = instance => asyncAfterAlls.DoIsolate(a => new AsyncMethodLevelAfterAll(a).Run(instance));
            }
        }

        public ClassContext(Type type, Conventions conventions = null, Tags tagsFilter = null, string tags = null)
            : base(type.CleanName(), tags)
        {
            this.type = type;

            this.conventions = conventions ?? new DefaultConventions().Initialize();

            this.tagsFilter = tagsFilter;

            if (type != typeof(Spec))
            {
                classHierarchyToClass.AddRange(type.GetAbstractBaseClassChainWithClass());
            }
        }

        public Type type;

        public Tags tagsFilter;
        List<Type> classHierarchyToClass = new List<Type>();
        Conventions conventions;
    }
}