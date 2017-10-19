using System.Collections.Generic;

namespace ChessTables
{
    class MovesQueen
    {

        Bitboard board;
        FigureCoord figureCoord;

        public MovesQueen (Bitboard board)
        {
            this.board = board;
        }

        ulong Slide1(ulong coord, ulong stops) => Slide(coord, -9, Moves.noH, stops);
        ulong Slide2(ulong coord, ulong stops) => Slide(coord, -8, Moves.ALL, stops);
        ulong Slide3(ulong coord, ulong stops) => Slide(coord, -7, Moves.noA, stops);
        ulong Slide4(ulong coord, ulong stops) => Slide(coord, -1, Moves.noH, stops);
        ulong Slide6(ulong coord, ulong stops) => Slide(coord, +1, Moves.noA, stops);
        ulong Slide7(ulong coord, ulong stops) => Slide(coord, +7, Moves.noH, stops);
        ulong Slide8(ulong coord, ulong stops) => Slide(coord, +8, Moves.ALL, stops);
        ulong Slide9(ulong coord, ulong stops) => Slide(coord, +9, Moves.noA, stops);
        ulong Slide(ulong coord, int shift, ulong range, ulong stops)
        {
            ulong map = 0UL;
            while (true)
            {
                if (shift > 0) coord <<= +shift;
                else coord >>= -shift;
                if (0 == (coord & range)) break;
                map |= coord;
                if (0 != (coord & stops)) break;
            }
            return map;
        }

        
        #region RookMoves
        public IEnumerable<FigureMove> NextRookMove(FigureCoord figureCoord)
        {
            this.figureCoord = figureCoord;
            return NextRookMove_BitMask();
        }

        IEnumerable<FigureMove> NextRookMove_BitMask()
        {
            ulong rookMoves = AllRookSlides();
            //rookMoves.ulong2ascii().Print();
            foreach (Coord to in Bitboard.NextCoord(rookMoves))
                yield return new FigureMove(figureCoord, to);
        }

        ulong AllRookSlides()
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
        #endregion RookMoves


        #region BishopMoves
        public IEnumerable<FigureMove> NextBishopMove(FigureCoord figureCoord)
        {
            this.figureCoord = figureCoord;
            return NextBishopMove_BitMask();
        }

        IEnumerable<FigureMove> NextBishopMove_BitMask()
        {
            ulong bishopMoves = AllBishopMoves();
            //bishopMoves.ulong2ascii().Print();
            foreach (Coord to in Bitboard.NextCoord(bishopMoves))
                yield return new FigureMove(figureCoord, to);
        }

        ulong AllBishopMoves()
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
        #endregion BishopMoves


        #region QueenMoves
        public IEnumerable<FigureMove> NextQueenMove(FigureCoord figureCoord)
        {
            this.figureCoord = figureCoord;
            return NextQueenMove_BitMask();
        }

        IEnumerable<FigureMove> NextQueenMove_BitMask()
        {
            ulong bishopMoves = AllQueenMoves();
            //bishopMoves.ulong2ascii().Print();
            foreach (Coord to in Bitboard.NextCoord(bishopMoves))
                yield return new FigureMove(figureCoord, to);
        }

        ulong AllQueenMoves()
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
        #endregion QueenMoves   



        /*
        IEnumerable<FigureMove> NextRookMove_Steps()
        {
            ulong rookMoves = AllRookSteps(figureCoord);
            // rookMoves.ulong2ascii().Print();
            foreach (Coord to in Bitboard.NextCoord(rookMoves))
                yield return new FigureMove(figureCoord, to);
        }
        
        ulong AllRookSteps()
        {
            figureCoord.coord.extract(out int x, out int y);
            ColorType color = figureCoord.figure.GetColor();
            ulong stops = board.GetOwnedBits();
            return
                (Step6(x, y, stops) |
                 Step4(x, y, stops) |
                 Step8(x, y, stops) |
                 Step2(x, y, stops)) & ~board.GetColorBits(color);
        }
        
        ulong Step1(int x, int y, ulong stops) => Step(x, y, -1, -1, stops);
        ulong Step2(int x, int y, ulong stops) => Step(x, y, 0, -1, stops);
        ulong Step3(int x, int y, ulong stops) => Step(x, y, +1, -1, stops);
        ulong Step4(int x, int y, ulong stops) => Step(x, y, -1, 0, stops);
        ulong Step6(int x, int y, ulong stops) => Step(x, y, +1, 0, stops);
        ulong Step7(int x, int y, ulong stops) => Step(x, y, -1, 1, stops);
        ulong Step8(int x, int y, ulong stops) => Step(x, y, 0, 1, stops);
        ulong Step9(int x, int y, ulong stops) => Step(x, y, +1, 1, stops);

        ulong Step(int x, int y, int sx, int sy, ulong stops)
        {
            ulong map = 0;
            while (true)
            {
                x += sx;
                y += sy;
                if (!Coord.OnBoard(x, y))
                    break;
                ulong bit = 1UL << ((y << 3) | x);
                map |= bit;
                if (0 != (stops & bit))
                    break;
            }
            return map;
        }*/


    }
}
