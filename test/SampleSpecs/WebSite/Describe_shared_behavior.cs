using System.Collections.Generic;
using NSpectator;

public abstract class Describe_ICollection : Spec
{
    protected ICollection<string> collection;

    void adding_to_collection()
    {
        before = () => collection.Add("Item 1");

        it["contains the entry"] = () =>
            collection.Contains("Item 1").should_be(true);
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
        before = () => collection.Add("Item 1");

        it["can add an item at the begining with ease"] = () =>
        {
            linkedList.AddFirst("Item 2");
            linkedList.First.Value.should_be("Item 2");
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
        before = () => collection.Add("Item 1");

        it["an item can be referenced by index"] = () =>
            list[0].should_be("Item 1");
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