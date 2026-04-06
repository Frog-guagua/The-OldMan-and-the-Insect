using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 定义一个 ScriptableObject 类
[CreateAssetMenu(fileName = "InsectData", menuName = "ScriptableObjects/InsectData", order = 1)]
public class InsectData : ScriptableObject
{
    // 在这里声明你的公共变量
    public int insectId;
    public string insectName;
    public float insectHP;
    public float insectAtk;
    public float pointsComsumption=1;
}
