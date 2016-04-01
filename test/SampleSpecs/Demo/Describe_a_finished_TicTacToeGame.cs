#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion

using NSpectator;
using FluentAssertions;

namespace SampleSpecs.Demo
{
    class Describe_a_finished_TicTacToeGame : Describe_TicTacToeGame
    {
        public string user;

        void Describe_a_draw()
        {
            context["all squares taken with no 3 in a row"] = () =>
            {
                before = () =>
                    0.To(2).Do(row =>
                        0.To(2).Do(column =>
                            game.Play(AlternateUser(), row, column)
                            )
                        );

                specify = () => game.Finished.Expected().True();
                specify = () => game.Draw.Expected().True();
            };
        }

        void Describe_a_winning_game()
        {
            players = new[] { "x", "o" };

            0.To(2).Do(index => players.Do(player =>
            {
                context["3 {0}'s in column {1}".With(player, index)] = () =>
                {
                    before = () => 0.To(2).Do(column => game.Play(player, index, column));

                    specify = () => game.Finished.Expected().True();

                    it["winner should be {0}".With(player)] = () => game.Winner.Should().Be(player);
                };

                context["3 {0}'s in row {1}".With(player, index)] = () =>
                {
                    before = () => 0.To(2).Do(row => game.Play(player, row, index));

                    specify = () => game.Finished.Expected().True();

                    it["winner should be {0}".With(player)] = () => game.Winner.Should().Be(player);
                };
            }));

            players.Do(player =>
            {
                context["3 {0}'s left to right".With(player)] = () =>
                {
                    before = () => 0.To(2).Do(index => game.Play(player, index, index));

                    specify = () => game.Finished.Expected().True();

                    it["winner should be {0}".With(player)] = () => game.Winner.Should().Be(player);
                };

                context["3 {0}'s right to left".With(player)] = () =>
                {
                    before = () =>
                    {
                        game.Play(player, 2, 0);
                        game.Play(player, 1, 1);
                        game.Play(player, 0, 2);
                    };

                    specify = () => game.Finished.Expected().True();

                    it["winner should be {0}".With(player)] = () => game.Winner.Should().Be(player);
                };
            });
        }

        private string AlternateUser()
        {
            if (user == "") return "x";

            return user = (user == "x") ? "y" : "x";
        }
    }
}