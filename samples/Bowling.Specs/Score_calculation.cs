#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NSpectator;
using NUnit.Framework;
using FluentAssertions;
using NSpectator.Domain;

namespace Bowling.Specs
{
    public class Score_calculation
    {
        class Describe_steps : Spec
        {
            Game _game;

            Action StartGame => () => _game = new Game();

            void TotalScoreShouldBe(int score)
            {
                _game.Score.Should().Be(score);
            }

            void ExpectedScore(int expected)
            {
                It[$"my total score should be {expected}"] = () => TotalScoreShouldBe(expected);
            }

            string FormatTimes(int times)
            {
                if (times > 1) return $" for {times} times";
                return string.Empty;
            }

            void Roll(string series, int times = 1)
            {
                int[] pins = series.Trim().Split(',').Select(int.Parse).ToArray();
                times.Times(() => Roll(pins));
            }

            void Roll(params int[] pins)
            {
                foreach (var pin in pins)
                {
                    _game.Roll(pin);
                }
            }

            void Scenario_gutter_game()
            {
                BeforeAll = StartGame;

                When["all of my balls are landing in the gutter"] = () =>
                {
                    int k = 0;
                    for (int i = 0; i < 20; i++)
                    {
                        _game.Roll(0);
                        k = k + i*2;
                    }
                };

                ExpectedScore(0);
            }

            void Scenario_beginners_game()
            {
                BeforeAll = StartGame;

                new Each<string, int>()
                {   
                    { "2, 7", 1 },
                    { "3, 4", 1 },
                    { "1, 1", 8 }
                }.Do((series, times) => When[$"I roll {series}{FormatTimes(times)}"] = () => Roll(series, times));

                ExpectedScore(32);
            }

            void Scenario_another_beginners_game()
            {
                BeforeAll = StartGame;

                // Given
                new Each<string, int>()
                {
                    { "2,7,3,4,1,1,5,1,1,1,1,1,1,1,1,1,1,1,5,1", 1 }
                }
                // When
                .Do((series, times) => When[$"I roll following series: {series}"] = () => Roll(series, times));
                
                // Then
                ExpectedScore(40);
            }

            void Scenario_all_strikes()
            {
                BeforeAll = StartGame;

                When["all of my rolls are strikes"] = () =>
                {
                    12.Times(() => _game.Roll(10));
                };

                ExpectedScore(300);
            }

            void Scenario_one_single_spare()
            {
                BeforeAll = StartGame;

                new Each<string, int>()
                {
                    { "2,8,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1", 1 }
                }
                // When
                .Do((series, times) => When[$"I roll following series: {series}"] = () => Roll(series, times));
                
                ExpectedScore(29);
            }

            void Scenario_all_spares()
            {
                BeforeAll = StartGame;

                new Each<string, int>()
                {
                    { "1, 9", 10 }
                }.Do((series, times) => When[$"I roll following series: {series}{FormatTimes(times)}"] = () => Roll(series, times));

                When["I roll 1"] = () => Roll(1);

                ExpectedScore(110);
            }

            // helper dialect property
            ActionRegister When => new ActionRegister((name, tags, action) => It[$"when {name}"] = action);
        }
    }
}
