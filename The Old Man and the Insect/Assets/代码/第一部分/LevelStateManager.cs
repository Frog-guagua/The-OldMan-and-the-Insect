using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//ok啊也是偷懒直接写一个伪状态机
public enum LevelState
{
   OnEnterGame,
   KnockingDoor,
}
public class LevelStateManager : MonoBehaviour
{   
    
    public float Delay_Before_Knocking = 2f;
    private LevelState currentState;
    private LevelState lastState;


    public DialogueData dia1;
    // Start is called before the first frame update
    void Start()
    {
        currentState = LevelState.OnEnterGame;
        lastState = LevelState.OnEnterGame;
       //待实现：播放音效，等写了音效管理系统
       print("吱吱吱");
       StartCoroutine(DelayToSwitchState(LevelState.KnockingDoor, Delay_Before_Knocking));
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != lastState)//进入新状态执行一次
        {
            switch (currentState)
            {   
               case LevelState.KnockingDoor:
                   StartCoroutine(KnockingDoorState());
                   break;
               
               
            }
            lastState = currentState;
        }
        
        
        switch (currentState)
        {
            
        }
    }

    // 每个状态的具体逻辑处理
   
    // 切换状态
    public void SwitchState(LevelState newState)
    {
        currentState = newState;
    }
    
    IEnumerator DelayToSwitchState(LevelState newState,float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SwitchState(newState);
    }

    IEnumerator KnockingDoorState()
    {   
        print("咚咚咚");
        yield return new WaitForSeconds(1f);
        DialogueManager.Instance.StartDialogue(dia1);
    }
}
