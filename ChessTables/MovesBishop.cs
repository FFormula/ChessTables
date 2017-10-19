using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTables
{
    partial class Moves
    {
        IEnumerable<FigureMove> NextBishopMove(FigureCoord figureCoord)
        {
            return NextBishopMove_BitMask(figureCoord);
        }

        IEnumerable<FigureMove> NextBishopMove_BitMask(FigureCoord figureCoord)
        {
            ulong bishopMoves = AllBishopMoves(figureCoord);
            bishopMoves.ulong2ascii().Print();
            foreach (Coord to in Bitboard.NextCoord(bishopMoves))
                yield return new FigureMove(figureCoord, to);
        }

        ulong AllBishopMoves(FigureCoord figureCoord)
        {
            ulong bishop = figureCoord.coord.GetBit();
            ColorType color = figureCoord.figure.GetColor();
            ulong stops = board.GetOwnedBits();
            return
                (Slide1(bishop, stops) |
                 Slide3(bishop, stops) |
                 Slide7(bishop, stops) |
                 Slide9(bishop, stops)) & ~board.GetColorBits(color);
        }

    }
}
