using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class Utility
{

    public static string ToKNumber(this int value)
    {
        int k = 0;
        int comma = 0;
        if (value % 1000 == 0)
        {
            k = value / 1000;
           return $"{k}k";
        }
        else if(value % 1000 != 0 && value > 1000)
        {
            k = value / 1000;
            comma = value % 1000;
            comma = comma / 100;
            return $"{k}.{comma}k";
        }
        else if(value < 1000)
        {
            k = value;
            return k.ToString();
            

        }
        else
        {
            return value.ToString();
        }
    }
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
}
