using System;
using System.Collections.Generic;

namespace ChessTables
{
    public class Bitboard
    {
        public ulong [] bits;
        const string figures = "PNBRQKpnbrqk";

        public Bitboard ()
        {
            bits = new ulong [Enum.GetValues(typeof(FigureType)).Length];
        }

        public Bitboard(string fen)
            : this ()
        {
            SetFEN(fen);
        }

        public void SetFEN(string fen)
        {
            bits = new ulong[12];
            byte x = 0;
            byte y = 7;
            int p;
            ulong one = (ulong)1;
            foreach (char c in fen)
            {
                if (c == '/')
                {
                    x = 0;
                    y--;
                }
                else
                if (c >= '1' && c <= '8')
                    x += (byte)(c - '0');
                else
                if (0 <= (p = figures.IndexOf(c)))
                    bits[p] |= one << ((y << 3) + x++);
                else
                    break;
            }
        }

        public IEnumerable<FigureCoord> NextWhiteFigureCoord => 
            NextFigureCoord(FigureType.wPawn, FigureType.wKing);

        public IEnumerable<FigureCoord> NextBlackFigureCoord => 
            NextFigureCoord(FigureType.bPawn, FigureType.bKing);

        IEnumerable<FigureCoord> NextFigureCoord (FigureType from, FigureType till)
        {
            for (FigureType figure = from; figure <= till; figure++)
                foreach (Coord start in NextCoord(bits[(int)figure]))
                    yield return new FigureCoord(figure, start);
        }

        static public IEnumerable<Coord> NextCoord (ulong map)
        {
            while (map != 0)
            {
                yield return new Coord(map & ~(map - 1));
                map &= map - 1;
            }
//            ulong bit = 1;
//            for (byte xy = 0; xy < 64; xy ++)
//            {
//                if (0 != (map & bit))
//                    yield return new Coord (xy);
//                bit <<= 1;
//            }
        }

        public bool IsEmpty (Coord coord)
        {
            ulong bit = coord.GetBit();
            for (int j = 0; j < 12; j++)
                if (0 != (bits[j] & bit))
                    return false;
            return true;
        }

        public ulong GetOwnedBits ()
        {
            ulong all = 0;
            for (int j = 0; j < 12; j++)
                all |= bits[j];
            return all;
        }

        public bool IsColor (Coord coord, ColorType color)
        {
            ulong bit = coord.GetBit();
            if (color == ColorType.white)
                for (int j = 0; j < 6; j++)
                    if (0 != (bits[j] & bit))
                        return true;
            if (color == ColorType.black)
                for (int j = 6; j < 12; j++)
                    if (0 != (bits[j] & bit))
                        return true;
            return false;
        }

        public ulong GetColorBits(ColorType color)
        {
            ulong all = 0;
            if (color == ColorType.white)
                for (int j = 0; j < 6; j++)
                    all |= bits[j];
            if (color == ColorType.black)
                for (int j = 6; j < 12; j++)
                    all |= bits[j];
            return all;
        }

        public char GetSquare (Coord coord)
        {
            ulong bit = coord.GetBit();
            for (int j = 0; j < 12; j++)
                if (0 != (bits[j] & bit))
                    return figures[j];
            return '.';
        }
    }
}
