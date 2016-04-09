using System;
using System.Collections.Generic;
using System.Linq;
using NSpectator;
// ReSharper disable ArrangeTypeMemberModifiers

namespace SampleSpecs.Model
{
    public class TicTacToGame
    {
        public TicTacToGame()
        {
            Board = new string[3, 3]
            {
                { "", "", "" },
                { "", "", "" },
                { "", "", "" }
            };
        }

        public string[] Players => new [] { "x", "o" };

        public bool Finished => !string.IsNullOrEmpty(Winner) && !Draw;

        public string[,] Board { get; set; }

        public bool Draw { get; private set; }

        public string Winner { get; private set; }

        public void Play(string xo, int row, int column)
        {
            if (string.IsNullOrEmpty(Board[row, column]) == false)
                throw new InvalidOperationException();

            Board[row, column] = xo;
            TestDone();
        }

        private void TestDone()
        {
            1.To(3, i => Winner = Winner ?? Players.FirstOrDefault(player => CheckStraightColumn(i, player) || CheckStraightRow(i, player)));
            
            Players.Each(player => 
            {
                if (CheckDiagonalLeft(player) || CheckDiagonalRight(player))
                    Winner = player;
            });
            
            CheckAllSquaresTaken();
        }

        bool CheckStraightColumn(int column, string xo) => Board[column, 0] == xo && Board[column, 1] == xo && Board[column, 2] == xo;

        bool CheckStraightRow(int row, string xo) => Board[0, row] == xo && Board[1, row] == xo && Board[2, row] == xo;
        
        bool CheckDiagonalLeft(string xo) => Board[0, 0] == xo && Board[1, 1] == xo && Board[2, 2] == xo;
        
        bool CheckDiagonalRight(string xo) => Board[2, 0] == xo && Board[1, 1] == xo && Board[0, 2] == xo;

        bool CheckAllSquaresTaken()
        {
            var val = 
                from string xo
                in Board
                where xo == string.Empty
                select xo;

            if (val.Any()) return false;
            
            if (string.IsNullOrEmpty(Winner)) Draw = true;
            return true;
        }
    }
}