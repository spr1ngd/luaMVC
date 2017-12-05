
using UnityEngine;

namespace Game
{
    public class ChesspieceData
    {
        public int ChesspieceType = 0;
        public bool active = false;
        public ClickIndex index;
        public Vector2 origin;
        public Color Color
        {
            get
            {
                if (ChesspieceType == 0)
                    return new Color(1, 0, 0, 1);
                if (ChesspieceType == 1)
                    return new Color(0, 1, 0, 1);
                if (ChesspieceType == 2)
                    return new Color(0, 0, 1, 1);
                if (ChesspieceType == 3)
                    return new Color(1, 1, 0, 1);
                if (ChesspieceType == 4)
                    return new Color(0, 1, 1, 1);
                if (ChesspieceType == 5)
                    return new Color(1, 0, 1, 1);
                if (ChesspieceType == 6)
                    return new Color(1, 1, 1, 1);
                return new Color(0, 0, 0, 1);
            }
        }

        public ChesspieceData()
        {
            active = false;
        }
        public ChesspieceData(int chesspieceType)
        {
            this.ChesspieceType = chesspieceType;
            active = true;
        }
        public ChesspieceData(int xIndex, int yIndex)  
        {
            this.index.x = xIndex;
            this.index.y = yIndex;
            active = true;
        }
    }
}