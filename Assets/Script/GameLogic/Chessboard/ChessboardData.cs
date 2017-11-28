
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ChessboardData
    {
        private Chessboard m_cbView = null;

        public ChessboardData( Chessboard cbView )
        {
            this.m_cbView = cbView;
        }

        //todo 算法可以进一步优化
        public ChesspieceData[,] BuilderChesspieces()
        {
            List<int> chesspiecesIndex = new List<int>();
            for (int i = 0; i < m_cbView.xCount * m_cbView.yCount; i++)
                chesspiecesIndex.Add(i);
            // 声明
            var ChesspieceDatas = new ChesspieceData[m_cbView.xCount, m_cbView.yCount];
            // 初始化
            for (int x = 0; x < m_cbView.xCount; x++)
            {
                for (int y = 0; y < m_cbView.yCount; y++)
                    ChesspieceDatas[x, y] = new ChesspieceData();
            }

            for (int i = (int)(chesspiecesIndex.Count * m_cbView.chesspiecePercent / 2.0f); i >= 0; i--)
            {
                // 同时生成两个棋子
                // todo 研究该种错误类型原因
                // ChesspieceData chesspieceData = new ChesspieceData(Random.Range(0, 7)); // 传入棋子的类型

                // 同时生成两个棋子
                int chesspieceType = Random.Range(0, 7);
                for (int j = 0; j < 2; j++)
                {
                    ChesspieceData chesspieceData = new ChesspieceData(chesspieceType);
                    int firstIndex = Random.Range(0, chesspiecesIndex.Count);
                    int xIndex = chesspiecesIndex[firstIndex] / m_cbView.yCount;
                    int yIndex = chesspiecesIndex[firstIndex] % m_cbView.yCount;
                    ChesspieceDatas[xIndex, yIndex] = chesspieceData;
                    chesspiecesIndex.RemoveAt(firstIndex);
                }
            }
            return ChesspieceDatas;
        }
    }
}