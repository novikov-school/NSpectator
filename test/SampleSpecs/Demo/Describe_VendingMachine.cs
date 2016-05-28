#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion

using NSpectator;
using SampleSpecs.Model;
using FluentAssertions;

namespace SampleSpecs.Demo
{
    class Describe_VendingMachine : Spec
    {
        VendingMachine vendingMachine = null;

        void before_each()
        {
            vendingMachine = new VendingMachine();
        }

        void when_stocking_vending_machine_with_chips()
        {
            Act = () => vendingMachine.AddInventory("chips");

            It["should contain chips with count of 1"] = () => vendingMachine.Inventory("chips").Should().Be(1);

            Context["multiple chips added"] = () =>
            {
                Act = () => vendingMachine.AddInventory("chips");

                It["should increment chip inventory with count of 2"] = () => vendingMachine.Inventory("chips").Should().Be(2);
            };
        }

        void when_buying_an_item()
        {
            Context["vending maching has inventory"] = () =>
            {
                Before = () =>
                {
                    vendingMachine.AddInventory("chips");
                    vendingMachine.PricePoint("chips", .5);
                };

                Act = () => vendingMachine.Buy("chips");

                It["should decrement inventory"] = () => vendingMachine.Inventory("chips").Should().Be(0);

                It["should increment cash in machine"] = () => vendingMachine.Cash.Should().Be(.5);
            };
        }
    }
}