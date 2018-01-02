
namespace Game
{
	using UnityEngine;

    public interface INPCAnimal
    {
        int id { get; set; }
        string name { get; set; }

        void Play(string stateName);
        void AutoPlay();
    }

    public interface IAnimal : INPCAnimal
    {
        float hunger { get; set; }
        float health { get; set; }
        float happy { get; set; }
		float iq { get; set;}
		float eq {get;set;}

        void HungerUp(float h);
        void HungerDown(float h);
        void HealthUp(float h);
        void HealthDown(float h);
        void HappyUp(float h);
        void HappyDown(float h);
    }

    public class Animal : IAnimal
    {
        public int id { get; set; }
        public string name { get; set; }
        public float hunger { get; set; }
        public float health { get; set; }
        public float happy { get; set; }
		public float iq { get; set;}
		public float eq {get;set;}
        public Animator animator { get; set; }

		public Animal( int id, string name )
		{
			this.id = id;
			this.name = name;
		}

        public virtual void Play(string stateName)
        {
            animator.Play(stateName);
        }
		public virtual void AutoPlay()
        {
            //todo 编写ai进行自动的游玩
        }

		public virtual void HungerUp(float h)
        {
            hunger += h;
        }
		public virtual void HungerDown(float h)
        {
            hunger -= h;
        }

		public virtual void HealthUp(float h)
        {
            health += h;
        }
		public virtual void HealthDown(float h)
        {
            health -= h;
        }

		public virtual void HappyUp(float h)
        {
            happy += h;
        }
		public virtual void HappyDown(float h)
        {
            happy -= h;
        }

		public virtual void IQUp( float iq )
		{
			iq += iq;
		}
		public virtual void IQDown( float iq )
		{
			iq -= iq;
		}

		public virtual void EQUp( float eq )
		{
			eq += eq;
		}
		public virtual void EQDown(float eq )
		{
			eq -= eq;
		}
    }
}