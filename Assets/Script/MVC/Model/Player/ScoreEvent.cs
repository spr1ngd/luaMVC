

namespace Game
{
    public class ScoreEvent
    {
        public int currentScore;
        public int highScore;

        public ScoreEvent( int currentScore,int highScore )
        {
            this.currentScore = currentScore;
            this.highScore = highScore;
        }
    }
}