using System;

namespace ChessTables
{
    public enum FigureType
    {
        wPawn,
        wKnight,
        wBishop,
        wRook,
        wQueen,
        wKing,
        bPawn,
        bKnight,
        bBishop,
        bRook,
        bQueen,
        bKing
    }

    public enum ColorType
    {
        white,
        black
    }

    public struct FigureCoord
    {
        public FigureType figure;
        public Coord coord;

        public FigureCoord (FigureType figure, Coord coord)
        {
            this.figure = figure;
            this.coord = coord;
        }
    }

}
