using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UISystem
{

    public class UIValueBaseBase { }

    public abstract class UIValueBase<T, U> :UIValueBaseBase where T : UIDataValueBase<U>
    {
        /// <summary>
        /// UseToMessage if Selected Value being changed,just work for FixedMode
        /// </summary>
        public void AddLinster(System.Action func)
        {
            func();
            InitLink();
            OnValueChange += func;
        }

        protected abstract void InitLink();

        protected System.Action OnValueChange;

        //fixedMode p

        //fixedMode p
        [SerializeField]
        UIDataValueBase<U> ValueTarget_ = null;
        protected UIDataValueBase<U> ValueTarget
        {
            get
            {
                if (string.IsNullOrEmpty(ValueTargetName) == false && ValueTarget_ == null)
                {
                   
                    ValueTarget_ = (T)typeof(UIDataContext).GetField(ValueTargetName).GetValue(UIDataContext.Instance);
                }
                return ValueTarget_;


            }
        }

     

        [SerializeField]
        protected string ValueTargetName = null;

          
        //else p
        [SerializeField]
       protected  object ValueTargrt;
        [SerializeField]
        protected FieldInfo IntFieldTarget;


    }

    [System.Serializable]
    public class UIValueInt : UIValueBase<IntValue,int>
    {


        protected override void InitLink()
        {
            if (FixedMode)
                ValueTarget.OnValueChange += () =>
                {
                    if (this.OnValueChange != null)
                        this.OnValueChange();
                };
        }

        //Run Time Only
        public int Value
        {
            get
            {
                if (FixedMode)
                    return ValueTarget != null ? ValueTarget.Value : 0;
                else
                    return (int)IntFieldTarget.GetValue(ValueTargrt);
            }
        }
        [SerializeField]
        bool FixedMode = true;

        //gameObject U Set
        [SerializeField]
        Object target;

      

    }




#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(UIValueBaseBase), true)]
    public class UIDataIntDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2;
        }


        string ValueTargetNameP;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            #region
            if (ValueTargetNameP != null)
            {
                property.FindPropertyRelative("ValueTargetName").stringValue = ValueTargetNameP;
                ValueTargetNameP = null;
            }
            #endregion



            Rect c = position;
            c.Set(c.x, c.y, c.width, c.height * 0.5f);

            #region Line 1
            Rect cc = c;
            cc.Set(cc.x, cc.y, c.width * 0.2f, c.height);
            property.FindPropertyRelative("FixedMode").boolValue = EditorGUI.Toggle(cc, property.FindPropertyRelative("FixedMode").boolValue);
            cc.Set(cc.xMax, cc.y, c.width * 0.8f, c.height);
            if (property.FindPropertyRelative("FixedMode").boolValue == false)
                property.FindPropertyRelative("target").objectReferenceValue = EditorGUI.ObjectField(cc, property.FindPropertyRelative("target").objectReferenceValue, typeof(Object));
            #endregion

            c.Set(c.x, c.yMax, c.width, c.height);
            c.width *= 0.5f;
            GUI.Label(c, "TargetValue");
            GameObject target = property.FindPropertyRelative("target").objectReferenceValue as GameObject;
            c.Set(c.xMax, c.y, c.width, c.height);

            string SetValue = "??";

            if (property.FindPropertyRelative("FixedMode").boolValue)
            {
                Color old = GUI.color;
                GUI.color = Color.yellow;
                if (string.IsNullOrEmpty(property.FindPropertyRelative("ValueTargetName").stringValue) == false)
                {
                    GUI.color = old;
                    SetValue = property.FindPropertyRelative("ValueTargetName").stringValue;
                }
            }
            else
            {
                SetValue = "null";
            }
            if (GUI.Button(c, SetValue))
            {
                GenericMenu selectMenu = new GenericMenu();

                if (property.FindPropertyRelative("FixedMode").boolValue)
                {
                    selectMenu.AddItem(new GUIContent("unSet"), false, () => { ValueTargetNameP = ""; });

                    List<FieldInfo> cr = new List<FieldInfo>();
                    foreach (var item in typeof(UIDataContext).GetFields())
                    {
                        System.Type cBaseType = null;
                        cBaseType = item.FieldType.BaseType ?? null;
                        if (cBaseType != null)
                            cBaseType = cBaseType.BaseType ?? null;
                        if (cBaseType != null && cBaseType.Equals(typeof(UIDataValueBaseBase)))
                        {
                            cr.Add(item);
                            selectMenu.AddItem(new GUIContent(item.Name), false, () => { ValueTargetNameP = item.Name; });
                        }
                    }
                }
                else
                {
                    var cCompoents = target.GetComponents<Component>();
                    foreach (var cItem in cCompoents)
                    {
                        foreach (var ccitem in cItem.GetType().GetFields())
                        {
                            if (ccitem.FieldType.Equals(typeof(int)))
                            {
                                int cds = (int)ccitem.GetValue(cItem);
                                //Debug.Log((cds));
                            }
                            else if (ccitem.FieldType.Equals(typeof(UIValueInt)))
                            {
                                UIValueInt cds = (UIValueInt)ccitem.GetValue(cItem);
                            }
                        }


                        selectMenu.AddItem(new GUIContent("ATT/A"), false, sss => { Debug.Log(cc.ToString()); property.FindPropertyRelative("ValueName").stringValue = "AAASSS"; }, "AA");
                    }

                    selectMenu.AddItem(new GUIContent("ATT/A"), false, sss => { Debug.Log(cc.ToString()); property.FindPropertyRelative("ValueName").stringValue = "AAASSS"; }, "AA");
                    selectMenu.AddItem(new GUIContent("BTT"), false, null, "BB");
                    selectMenu.AddItem(new GUIContent("CTT"), false, null, "CC");

                }
                selectMenu.ShowAsContext();

            }




        }
        public void RRR()
        {

        }


    }
}


#endif