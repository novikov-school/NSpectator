#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using NSpectator;
using FluentAssertions;

namespace SampleSpecs.Compare
{
    class VendingMachineSpec : Spec
    {
        private VendingMachine machine;

        void given_new_vending_machine()
        {
            Before = () => machine = new VendingMachine();

            Specify = () => machine.Items().Should().NotBeEmpty("");

            It["getting item A1 should throw ItemNotRegistered"] = Expect<ItemNotRegisteredException>(() => machine.Item("A1"));

            Context["given doritos are registered in A1 for 50 cents"] = () =>
            {
                Before = () => machine.RegisterItem("A1", "doritos", .5m);

                Specify = () => machine.Items().Should().HaveCount(1, "");

                Specify = () => machine.Item("A1").Name.Should().Be("doritos", "");

                Specify = () => machine.Item("A1").Price.Should().Be(.5m, "");

                Context["given a second item is registered"] = () =>
                {
                    Before = () => machine.RegisterItem("A2", "mountain dew", .6m);

                    Specify = () => machine.Items().Should().HaveCount(2, empty_reason);
                };
            };
            // got to force/refactor getting rid of the dictionary soon
        }
    }

    public class ItemNotRegisteredException : Exception {}

    internal class VendingMachine
    {
        private List<Item> items;

        public VendingMachine()
        {
            items = new List<Item>();
        }

        public IEnumerable<Item> Items()
        {
            return items;
        }

        public void RegisterItem(string slot, string name, decimal price)
        {
            items.Add(new Item { Name = name, Price = price, Slot = slot });
        }

        public Item Item(string slot)
        {
            if (items.All(i => i.Slot != slot)) throw new ItemNotRegisteredException();

            return items.First();
        }
    }

    internal class Item
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Slot { get; set; }
    }
}