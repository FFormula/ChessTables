using System.Collections.Generic;

namespace ChessTables
{
    partial class Moves
    {
        const int KingStrategy = 2;

        IEnumerable<FigureMove> NextKingMove(FigureCoord figureCoord)
        {
            switch (KingStrategy)
            {
                case 1 : return NextKingMove_Steps(figureCoord);
                case 2 : return NextKingMove_BitMask(figureCoord);
                case 3 :
                default: return NextKingMove_Array64(figureCoord);
            }
        }

        IEnumerable<FigureMove> NextKingMove_Steps(FigureCoord figureCoord)
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

        IEnumerable<FigureMove> NextKingMove_BitMask(FigureCoord figureCoord)
        {
            ulong kingMoves = AllKingMoves(figureCoord);
            kingMoves.ulong2ascii().Print();
            foreach (Coord to in Bitboard.NextCoord(kingMoves))
               yield return new FigureMove(figureCoord, to);
        }

        static ulong[] kingMovesArray = null;

        IEnumerable<FigureMove> NextKingMove_Array64(FigureCoord figureCoord)
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

        ulong AllKingMoves(FigureCoord figureCoord)
        {
            ulong king = figureCoord.coord.GetBit();
            ColorType color = figureCoord.figure.GetColor();
            return AllKingSquares(king) & ~board.GetColorBits(color);
        }

        ulong AllKingSquares(ulong king)
        {
            return noA & (king << 9 | king << 1 | king >> 7) |
                         (king << 8 | king >> 8) |
                   noH & (king << 7 | king >> 1 | king >> 9);
        }

    }
}
