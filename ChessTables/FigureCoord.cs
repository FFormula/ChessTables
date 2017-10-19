namespace ChessTables
{
    public struct FigureCoord
    {
        public FigureType figure;
        public Coord coord;

        public FigureCoord(FigureType figure, Coord coord)
        {
            this.figure = figure;
            this.coord = coord;
        }
    }
}
