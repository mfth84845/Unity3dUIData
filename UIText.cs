using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Reflection;
using Object = UnityEngine.Object;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
using UISystem;
#endif

[RequireComponent(typeof(Text))]
public class UIText : MonoBehaviour
{
    public bool Test = false;
    // Use this for initialization
    void Start()
    {
        TextValueInt.AddLinster(() => { GetComponent<Text>().text = TextValueInt.Value.ToString(); });

    }


    // Update is called once per frame
    void Update()
    {
        if (Test == true)
        {
            Test = false;
            UIDataContext.Instance.LocalRoleHp.LogicSetValue(Random.Range(0, 20));
        }
    }

    [Header("测试值")]
    public UIValueInt TextValueInt;




}


