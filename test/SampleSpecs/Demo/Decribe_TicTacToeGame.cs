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
        protected TicTacToGame game;

        protected string[] players;

        void before_each()
        {
            game = new TicTacToGame();

            players = game.Players;
        }

        void When_players_try_to_take_the_same_square()
        {
            It["should throw exception"] = Expect<InvalidOperationException>(() =>
                players.Each(player => game.Play(player, 0, 0))
                );
        }
    }
}