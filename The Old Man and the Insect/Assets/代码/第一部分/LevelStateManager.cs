using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;



//ok啊也是偷懒直接写一个伪状态机
public enum LevelState
{
   OnEnterGame,
   KnockingDoor,
   openDoorAnm,
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
    [Header("敲门后延迟播放对话时间")]
    public float Delay_Before_Knocking = 2f;
    [Header("开始播放开门动画到开启对话的延迟时间")]
    public float Delay_Before_dia = 2f;
    private LevelState currentState;
    private LevelState lastState;
<<<<<<< Updated upstream
    public AudioClip bgm;
=======
    public GameObject player;
    [Header("音效")]
>>>>>>> Stashed changes
   public AudioClip KnockingSound;
   public AudioClip birdsound;
   [Header("对话")]
    public DialogueData dia1;
    public DialogueData dia2;
    [Header("门检测区域范围")]
    public Vector2 leftAndDown_DoorRange;
    public Vector2 rightAndUp_DoorRange;
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
        AudioMgr.Instance.PlayBGM(bgm);
      
        currentState = LevelState.OnEnterGame;
        lastState = LevelState.OnEnterGame;
       
       AudioMgr.Instance.PlaySFX(birdsound);
      
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
               case LevelState.openDoorAnm:
                   StartCoroutine(openAnim());
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
            case LevelState.KnockingDoor://目前设定为玩家移动到门区域然后就开门,使用一个最愚蠢的坐标判定
                if (player.transform.position.x > leftAndDown_DoorRange.x 
                    && player.transform.position.x < rightAndUp_DoorRange.x
                    && player.transform.position.y > leftAndDown_DoorRange.y &&
                    player.transform.position.y < rightAndUp_DoorRange.y)

                {
                    SwitchState(LevelState.openDoorAnm);
                }
                break;
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
    IEnumerator openAnim()
    {
        print("放开门动画");
        yield return new WaitForSeconds(Delay_Before_dia);
        SwitchState(LevelState.dialogue);
    }

    void diaEnd()
    {   
        print("获得笼子");
        //获得笼子
        SwitchState(LevelState.havingCage);
    }
}
