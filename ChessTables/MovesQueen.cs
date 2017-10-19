using System.Collections.Generic;

namespace ChessTables
{
    partial class Moves
    {
        IEnumerable<FigureMove> NextQueenMove(FigureCoord figureCoord)
        {
            return NextQueenMove_BitMask(figureCoord);
        }

        IEnumerable<FigureMove> NextQueenMove_BitMask(FigureCoord figureCoord)
        {
            ulong bishopMoves = AllQueenMoves(figureCoord);
            bishopMoves.ulong2ascii().Print();
            foreach (Coord to in Bitboard.NextCoord(bishopMoves))
                yield return new FigureMove(figureCoord, to);
        }

        ulong AllQueenMoves(FigureCoord figureCoord)
        {
            ulong queen = figureCoord.coord.GetBit();
            ColorType color = figureCoord.figure.GetColor();
            ulong stops = board.GetOwnedBits();
            return
                (Slide1(queen, stops) |
                 Slide2(queen, stops) |
                 Slide3(queen, stops) |
                 Slide4(queen, stops) |
                 Slide6(queen, stops) |
                 Slide7(queen, stops) |
                 Slide8(queen, stops) |
                 Slide9(queen, stops)) & ~board.GetColorBits(color);
        }
    }
}
