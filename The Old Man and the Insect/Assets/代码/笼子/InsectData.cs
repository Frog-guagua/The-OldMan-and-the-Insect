using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class InsectData:MonoBehaviour//这个玩意作为正式使用的虫虫数据集
//呱：强 抑郁症模型小鼠老大 强
{
    public int insectId=0;//abcdef对应123456
    public string insectName;
    public int insectHP=0;
    public int insectAtk=0;
    public int insectLevel; //呱：虫子的等级 
    
    public string description;
    public Image Image;
    /// <summary>
    /// 用于加小虫的战斗力 的经验值 （消耗）
    /// </summary>
     public int AtkUpConsumpution=1;
    
    /// <summary>
    /// 用于加小虫的生命值 的经验值 （消耗）
    /// </summary>
     public int HpUpConsumption = 1;



    public int UpdateExperience(int consumption)
    {
        return DataBroker.experience -= consumption;
        
    }
    
    /// <summary>
    /// 给攻击力 加点 的逻辑
    /// </summary>
    /// <returns></returns>
    public float LetAtkUp()
    {
        //呱： 只有消耗的经验值 是 虫虫等级的倍数 的时候 才可以成功的给攻击力加点
        if (AtkUpConsumpution % insectLevel == 0)
        {
            insectAtk += AtkUpConsumpution/insectLevel;
        }
        
        return insectAtk;
    }

    
    /// <summary>
    /// 设置生命升级的逻辑
    /// </summary>
    /// <returns></returns>
    public float LetHPUp()
    {   
        //呱： 只有消耗的经验值 是 虫虫等级的倍数 的时候 才可以成功的给生命值加点
        if (AtkUpConsumpution % insectHP == 0)
        {
            insectHP += AtkUpConsumpution/insectLevel;
        }
        
        return insectHP;
    }
    //这一块计划全部根据id用switch来写
    //直接在这里处理升级逻辑
}
