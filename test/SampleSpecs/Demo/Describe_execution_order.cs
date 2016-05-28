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
            BeforeAll = () => Increase("method: before all\n");

            Before = () => WriteLine("method: before each");

            It["it works method 5"] = () => WriteLine("method: it works 5");

            It["it works method 6"] = () => WriteLine("method: it works 6");

            After = () => WriteLine("method: after each");

            AfterAll = () => Decrease("method: after all\n");

            Context["sub context"] = () =>
            {
                BeforeAll = () => Increase("sub: before all\n");

                Before = () => WriteLine("sub: before each");

                It["it works sub 7"] = () => WriteLine("sub: it works 7 ");

                It["it works sub 8"] = () => WriteLine("sub: it works 8 ");

                After = () => WriteLine("sub: after each");

                AfterAll = () => Decrease("sub: after all\n");
            };
        }
    }
}