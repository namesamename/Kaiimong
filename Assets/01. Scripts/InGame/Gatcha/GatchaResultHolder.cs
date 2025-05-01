using System.Collections.Generic;
using UnityEngine;

public static class GatchaResultHolder //리스트홀더- 씬을 넘기고 나서 결과를 보이기 위한 리스트를 가져가는 메서드
{
    public static List<Character> results = new();
    public static GatchaSessionData session;
}