using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class Utility
{
    public static List<T> ToList<T>(this T[] obj)
    {
        return new List<T>(obj);
    }

    public static Vector3 ToVector3(this string str)
    {
        if (str[0] == '(' && str.Last() == ')')
        {
            var pos = str.Substring(1, str.Length -2).Split(",");

            return new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));
        }
        return Vector3.zero;
    }
    public static TaskAwaiter<UnityWebRequest> GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
    {
        var tcs = new TaskCompletionSource<UnityWebRequest>();
        asyncOp.completed += _ => tcs.SetResult(asyncOp.webRequest); // ��û �Ϸ� �� ��� ��ȯ
        return tcs.Task.GetAwaiter();
    }
    public static string KoreanValueChanger(string data)
    {
        if (data == "ĳ���� ��")
        {
            return "Name";
        }
        else if (data == "���")
        {
            return "Grade";
        }
        else if (data == "ü��")
        {
            return "Health";
        }
        else if (data == "���ݷ�")
        {
            return "Attack";
        }
        else if (data == "����")
        {
            return "Defense";
        }
        else if (data == "��ø��")
        {
            return "Speed";
        }
        else if (data == "ġ��Ÿ Ȯ��")
        {
            return "CriticalPer";
        }
        else if (data == "ġ��Ÿ ����")
        {
            return "CriticalAttack";
        }
        else
        {
            return data;
        }
    }
    public static string KoreanClassChanger(string data)
    {
        if (data == "ĳ���� ����")
        {
            return "CharacterSO";
        }
        else if (data == "��ų")
        {
            return "SkillSO";
        }
        else
        {
            return data;
        }
    }
}
