using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTables
{
    partial class Moves
    {
        IEnumerable<FigureMove> NextWhitePawnMove(FigureCoord figureCoord)
        {
            figureCoord.coord.extract(out int x, out int y);
            ulong bit = figureCoord.coord.GetBit();
            ulong owned = board.GetOwnedBits();
            if (0 == (owned & (bit << 8)))
                if (y < 6)
                    yield return new FigureMove(figureCoord, new Coord((byte)(figureCoord.coord.xy + 8)));
                else
                    for (FigureType promo = FigureType.wKnight; promo <= FigureType.wQueen; promo ++)
                    {
                        FigureMove move = new FigureMove(figureCoord, new Coord((byte)(figureCoord.coord.xy + 8)));
                        move.is_promotion = true;
                        move.promotion = promo;
                        yield return move;
                    }
            if (y == 1)
                if (0 == (owned & (bit << 8)))
                    if (0 == (owned & (bit << 16)))
                    {
                        FigureMove move = new FigureMove(figureCoord, new Coord((byte)(figureCoord.coord.xy + 16)));
                        move.is_enpassant = true;
                        move.enpassant = new Coord(x, y + 1);
                    }


        }

        IEnumerable<FigureMove> NextBlackPawnMove(FigureCoord figureCoord)
        {
            yield break;
        }

        

    }
}
