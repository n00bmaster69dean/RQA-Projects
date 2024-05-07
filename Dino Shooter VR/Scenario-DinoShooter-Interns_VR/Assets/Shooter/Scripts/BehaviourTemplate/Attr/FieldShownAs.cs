using System;

namespace U4K.BehaviourTemplate.Attr
{
    public class FieldShownAs : Attribute
    {
        private string value;

        public FieldShownAs(string value)
        {
            this.value = value;
        }
        
        public virtual string Value
        {
            get {return value;}
        }
    }
}