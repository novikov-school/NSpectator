#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System;
using NSpectator;
using SampleSpecs.Model;

namespace SampleSpecs.Demo
{
    class Describe_TicTacToeGame : Spec
    {
        void before_each()
        {
            game = new TicTacToGame();

            players = new[] { "x", "o" };
        }

        void When_players_try_to_take_the_same_square()
        {
            it["should throw exception"] = expect<InvalidOperationException>(() =>
                players.Do(player => game.Play(player, 0, 0))
                );
        }

        protected TicTacToGame game;
        protected string[] players;
    }
}
