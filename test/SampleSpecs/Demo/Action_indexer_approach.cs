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
    class Action_indexer_approach : Spec
    {
        User user;

        void a_user()
        {
            before = () => user = new User();

            specify = () => user.Id.Should().NotBeDefault("because it is initialized");

            context["user is admin"] = () =>
            {
                before = () => user.Admin = true;

                specify = () => user.Admin.should_be_true();

                context["user is terminated"] = () =>
                {
                    before = () => user.Terminated = true;

                    specify = () => user.Terminated.should_be_true();
                };
            };

            specify = () => user.Admin.Expected().True();

            it["should work"] = () => { };

            //soon.user_should_not_have_default_password();
        }
    }

    //output from above
    //given a_user
    //    user Id should_not_be_default
    //    user Admin should_be_false
    //    when user is admin
    //        user Admin should_be_true
    //        when user is terminated
    //            user Terminated should_be_true
}