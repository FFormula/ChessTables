using System;

namespace ChessTables
{
    public struct Coord
    {
        public byte xy;

        public Coord(byte xy)
        {
            this.xy = xy;
        }

        public Coord(int x, int y)
        {
            this.xy = (byte)(x + (y << 3));
        }

        public Coord(ulong bit)
//        byte GetLastBitIndex(ulong bit)
        {
            byte n = 0;
            if ((bit & 0xFFFFFFFF) == 0) { n = 32; bit >>= 32; }
            if ((bit & 0xFFFF) == 0) { n += 16; bit >>= 16; }
            if ((bit & 0xFF) == 0) { n += 8; bit >>= 8; }
            if ((bit & 0xF) == 0) { n += 4; bit >>= 4; }
            if ((bit & 3) == 0) { n += 2; bit >>= 2; }
            if ((bit & 1) == 0) { ++n; }
            xy = n;
        }

        public string Print()
        {
            extract(out int x, out int y);
            return ((char)('a' + x)).ToString() +
                   ((char)('1' + y)).ToString();
        }

        public void extract(out int x, out int y)
        {
            x = xy & 7;
            y = xy >> 3;
        }

        public bool Shift(int sx, int sy, out Coord newCoord)
        {
            extract(out int x, out int y);
            if (OnBoard(x + sx, y + sy))
            {
                newCoord = new Coord(x + sx, y + sy);
                return true;
            }
            newCoord = this;
            return false;
        }

        public static bool OnBoard(int p)
        {
            return (p >= 0) && (p <= 7);
        }

        public static bool OnBoard (int x, int y)
        {
            return OnBoard(x) && OnBoard(y);
        }

        public static bool operator ==(Coord a, Coord b)
        {
            return a.xy == b.xy;
        }

        public static bool operator !=(Coord a, Coord b)
        {
            return a.xy != b.xy;
        }

        public ulong GetBit ()
        {
            return (ulong)1 << xy;
        }
    }

    struct FigureMove
    {
        public FigureCoord figure;
        public Coord to;

        public bool is_enpassant;       public Coord enpassant; // координата битового поля после прыжка пешки
        public bool is_promotion;       public FigureType promotion; // фигура, в которую превратилась пешка
        public bool is_castle;          public FigureCoord castle; // начальная координата ладьи для рокировки

        public FigureMove(FigureCoord figure)
             : this(figure, figure.coord) { }

        public FigureMove(FigureCoord figure, Coord to)
        {
            this.figure = figure;
            this.to = to;
            is_enpassant = false;       enpassant = new Coord();
            is_promotion = false;       promotion = figure.figure;
            is_castle = false;          castle = figure;
        }

        public string Print()
        {
            return figure.figure + " " + figure.coord.Print() +
                   (figure.coord == to ? "" : "-" + to.Print());
        }
    }
}
