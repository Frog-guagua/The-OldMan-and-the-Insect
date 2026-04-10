using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// 定义一个 ScriptableObject 类
[CreateAssetMenu(fileName = "InsectData", menuName = "ScriptableObjects/InsectData", order = 1)]
public class InsectDataSO : ScriptableObject
{
    // 在这里声明你的公共变量
    public int insectId=0;//abcdef对应123456
    public string insectName;
    public float insectHP=0;
    public float insectAtk=0;
    public float pointsComsumption;
    public string description;
    public Image Image;
    public int insectAtklevel=1;
    public int insetHplevel = 1;
}
