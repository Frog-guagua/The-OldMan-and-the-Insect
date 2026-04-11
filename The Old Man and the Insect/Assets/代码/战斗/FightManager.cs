using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//呱： woc我真快写疯了 我决定给待机虫虫 挂载这个美味脚本
//    代码求你自己跑起来
//    做法 # 做法
public class FightManager : MonoBehaviour
{
    public static bool OnGame1;
    public static bool OnGame2;
    public static bool OnGame3;


    private BugMatch bugMatch;
    private int nowRound =1;
    void Start()
    {
        OnGame1 = true;
    }

    
    void Update()
    {
        bugMatch = GetComponent<BugMatch>();
    }

    //呱： 给第一关教学关卡 写的函数
    public void TeachingRound(GameObject nowBug)
    {
       if(!OnGame1) return;
        
            if (nowRound == 1)
            {
                if (Draggable.nowBugType == E_BugType.A)
                {
                    //呱 ： 注意这里数组下标 需要减一 原本对应的是 第五格
                    if (Draggable.nowGridIndex == 4 )
                    {
                       
                       bugMatch.StartFightBug();
                       nowBug.SetActive(false) ;
                      
                    }
                }
                else if (Draggable.nowBugType == E_BugType.B)
                {
                    if (Draggable.nowGridIndex == 5)
                    {
                        bugMatch.StartFightBug();
                        nowBug.SetActive(false) ;
                    }
                }
                
                
            }
            else if (nowRound == 2)
            {
                
            }
            
    }
}
