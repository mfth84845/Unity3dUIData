using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Reflection;
using Object = UnityEngine.Object;
using System.Linq;
using UISystem;

using ExtensionMethods;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Text))]
public class UIText : MonoBehaviour
{
    public bool Test = false;
    // Use this for initialization
    void Start()
    {
        //TextValueInt.AddLinster(() => { GetComponent<Text>().text = TextValueInt.Value.ToString(); });
        TextValueInt.AddLinster(() => { GetComponent<Text>().text = TextValueFloat.Value.ToString(); });

    }


    // Update is called once per frame
    void Update()
    {
        if (Test == true)
        {
            Test = false;
            UIDataContext.Instance.LocalRoleHp.LogicSetValue(Random.Range(0, 20));
            UIDataContext.Instance.Exp.LogicSetValue(Random.Range(20.1f, 100));
        }


    }

    [Header("测试值I")]
    public UIValueInt TextValueInt;
    [Header("测试值F")]
    public UIValueFloat TextValueFloat;




}
namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static int WordCount(this System.String str)
        {
            return str.Split(new char[] { ' ', '.', '?' },
                           System.StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
    public static class AAA
    {
        public static int getInfo(this IEnumerable s)
        {
            return 1;
        }
    }
    public static class Extensions
    {
        public static Grades minPassing = Grades.D;
        public static bool Passing(this Grades grade)
        {
            return grade >= minPassing;
        }

        public static int  Excute<T>(this Grades g,int a,T r)
        {
            int cr = 0;
            switch (g)
            {
                case Grades.F:
                    cr = (int)g + a;
                    break;
                case Grades.D:
                    break;
                case Grades.C:
                    break;
                case Grades.B:
                    break;
                case Grades.A:
                    break;
                default:
                    break;
            }
            return cr;
        }

        public static void InfoList(this string s, string n = "A", float Deep = 3.5f, int Index=23)
        {

        }

    }

    public enum Grades { F = 0, D = 1, C = 2, B = 3, A = 4 };

}

public class AAA
{
    public void SA()
    {
        var cr = "JKL";
        cr.WordCount();
        cr.InfoList(Index: 1);

        cr.getInfo();
        List<int> crr = new List<int>();

        crr.getInfo();
        Grades ss = Grades.B;
        ss.Passing();
        ss.Excute(r: "sa", a: 3);
    
    }
}


