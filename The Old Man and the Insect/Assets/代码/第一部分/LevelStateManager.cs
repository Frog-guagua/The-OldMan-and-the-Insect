using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//ok啊也是偷懒直接写一个伪状态机
public enum LevelState
{
   OnEnterGame,
   KnockingDoor,
   dialogue,
   havingCage
}

public class LevelStateManager : MonoBehaviour
{
    #region 单例

    

   
    private LevelStateManager() {}

   
    private static LevelStateManager _instance;

    
    public static LevelStateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelStateManager>();
                if (_instance == null)
                {
                  
                    var singletonObject = new GameObject("LevelStateManagerSingleton");
                    _instance = singletonObject.AddComponent<LevelStateManager>();
                }
            }
            return _instance;
        }
    }
    #endregion
    public float Delay_Before_Knocking = 2f;
    private LevelState currentState;
    private LevelState lastState;

   public AudioClip KnockingSound;
   public AudioClip birdsound;
    public DialogueData dia1;
    public DialogueData dia2;
    // Start is called before the first frame update
    void Start()
    {   
        if (_instance != this && _instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
         
        }

      
        currentState = LevelState.OnEnterGame;
        lastState = LevelState.OnEnterGame;
       //待实现：播放音效，等写了音效管理系统
       AudioMgr.Instance.PlaySFX(birdsound);
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
               case LevelState.dialogue:
                   DialogueManager.Instance.StartDialogue(dia2,diaEnd);
                   break;
               case LevelState.havingCage:
                   //桌子逻辑，点击桌子放笼子
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
        print("咚咚咚");//待实现音效
        AudioMgr.Instance.PlaySFX(KnockingSound);
        yield return new WaitForSeconds(1f);
        DialogueManager.Instance.StartDialogue(dia1);
    }

    void diaEnd()
    {   
        print("ding");
        //获得笼子
        SwitchState(LevelState.havingCage);
    }
}
