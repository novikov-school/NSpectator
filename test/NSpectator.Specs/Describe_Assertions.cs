#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace NSpectator.Specs
{
    // PlaceHolder for future
    public class Describe_Assertions {}

    [TestFixture]
    public class When_comparing_two_objects
    {
        [Test]
        public void Given_timespans_Should_be_able_to_assert_on_is_close_to_with_tolerance()
        {
            var thirtyMinutes = new TimeSpan(0, 30, 0);
            var twentyMinutes = new TimeSpan(0, 20, 0);
            var tenMinuteTolerance = new TimeSpan(0, 10, 0);

            thirtyMinutes.ShouldBeCloseTo(twentyMinutes, tenMinuteTolerance);
        }

        [Test]
        public void Given_datetimes_Should_be_able_to_assert_on_is_close_to_with_tolerance()
        {
            var arg1 = new DateTime(2012, 2, 1); // {2/1/2012}
            var arg2 = new DateTime(2012, 2, 2); // {2/2/2012}
            var oneDayTolerance = new DateTime(TimeSpan.TicksPerDay);

            arg1.ShouldBeCloseTo(arg2, oneDayTolerance);
        }

        [Test]
        public void Should_be_able_to_assert_on_greater_than()
        {
            2.Should().BeGreaterThan(1);
        }

        [Test]
        public void Should_be_able_to_assert_on_greater_or_equal_to()
        {
            2.Should().BeGreaterOrEqualTo(1);
        }

        [Test]
        public void Should_be_able_to_assert_on_less_than()
        {
            2.Should().BeLessThan(3);
        }

        [Test]
        public void Should_be_able_to_assert_on_less_or_equal_to()
        {
            2.Should().BeLessOrEqualTo(3);
        }

        [Test]
        public void given_floats_Should_be_able_to_assert_on_is_close_to_with_default_tolerance()
        {
            1e-9f.Should().BeCloseTo(1e-9f);
        }

        [Test]
        public void given_floats_Should_be_able_to_assert_on_is_close_to_with_custom_tolerance()
        {
            200f.Should().BeCloseTo(300f, 100f);
        }

        [Test]
        public void given_doubles_Should_be_able_to_assert_on_is_close_to_with_default_tolerance()
        {
            1e-9.Should().BeCloseTo(1e-9);
        }

        [Test]
        public void given_doubles_Should_be_able_to_assert_on_is_close_to_with_custom_tolerance()
        {
            200.0.Should().BeCloseTo(200.5, .5);
        }

		[Test]
		public void given_an_empty_string_it_Should_be_empty()
		{
			"".Should().BeEmpty();
			((string)null).Should().BeNullOrEmpty();
		}

		[Test]
		public void given_a_nonempty_string_it_Should_not_be_empty()
		{
			"a".Should().NotBeEmpty();
		}

		[Test]
		public void given_a_null_class_instance_it_Should_be_null()
		{
			((string)null).Should().BeNull();
			((List<int>)null).Should().BeNull();
		}

		[Test]
		public void given_a_non_null_class_instance_it_Should_not_be_null()
		{
			((string)"").Should().NotBeNull();
			(new List<int>()).Should().NotBeNull();
		}

		[Test]
		public void given_a_null_nullable_struct_it_Should_be_null()
		{
			int? i = null;
			decimal? j = null;
			i.Should().NotHaveValue();
			j.Should().NotHaveValue();
		}

		[Test]
		public void given_a_null_nullable_struct_it_Should_not_be_null()
		{
			int? i = 2;
			decimal? j = 3;
		    i.Should().HaveValue();
		    j.Should().HaveValue();
		}
    }
}
