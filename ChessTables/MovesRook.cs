using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTables
{
    partial class Moves
    {
        const int RookStrategy = 2;
        const ulong onA = 0x0101010101010101;

        IEnumerable<FigureMove> NextRookMove(FigureCoord figureCoord)
        {
            switch (KingStrategy)
            {
                case 1: return NextRookMove_Steps(figureCoord);
                case 2:
               default: return NextRookMove_BitMask(figureCoord);
            }
        }

        IEnumerable<FigureMove> NextRookMove_Steps (FigureCoord figureCoord)
        {
            return null;
        }

        IEnumerable<FigureMove> NextRookMove_BitMask(FigureCoord figureCoord)
        {
            ulong rookMoves = AllRookMoves(figureCoord);
            rookMoves.ulong2ascii().Print();
            foreach (Coord to in Bitboard.NextCoord(rookMoves))
                yield return new FigureMove(figureCoord, to);
        }

        ulong AllRookMoves(FigureCoord figureCoord)
        {
            ulong rook = figureCoord.coord.GetBit();
            ColorType color = figureCoord.figure.GetColor();
            ulong stops = board.GetOwnedBits();
            return
                (Slide6(rook, stops) |
                 Slide4(rook, stops) |
                 Slide8(rook, stops) |
                 Slide2(rook, stops)) & ~board.GetColorBits(color);
        }

        ulong Slide6(ulong coord, ulong stops) => Slide(coord, +1, noA, stops);
        ulong Slide4(ulong coord, ulong stops) => Slide(coord, -1, noH, stops);
        ulong Slide8(ulong coord, ulong stops) => Slide(coord, +8, ALL, stops);
        ulong Slide2(ulong coord, ulong stops) => Slide(coord, -8, ALL, stops);

        ulong Slide (ulong coord, int shift, ulong range, ulong stops)
        {
            ulong map = 0UL;
            while (true)
            {
                if (shift > 0)  coord <<= +shift;
                else            coord >>= -shift;
                if (0 == (coord & range)) break;
                map |= coord;
                if (0 != (coord & stops)) break;
            }
            return map;
        }

    }
}
