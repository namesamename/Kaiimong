using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatGrade
{
    A, B, C, D,
}

public class Enemy : SO
{
    public string Name;
    public float HP;
    public StatGrade Att;
    public StatGrade Def;
    public StatGrade Speed;
    public StatGrade Cri;
    public int SkillID;


  

}
