using System.Collections.Generic;

namespace ChessTables
{
    partial class Moves
    {
        IEnumerable<FigureMove> NextWhitePawnMove(FigureCoord figureCoord)
        {
            figureCoord.coord.extract(out int x, out int y);
            ulong bit = figureCoord.coord.GetBit();
            ulong owned = board.GetOwnedBits();
            foreach (FigureMove move in NextWhitePawnMove_Double(figureCoord, owned)) // e2-e4
                yield return move;
            foreach (FigureMove move in NextWhitePawnMove_Forward(figureCoord, owned)) // e2-e3
                foreach (FigureMove promove in NextWhitePawnMove_Promotion(move))
                    yield return promove;
            foreach (FigureMove move in NextWhitePawnMove_Fight(figureCoord, owned)) // ed
                foreach (FigureMove promove in NextWhitePawnMove_Promotion(move))
                    yield return promove;
        }

        // e2-e3
        IEnumerable<FigureMove> NextWhitePawnMove_Forward (FigureCoord figureCoord, ulong owned)
        {
            figureCoord.coord.extract(out int x, out int y);
            Coord to = new Coord(x, y + 1);
            if (board.IsEmpty(to))
                yield return new FigureMove(figureCoord, to);
        }

        // e2-e4
        IEnumerable<FigureMove> NextWhitePawnMove_Double(FigureCoord figureCoord, ulong owned)
        {
            figureCoord.coord.extract(out int x, out int y);
            if (y == 1)
            {
                Coord to1 = new Coord(x, 2);
                Coord to2 = new Coord(x, 3);
                if (board.IsEmpty(to1))
                    if (board.IsEmpty(to2))
                    {
                        FigureMove move = new FigureMove(figureCoord, to2);
                        move.is_enpassant = true;
                        move.enpassant = new Coord(x, 2);
                        yield return move;
                    }
            }
        }

        // ed
        IEnumerable<FigureMove> NextWhitePawnMove_Fight(FigureCoord figureCoord, ulong owned)
        {
            figureCoord.coord.extract(out int x, out int y);
            Coord to;
            if (x > 0)
            {
                to = new Coord(x - 1, y + 1);
                if (board.IsColor(to, ColorType.black))
                    yield return new FigureMove(figureCoord, to);
            }
            if (x < 7)
            {
                to = new Coord(x + 1, y + 1);
                if (board.IsColor(to, ColorType.black))
                    yield return new FigureMove(figureCoord, to);
            }
        }

        IEnumerable<FigureMove> NextWhitePawnMove_Promotion (FigureMove move)
        {
            if (move.to.xy < 56)
                yield return move;
            else
            {
                move.is_promotion = true;
                for (FigureType promo = FigureType.wKnight; promo <= FigureType.wQueen; promo++)
                {
                    move.promotion = promo;
                    yield return move;
                }
            }
        }


    }
}
