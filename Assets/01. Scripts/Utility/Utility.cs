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
        asyncOp.completed += _ => tcs.SetResult(asyncOp.webRequest); // 요청 완료 후 결과 반환
        return tcs.Task.GetAwaiter();
    }
    public static string KoreanValueChanger(string data)
    {
        if (data == "캐릭터 명")
        {
            return "Name";
        }
        else if (data == "등급")
        {
            return "Grade";
        }
        else if (data == "체력")
        {
            return "Health";
        }
        else if (data == "공격력")
        {
            return "Attack";
        }
        else if (data == "방어력")
        {
            return "Defense";
        }
        else if (data == "민첩도")
        {
            return "Speed";
        }
        else if (data == "치명타 확률")
        {
            return "CriticalPer";
        }
        else if (data == "치명타 피해")
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
        if (data == "캐릭터 스탯")
        {
            return "CharacterSO";
        }
        else if (data == "스킬")
        {
            return "SkillSO";
        }
        else
        {
            return data;
        }
    }
}
