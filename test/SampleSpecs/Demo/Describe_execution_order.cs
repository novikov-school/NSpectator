#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion
using System;
using NSpectator;

namespace SampleSpecs.Demo
{
    // this can be either an abstract class, or just a class
    abstract class Parent_class : Spec
    {
        public const int indentSize = 3;
        public static string order = "\n\n";
        public static int indent = 0;

        void Print(string s)
        {
            Console.WriteLine(s);
        }

        protected void Increase(string s)
        {
            Write(s);
            indent += indentSize;
        }

        protected void Decrease(string s)
        {
            indent -= indentSize;
            Write(s);
        }

        protected void Write(string s)
        {
            order += s.PadLeft(s.Length + indent);
        }

        protected void WriteLine(string s)
        {
            Write(s + "\n");
        }

        void before_all()
        {
            Increase("parent: before all\n");
        }

        void before_each()
        {
            WriteLine("parent: before each");
        }

        void it_works_parent_1()
        {
            WriteLine("parent: it works 1");
        }

        void it_works_parent_2()
        {
            WriteLine("parent: it works 2");
        }

        void after_each()
        {
            WriteLine("parent: after each\n");
        }

        void after_all()
        {
            Decrease("parent: after all\n");
            Print(order);
        }
    }

    class Child_class : Parent_class
    {
        void before_all()
        {
            Increase("child: before all\n");
        }

        void before_each()
        {
            Write("child: before each\n");
        }

        void it_works_child_3()
        {
            Write("child: it works 3\n");
        }

        void it_works_child_4()
        {
            Write("child: it works 4\n");
        }

        void after_each()
        {
            Write("child: after each\n");
        }

        void after_all()
        {
            Decrease("child: after all\n");
        }

        void method_level_context()
        {
            beforeAll = () => Increase("method: before all\n");

            before = () => WriteLine("method: before each");

            it["it works method 5"] = () => WriteLine("method: it works 5");

            it["it works method 6"] = () => WriteLine("method: it works 6");

            after = () => WriteLine("method: after each");

            afterAll = () => Decrease("method: after all\n");

            context["sub context"] = () =>
            {
                beforeAll = () => Increase("sub: before all\n");

                before = () => WriteLine("sub: before each");

                it["it works sub 7"] = () => WriteLine("sub: it works 7 ");

                it["it works sub 8"] = () => WriteLine("sub: it works 8 ");

                after = () => WriteLine("sub: after each");

                afterAll = () => Decrease("sub: after all\n");
            };
        }
    }
}