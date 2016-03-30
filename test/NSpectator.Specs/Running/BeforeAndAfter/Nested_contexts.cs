﻿#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using NUnit.Framework;

namespace NSpectator.Specs.Running.BeforeAndAfter
{
    [TestFixture]
    public class Nested_contexts : When_running_specs
    {
        class SpecClass : Sequence_spec
        {
            void a_context()
            {
                beforeAll = () => sequence = "A";
                before = () => sequence += "C";

                context["a subcontext"] = () =>
                {
                    beforeAll = () => sequence += "B";
                    before = () => sequence += "D";

                    specify = () => 1.Is(1);

                    after = () => sequence += "E";
                    afterAll = () => sequence += "G";
                };

                after = () => sequence += "F";
                afterAll = () => sequence += "H";
            }
        }

        [SetUp]
        public void Setup()
        {
            Run(typeof(SpecClass));
        }

        [Test]
        public void before_alls_at_every_level_run_before_before_eaches_from_the_outside_in()
        {
            Sequence_spec.sequence.should_start_with("ABCD");
        }

        [Test]
        public void after_alls_at_every_level_run_after_after_eaches_from_the_inside_out()
        {
            Sequence_spec.sequence.should_end_with("EFGH");
        }
    }
}