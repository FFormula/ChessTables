using System.Collections.Generic;

namespace ChessTables
{
    partial class Moves
    {
        const int KnightStrategy = 2;

        public const ulong noA = 0xFeFeFeFeFeFeFeFe;
        public const ulong nAB = 0xFcFcFcFcFcFcFcFc;
        public const ulong noH = 0x7f7f7f7f7f7f7f7f;
        public const ulong nGH = 0x3f3f3f3f3f3f3f3f;
        public const ulong ALL = 0xFFFFFFFFFFFFFFFF;

        IEnumerable<FigureMove> NextKnightMove(FigureCoord figureCoord)
        {
            switch (KnightStrategy)
            {
                case 1: return NextKnightMove_Steps(figureCoord);
                case 2: return NextKnightMove_BitMask(figureCoord);
                case 3:
                default: return NextKnightMove_Array64(figureCoord);
            }
        }

        IEnumerable<FigureMove> NextKnightMove_Steps(FigureCoord figureCoord)
        {
            ColorType other = figureCoord.figure.GetColor().Swap();
            int[,] KnightSteps =
            {
                            { -1, -2 }, { +1, -2},
                { -2, -1 },                        { +2, -1},
                { -2, +1 },                        { +2, +1},
                            { -1, +2 }, { +1, +2}
            };
            FigureMove figureMove = new FigureMove(figureCoord);
            for (int j = 0; j < 8; j++)
            {
                if (figureCoord.coord.Shift(KnightSteps[j, 0],
                                       KnightSteps[j, 1],
                                   out figureMove.to))
                {
                    if (board.IsEmpty(figureMove.to))
                        yield return figureMove;
                    else if (board.IsColor(figureMove.to, other))
                        yield return figureMove;
                }
            }
        }

        IEnumerable<FigureMove> NextKnightMove_BitMask(FigureCoord figureCoord)
        {
            ulong knightMoves = AllKnightMoves(figureCoord);
            knightMoves.ulong2ascii().Print();
            foreach (Coord to in Bitboard.NextCoord(knightMoves))
                yield return new FigureMove(figureCoord, to);
        }

        static ulong[] knightMovesArray = null;

        IEnumerable<FigureMove> NextKnightMove_Array64(FigureCoord figureCoord)
        {
            if (knightMovesArray == null)
            {
                knightMovesArray = new ulong[64];
                for (int j = 0; j < 64; j++)
                    knightMovesArray[j] = AllKnightSquares(1UL << j);
            }
            ulong knightMoves = knightMovesArray[figureCoord.coord.xy];
            //knightMoves.ulong2ascii().Print();
            foreach (Coord to in Bitboard.NextCoord(knightMoves))
                yield return new FigureMove(figureCoord, to);

        }

        ulong AllKnightMoves(FigureCoord figureCoord)
        {
            ulong knight = figureCoord.coord.GetBit();
            ColorType color = figureCoord.figure.GetColor();
            return AllKnightSquares(knight) & ~board.GetColorBits(color);
        }

        ulong AllKnightSquares(ulong knight)
        {
            return nAB & (knight << 10 | knight >>  6) |
                   noA & (knight << 17 | knight >> 15) |
                   noH & (knight << 15 | knight >> 17) |
                   nGH & (knight <<  6 | knight >> 10);
        }

    }
}
