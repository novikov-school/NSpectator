#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System.Collections.Generic;
using FluentAssertions;
using NSpectator;

namespace SampleSpecs.WebSite
{
    public abstract class Describe_ICollection : Spec
    {
        protected ICollection<string> collection;

        void adding_to_collection()
        {
            Before = () => collection.Add("Item 1");

            It["contains the entry"] = () =>
                collection.Contains("Item 1").Should().Be(true);
        }
    }

    public class Describe_LinkedList : Describe_ICollection
    {
        LinkedList<string> linkedList;

        void before_each()
        {
            linkedList = new LinkedList<string>();
            collection = linkedList;
        }

        void specific_actions()
        {
            Before = () => collection.Add("Item 1");

            It["can add an item at the begining with ease"] = () =>
            {
                linkedList.AddFirst("Item 2");
                linkedList.First.Value.Should().Be("Item 2");
            };
        }
    }

    public class Describe_List : Describe_ICollection
    {
        List<string> list;

        void before_each()
        {
            list = new List<string>();
            collection = list;
        }

        void specific_actions()
        {
            Before = () => collection.Add("Item 1");

            It["an item can be referenced by index"] = () =>
                list[0].Should().Be("Item 1");
        }
    }

    public static class Describe_ICollection_output
    {
        public static string Output = @"
describe LinkedList
  adding to collection
    contains the entry
  specific actions
    can add an item at the begining with ease

describe List
  adding to collection
    contains the entry
  specific actions
    an item can be referenced by index

4 Examples, 0 Failed, 0 Pending
";
        public static int ExitCode = 0;
    }
}