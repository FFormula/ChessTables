﻿using System;

namespace ChessTables
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Program program = new Program();
            program.Start();
            Console.ReadKey();
        }

        void Start ()
        {
            string fen;
            fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
            //fen = "r1bqkb1r/pppp1ppp/2n2n2/4p3/4P3/2N2N2/PPPP1PPP/R1BQKB1R";
            //fen = "8/8/8/K7/8/8/8/8";
            //fen = "8/6K1/8/8/1N1Q5/8/3B4/R7";
            //fen = "k6b/p3pNP1/5P2/3n1R2/2ppPp1n/1b1P1p1p/PPP3P1/7K";
            Bitboard board = new Bitboard(fen);
            board.Board2Ascii().Print();

            Moves moves = new Moves(board);
            int nr = 0;
            while (true)
            {
                foreach (FigureMove move in moves.NextWhiteFigureMove())
                {
                    Console.WriteLine((++nr).ToString() + ". " + move.Print());
                    board.MakeMove(move);
                    board.Board2Ascii().Print();
                    break;
                }
                Console.ReadKey();
                foreach (FigureMove move in moves.NextBlackFigureMove())
                {
                    Console.WriteLine((++nr).ToString() + ". " + move.Print());
                    board.MakeMove(move);
                    board.Board2Ascii().Print();
                    break;
                }
                Console.ReadKey();
            }
        }
    }
}
