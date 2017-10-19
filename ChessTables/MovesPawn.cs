using System.Collections.Generic;

namespace ChessTables
{
    class MovesPawn
    {
        // white/black params
        int y1 = 1;
        int y2 = 2;
        int y3 = 3;
        int up = +1;
        int yPromo;
        ColorType fightColor = ColorType.black;

        ulong bit;
        int x, y;
        ulong owned;
        Bitboard board;
        FigureCoord figureCoord;

        public MovesPawn (Bitboard board)
        {
            this.board = board;
        }

        void InitWhite ()
        {
            y1 = 1;
            y2 = 2;
            y3 = 3;
            yPromo = 6;
            up = +1;
            fightColor = ColorType.black;
        }

        void InitBlack ()
        {
            y1 = 6;
            y2 = 5;
            y3 = 4;
            yPromo = 1;
            up = -1;
            fightColor = ColorType.white;
        }

        public IEnumerable<FigureMove> NextPawnMove(FigureCoord figureCoord)
        {
            if (figureCoord.figure.GetColor() == ColorType.white)
                InitWhite();
            else
                InitBlack();
            bit = figureCoord.coord.GetBit();
            figureCoord.coord.extract(out x, out y);
            owned = board.GetOwnedBits();
            this.figureCoord = figureCoord;

            foreach (FigureMove move in NextPawnMove_Double()) // e2-e4
                yield return move;
            foreach (FigureMove move in NextPawnMove_Forward()) // e2-e3
                foreach (FigureMove promove in NextPawnMove_Promotion(move))
                    yield return promove;
            foreach (FigureMove move in NextPawnMove_Fight()) // ed
                foreach (FigureMove promove in NextPawnMove_Promotion(move))
                    yield return promove;
        }

        // e2-e3
        IEnumerable<FigureMove> NextPawnMove_Forward ()
        {
            Coord to = new Coord(x, y + up);
            if (board.IsEmpty(to))
                yield return new FigureMove(figureCoord, to);
        }

        // e2-e4
        IEnumerable<FigureMove> NextPawnMove_Double()
        {
            if (y == y1)
            {
                Coord to1 = new Coord(x, y2);
                Coord to2 = new Coord(x, y3);
                if (board.IsEmpty(to1))
                    if (board.IsEmpty(to2))
                    {
                        FigureMove move = new FigureMove(figureCoord, to2);
                        move.is_enpassant = true;
                        move.enpassant = new Coord(x, y2);
                        yield return move;
                    }
            }
        }

        // ed
        IEnumerable<FigureMove> NextPawnMove_Fight()
        {
            Coord to;
            if (x > 0)
            {
                to = new Coord(x - 1, y + up);
                if (board.IsColor(to, fightColor))
                    yield return new FigureMove(figureCoord, to);
            }
            if (x < 7)
            {
                to = new Coord(x + 1, y + up);
                if (board.IsColor(to, fightColor))
                    yield return new FigureMove(figureCoord, to);
            }
        }

        IEnumerable<FigureMove> NextPawnMove_Promotion (FigureMove move)
        {
            if (y == yPromo)
            {
                move.is_promotion = true;
                for (FigureType promo = FigureType.wKnight; promo <= FigureType.wQueen; promo++)
                {
                    move.promotion = promo;
                    yield return move;
                }
            } else
                yield return move;
        }


    }
}
