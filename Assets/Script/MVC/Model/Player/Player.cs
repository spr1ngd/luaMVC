
using System.Collections.Generic;

namespace Game
{
    public interface IPlayer
    {
        string username { get;}
        string password { get; }
    }

    public class Player : IPlayer
    {
        // login
        public string username { get; }
        public string password { get; }

        // register
        public string email = string.Empty;
        public int age;
        public int sex; //0 男 1 女
        public int idCard;

        // roles
        public List<Role> roles = new List<Role>();

        public Player(){}
        public Player(string name, string password)
        {
            this.username = name;
            this.password = password;
        }
        public Player(string name, string password, string email = null, int age = 0, int sex = 0, int idCard = 0)
        {
            this.username = name;
            this.password = password;
            this.email = email;
            this.age = age;
            this.sex = sex;
            this.idCard = idCard;
        }

        
    }
}