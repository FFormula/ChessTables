using System;
using System.Text;

namespace ChessTables
{
    static public class PrintMethods
    {
        public static void Print (this string text)
        {
            ConsoleColor oldForeColor = Console.ForegroundColor;
            foreach (char x in text)
            {
                if (x >= 'a' && x <= 'z')
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (x >= 'A' && x <= 'Z')
                    Console.ForegroundColor = ConsoleColor.White;
                else
                    Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(x);
            }
            Console.ForegroundColor = oldForeColor;
        }

        public static string Board2Ascii (this Bitboard board)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("  +-----------------+");
            for (int y = 7; y >= 0; y--)
            {
                sb.Append(y + 1);
                sb.Append(" | ");
                for (int x = 0; x < 8; x++)
                    sb.Append(board.GetSquare(new Coord (x, y)) + " ");
                sb.AppendLine("|");
            }
            sb.AppendLine("  +-----------------+");
            sb.AppendLine("    a b c d e f g h  ");
            return sb.ToString();
        }

        public static string ULong2Ascii(this ulong bits)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("  +-----------------+");
            for (int y = 7; y >= 0; y--)
            {
                sb.Append(y + 1);
                sb.Append(" | ");
                for (int x = 0; x < 8; x++)
                    sb.Append(
                        (0 == (new Coord(x, y).GetBit() & bits)) ? ". " : "1 ");
                sb.AppendLine("|");
            }
            sb.AppendLine("  +-----------------+");
            sb.AppendLine("    a b c d e f g h  ");
            return sb.ToString();
        }

        public static string Fen2Ascii (this string fen)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("  +-----------------+");
            for (int j = 8; j >= 2; j--)
                fen = fen.Replace(j.ToString(), (j - 1).ToString() + ".");
            fen = fen.Replace("1", ".");
            string[] lines = fen.Split('/');
            for (int j = 0; j < 8; j++)
            {
                sb.Append(8 - j);
                sb.Append(" | ");
                for (int i = 0; i < 8; i++)
                    sb.Append(lines[j][i] + " ");
                sb.AppendLine("|");
            }
            sb.AppendLine("  +-----------------+");
            sb.AppendLine("    a b c d e f g h  ");
            return sb.ToString();
        }

        public static ColorType GetColor(this FigureType figure)
        {
            if (figure < FigureType.bPawn)
                return ColorType.white;
            else
                return ColorType.black;
        }

        public static ColorType Swap(this ColorType color)
        {
            if (color == ColorType.black)
                return ColorType.white;
            else
                return ColorType.black;
        }

        public static string Print(this Coord coord)
        {
            coord.extract(out int x, out int y);
            return ((char)('a' + x)).ToString() +
                   ((char)('1' + y)).ToString();
        }
        
        public static string Print(this FigureMove move)
        {
            return move.figure.figure + " " + move.figure.coord.Print() +
                   (move.figure.coord == move.to ? "" : "-" + move.to.Print()) +
                   (move.is_promotion ? " " + move.promotion : "");
        }
    }
}
