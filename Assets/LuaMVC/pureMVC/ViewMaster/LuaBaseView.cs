
// 这个改造是否值得且正确？？
using System;

namespace LuaMVC 
{ 
    public interface ILuaBaseView
    {
        string ViewName{get;set;}
        Action Initialize {get;set;}
        Action Open{get;set;}
        Action Close{get;set;} 
    }

    public class LuaBaseView : ILuaBaseView
    {
        public string ViewName{get;set;}
        public Action Initialize { get; set; }
        public Action Open { get; set; }
        public Action Close { get; set; } 
    }
}