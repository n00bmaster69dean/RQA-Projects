using System;

namespace U4K.BehaviourTemplate.Attr
{
    public class FieldSortIndex : Attribute
    {
        public FieldSortIndex(int index)
        {
            Index = index;
        }

        public virtual int Index { get; }
    }
}