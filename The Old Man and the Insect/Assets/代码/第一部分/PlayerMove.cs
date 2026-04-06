using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//呱：写在最前面 ——> 这个脚本挂在玩家身上
public class PlayerMove : MonoBehaviour
{
    //[玩家固定步长] ： 这个美术老大自己来调数值 可以随便安个
    [SerializeField]  float PlayerMoveLength_X =0;
    [SerializeField]  float PlayerMoveLength_Y =0;
    
    //[玩家坐标]
    private Transform PlayerTransform;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
