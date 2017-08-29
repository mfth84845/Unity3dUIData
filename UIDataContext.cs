using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UISystem;

namespace UISystem
{

    public class UIDataValueBaseBase { }

    public class UIDataValueBase<T> : UIDataValueBaseBase
    {
        public T Value { get; protected set; }

        public void LogicSetValue(T v)
        {
            Value = v;
            if (OnValueChange != null)
                OnValueChange();
        }

        public System.Action OnValueChange;
    }

    public class IntValue : UIDataValueBase<int>
    {

    }
    public class FloatValue : UIDataValueBase<float>
    {

    }
    public class StringValue : UIDataValueBase<string>
    {

    }
}

public class UIDataContext
{
    public static UIDataContext Instance = new UIDataContext();


    public IntValue LocalRoleHp = new IntValue();

    public IntValue LocalRoleHpMax = new IntValue();


    public FloatValue Exp = new FloatValue();


    void das()
    {


    }

}



