
namespace Game
{
    public class Role
    {
        public int id;
        public int playerid;
        public string name;
        public int roleType;
        public int level;
        public int maxscore;

        public Role(){}
        public Role( int id,int playerId,string name,int roleType,int level,int maxScore)
        {
            this.id = id;
            this.playerid = playerId;
            this.name = name;
            this.roleType = roleType;
            this.level = level;
            this.maxscore = maxScore;
        }
    }
}