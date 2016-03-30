#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using FluentAssertions;

namespace NSpectator.Specs.ClassContextBug
{
    class Grand_Parent : Spec
    {
        public string TestValue;

        void before_each()
        {
            this.TestValue = "Grand Parent";
        }

        void act_each()
        {
            this.TestValue = this.TestValue + "!!!";
        }

        void Grand_Parent_Context()
        {
            it["TestValue should be \"Grand Parent!!!\""] = () => TestValue.Should().Be("Grand Parent!!!");
        }
    }

    class Parent : Grand_Parent
    {
        void before_each()
        {
            this.TestValue += "." + "Parent";
        }

        void act_each()
        {
            this.TestValue = this.TestValue + "@@@";
        }

        void Parent_Context()
        {
            it["TestValue should be \"Grand Parent.Parent!!!@@@\""] = () => TestValue.Should().Be("Grand Parent.Parent!!!@@@");
        }
    }

    class Child : Parent
    {
        void before_each()
        {
            this.TestValue += "." + "Child";
        }

        void act_each()
        {
            this.TestValue = this.TestValue + "###";
        }

        void Child_Context()
        {
            it["TestValue should be \"Grand Parent.Parent.Child!!!@@@###\""] = () => TestValue.Should().Be("Grand Parent.Parent.Child!!!@@@###");
        }
    }
}