using System.Collections.Generic;

namespace ChessTables
{
    class MovesKing
    {
        const int KingStrategy = 2;

        Bitboard board;
        FigureCoord figureCoord;

        static ulong[] kingMovesArray = null;

        public MovesKing (Bitboard board)
        {
            this.board = board;
        }

        public IEnumerable<FigureMove> NextKingMove(FigureCoord figureCoord)
        {
            this.figureCoord = figureCoord;

            switch (KingStrategy)
            {
                case 1 : return NextKingMove_Steps();
                case 2 : return NextKingMove_BitMask();
                case 3 :
                default: return NextKingMove_Array64();
            }
        }

        IEnumerable<FigureMove> NextKingMove_Steps()
        {
            FigureMove figureMove = new FigureMove(figureCoord);
            ColorType otherColor = figureCoord.figure.GetColor().Swap();
            int[,] KingSteps =
            {
                { -1, -1 }, { -1,  0}, { -1, +1 },
                {  0, -1 },            {  0, +1 },
                { +1, -1 }, { +1,  0}, { +1, +1 }
            };
            for (int j = 0; j < 8; j++)
            {
                if (figureCoord.coord.Shift(KingSteps[j, 0],
                                       KingSteps[j, 1],
                                   out figureMove.to))
                {
                    if (board.IsEmpty(figureMove.to))
                        yield return figureMove;
                    else if (board.IsColor(figureMove.to, otherColor))
                        yield return figureMove;
                }
            }
        }

        IEnumerable<FigureMove> NextKingMove_BitMask()
        {
            ulong kingMoves = AllKingMoves();
            //kingMoves.ULong2Ascii().Print();
            foreach (Coord to in Bitboard.NextCoord(kingMoves))
               yield return new FigureMove(figureCoord, to);
        }

        IEnumerable<FigureMove> NextKingMove_Array64()
        {
            if (kingMovesArray == null)
            {
                kingMovesArray = new ulong[64];
                for (int j = 0; j < 64; j++)
                    kingMovesArray[j] = AllKingSquares(1UL << j);
            }
            ulong kingMoves = kingMovesArray [figureCoord.coord.xy];

            foreach (Coord to in Bitboard.NextCoord(kingMoves))
                yield return new FigureMove(figureCoord, to);
        }

        ulong AllKingMoves()
        {
            ulong king = figureCoord.coord.GetBit();
            ColorType color = figureCoord.figure.GetColor();
            return AllKingSquares(king) & ~board.GetColorBits(color);
        }

        ulong AllKingSquares(ulong king)
        {
            return Moves.noA & (king << 9 | king << 1 | king >> 7) |
                               (king << 8 | king >> 8) |
                   Moves.noH & (king << 7 | king >> 1 | king >> 9);
        }

    }
}
