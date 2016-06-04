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
            Before = () => user = new User();

            Specify = () => user.Id.Should().NotBeDefault("because it is initialized");

            Context["user is admin"] = () =>
            {
                Before = () => user.Admin = true;

                Specify = () => user.Admin.Expected().True();

                Context["user is terminated"] = () =>
                {
                    Before = () => user.Terminated = true;

                    Specify = () => user.Terminated.Expected().True();
                };
            };

            Specify = () => user.Admin.Expected().True();

            It["should work"] = () => { };

            // soon.user_should_not_have_default_password();
        }
    }

    // output from above
    // given a_user
    //     user Id should_not_be_default
    //     user Admin should_be_false
    //     when user is admin
    //         user Admin should_be_true
    //         when user is terminated
    //             user Terminated should_be_true
}