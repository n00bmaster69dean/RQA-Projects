using System;

namespace U4K.BehaviourTemplate.Attr
{
    public class CustomVector2ShownAs : Attribute
    {
        private string xshow;
        private string yshow;

        public CustomVector2ShownAs(string xshow, string yshow)
        {
            this.xshow = xshow;
            this.yshow = yshow;
        }
        
        public virtual string Xshow
        {
            get {return xshow;}
        }
        public virtual string Yshow
        {
            get {return yshow;}
        }
        
    }
}