using System.Collections.Generic;

namespace ChessTables
{
    partial class Moves
    {
        Bitboard board;

        public const ulong noA = 0xFeFeFeFeFeFeFeFe;
        public const ulong nAB = 0xFcFcFcFcFcFcFcFc;
        public const ulong noH = 0x7f7f7f7f7f7f7f7f;
        public const ulong nGH = 0x3f3f3f3f3f3f3f3f;
        public const ulong ALL = 0xFFFFFFFFFFFFFFFF;

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
                    foreach (FigureMove move in new MovesKing(board).NextKingMove(figureCoord))
                        yield return move;
                    break;

                case FigureType.wKnight:
                case FigureType.bKnight:
                    foreach (FigureMove move in new MovesKnight(board).NextKnightMove(figureCoord))
                        yield return move;
                    break;
                    
                case FigureType.wRook:
                case FigureType.bRook:
                    foreach (FigureMove move in new MovesQueen(board).NextRookMove(figureCoord))
                        yield return move;
                    break;
                    
                case FigureType.wBishop:
                case FigureType.bBishop:
                    foreach (FigureMove move in new MovesQueen(board).NextBishopMove(figureCoord))
                        yield return move;
                    break;

                case FigureType.wQueen:
                case FigureType.bQueen:
                    foreach (FigureMove move in new MovesQueen(board).NextQueenMove(figureCoord))
                        yield return move;
                    break;

                case FigureType.wPawn:
                case FigureType.bPawn:
                    foreach (FigureMove move in new MovesPawn(board).NextPawnMove(figureCoord))
                        yield return move;
                    break;

            }
        }

    }
}
