using System;

namespace U4K.BehaviourTemplate.Attr
{
    public class RuntimeActor : Attribute
    {
        private string value;

        public RuntimeActor(string value)
        {
            this.value = value;
        }
        
        public virtual string Value
        {
            get {return value;}
        }
    }
}