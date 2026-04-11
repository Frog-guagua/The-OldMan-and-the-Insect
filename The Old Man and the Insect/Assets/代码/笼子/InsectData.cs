using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InsectData:MonoBehaviour//这个玩意作为正式使用的虫虫数据集
{
    public int insectId=0;//abcdef对应123456
    public string insectName;
    public float insectHP=0;
    public float insectAtk=0;
    public float pointsComsumption;
    public string description;
    public Image Image;
    public int insectAtklevel=1;
    public int insetHplevel = 1;

    

    public float setAtkUpData()
    {   
        if(insectAtklevel<3)
        {
            switch (insectId)
            {
                case 1:
                    insectAtk += insectAtklevel * 1;
                    break;
                case 2:
                    insectAtk += insectAtklevel  * 2;
                    break;
                case 3:
                    insectAtk += insectAtklevel * 3;
                    break;
                case 4:
                    insectAtk += insectAtklevel * 1;
                    break;
                case 5:
                    insectAtk += insectAtklevel * 2;
                    break;
                case 6:
                    insectAtk += insectAtklevel  * 4;
                    break;
            }
        }
        insectAtklevel++;
        return insectAtk;
    }

    public float setHpUpData()
    {   
        if(insetHplevel<3)
        {
            switch (insectId)
            {
                case 1:
                    insectHP += insetHplevel  * 2;
                    break;
                case 2:
                    insectHP += insetHplevel  * 2;
                    break;
                case 3:
                    insectHP += insetHplevel  * 1;
                    break;
                case 4:
                    insectHP +=insetHplevel  * 4;
                    break;
                case 5:
                    insectHP += insetHplevel  * 3;
                    break;
                case 6:
                    insectHP += insetHplevel  * 1;
                    break;
            }
        }
        insetHplevel++;
        return insectHP;
    }
    //这一块计划全部根据id用switch来写
    //直接在这里处理升级逻辑
}
