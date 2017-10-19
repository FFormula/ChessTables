using System;
using System.Collections.Generic;

namespace ChessTables
{
    partial class Moves
    {
        Bitboard board;

        public Moves(Bitboard board)
        {
            this.board = board;
        }

        public IEnumerable<FigureMove> NextWhiteFigureMove()
        {
            foreach (FigureCoord figureCoord in board.NextWhiteFigureCoord)
                foreach (FigureMove figureMove in NextFigureMove(figureCoord))
                    yield return figureMove;
        }

        public IEnumerable<FigureMove> NextBlackFigureMove()
        {
            foreach (FigureCoord figureCoord in board.NextBlackFigureCoord)
                foreach (FigureMove figureMove in NextFigureMove(figureCoord))
                    yield return figureMove;
        }

        IEnumerable<FigureMove> NextFigureMove(FigureCoord figureCoord)
        {
            switch (figureCoord.figure)
            {
                case FigureType.wKing:
                case FigureType.bKing:
                    foreach (FigureMove move in NextKingMove(figureCoord))
                        yield return move;
                    break;

                case FigureType.wKnight:
                case FigureType.bKnight:
                    foreach (FigureMove move in NextKnightMove(figureCoord))
                        yield return move;
                    break;
                    
                case FigureType.wRook:
                case FigureType.bRook:
                    foreach (FigureMove move in NextRookMove(figureCoord))
                        yield return move;
                    break;
                    
                case FigureType.wBishop:
                case FigureType.bBishop:
                    foreach (FigureMove move in NextBishopMove(figureCoord))
                        yield return move;
                    break;

                case FigureType.wQueen:
                case FigureType.bQueen:
                    foreach (FigureMove move in NextQueenMove(figureCoord))
                        yield return move;
                    break;

                case FigureType.wPawn:
                    foreach (FigureMove move in NextWhitePawnMove(figureCoord))
                        yield return move;
                    break;
                    /*
                case FigureType.bPawn:
                    foreach (FigureMove move in NextBlackPawnMove(figureCoord))
                        yield return move;
                    break;*/

            }
        }

    }
}
