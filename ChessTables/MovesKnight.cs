using System.Collections.Generic;

namespace ChessTables
{
    class MovesKnight
    {
        const int KnightStrategy = 2;

        Bitboard board;
        FigureCoord figureCoord;

        static ulong[] knightMovesArray = null;

        public MovesKnight(Bitboard board)
        {
            this.board = board;
        }

        public IEnumerable<FigureMove> NextKnightMove(FigureCoord figureCoord)
        {
            this.figureCoord = figureCoord;
            switch (KnightStrategy)
            {
                case 1: return NextKnightMove_Steps();
                case 2: return NextKnightMove_BitMask();
                case 3:
                default: return NextKnightMove_Array64();
            }
        }

        IEnumerable<FigureMove> NextKnightMove_Steps()
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

        IEnumerable<FigureMove> NextKnightMove_BitMask()
        {
            ulong knightMoves = AllKnightMoves();
            //knightMoves.ulong2ascii().Print();
            foreach (Coord to in Bitboard.NextCoord(knightMoves))
                yield return new FigureMove(figureCoord, to);
        }

        IEnumerable<FigureMove> NextKnightMove_Array64()
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

        ulong AllKnightMoves()
        {
            ulong knight = figureCoord.coord.GetBit();
            ColorType color = figureCoord.figure.GetColor();
            return AllKnightSquares(knight) & ~board.GetColorBits(color);
        }

        ulong AllKnightSquares(ulong knight)
        {
            return Moves.nAB & (knight << 10 | knight >>  6) |
                   Moves.noA & (knight << 17 | knight >> 15) |
                   Moves.noH & (knight << 15 | knight >> 17) |
                   Moves.nGH & (knight <<  6 | knight >> 10);
        }

    }
}
