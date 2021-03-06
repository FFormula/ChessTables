﻿namespace ChessTables
{
    public struct FigureMove
    {
        public FigureCoord figure;
        public Coord to;

        public bool is_enpassant; public Coord enpassant; // координата битового поля после прыжка пешки
        public bool is_promotion; public FigureType promotion; // фигура, в которую превратилась пешка
        public bool is_castle; public FigureCoord castle; // начальная координата ладьи для рокировки

        public FigureMove(FigureCoord figure)
             : this(figure, figure.coord) { }

        public FigureMove(FigureCoord figure, Coord to)
        {
            this.figure = figure;
            this.to = to;
            is_enpassant = false; enpassant = new Coord();
            is_promotion = false; promotion = figure.figure;
            is_castle = false; castle = figure;
        }

    }
}
