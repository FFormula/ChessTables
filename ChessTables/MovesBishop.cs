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

        ulong Slide1(ulong coord, ulong stops) => Slide(coord, -9, noH, stops);
        ulong Slide3(ulong coord, ulong stops) => Slide(coord, -7, noA, stops);
        ulong Slide7(ulong coord, ulong stops) => Slide(coord, +7, noH, stops);
        ulong Slide9(ulong coord, ulong stops) => Slide(coord, +9, noA, stops);

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
