
using UnityEngine;

namespace Game
{
    public class BaseView : MonoBehaviour ,IView
    {
        public const string ViewName = ViewDefines.NULL;

        public virtual void Awake()
        {
            
        }

        public virtual void Open()
        {
         
        }

        public virtual void Close()
        {
        
        }
    }
}