using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTables
{
    struct FigureMove
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

        public string Print()
        {
            return figure.figure + " " + figure.coord.Print() +
                   (figure.coord == to ? "" : "-" + to.Print()) +
                   (is_promotion ? " " + promotion : "");
        }
    }
}
