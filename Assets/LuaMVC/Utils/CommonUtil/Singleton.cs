
using System;

namespace LuaMVC
{
    public abstract class Singleton<T> where T : class, new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }

        protected Singleton()
        {
            if (null != _instance)
                throw new Exception("This " + typeof(T) + " Singleton Instance is not null !!!");
            Init();
        }

        public virtual void Init()
        {

        }
    }
}