using System;
using U4K.BaseScripts;
using U4K.BehaviourTemplate.Action.Actor;
using UnityEngine;

namespace U4K.BehaviourTemplate.Utils
{
    [Serializable]
    public class LogicCompareUtil
    {
        public static bool isNumberCompareTrue(GameObject go, CompareNumberActor target, LogicNumberCompare compare)
        {
            var numContent = go == null? null : go.GetComponent<NumberContent>();
            if (numContent != null)
            {
                var n1 = numContent.number;
                if (target != null)
                {
                    float n2;
                    if (target.CompareType == CompareType.Object)
                    {
                        var tarNumContent = target.SingleTarget == null
                            ? null
                            : target.SingleTarget.GetComponent<NumberContent>();
                        if (tarNumContent != null)
                        {
                            n2 = tarNumContent.number;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        n2 = target.SingleInput;
                    }
                    switch (compare)
                    {
                        case LogicNumberCompare.Equal:
                            return Mathf.Approximately(n1, n2);
                        case LogicNumberCompare.NotEqual:
                            return !Mathf.Approximately(n1, n2);
                        case LogicNumberCompare.Greater:
                            return n1 > n2;
                        case LogicNumberCompare.GreaterOrEqual:
                            return n1 >= n2;
                        case LogicNumberCompare.Less:
                            return n1 < n2;
                        case LogicNumberCompare.LessOrEqual:
                            return n1 <= n2;
                    }
                }
            }
            return false;
        }
        
        public static bool isBooleanCompareTrue(GameObject go, CompareBooleanActor target, LogicBooleanCompare compare)
        {
            var boolContent = go == null? null : go.GetComponent<BooleanContent>();
            if (boolContent != null)
            {
                var b1 = boolContent.value;
                if (target != null)
                {
                    bool b2;
                    if (target.CompareType == CompareType.Object)
                    {
                        var tarContent = target.SingleTarget == null
                            ? null
                            : target.SingleTarget.GetComponent<BooleanContent>();
                        if (tarContent != null)
                        {
                            b2 = tarContent.value;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        b2 = target.SingleInput;
                    }
                    switch (compare)
                    {
                        case LogicBooleanCompare.Equal:
                            return b1 == b2;
                        case LogicBooleanCompare.NotEqual:
                            return b1 != b2;
                    }
                }
            }
            return false;
        }
    }
    
    public enum LogicNumberCompare
    {
        Equal,
        NotEqual,
        Less,
        LessOrEqual,
        Greater,
        GreaterOrEqual
    }
    
    public enum LogicBooleanCompare
    {
        Equal,
        NotEqual
    }
}