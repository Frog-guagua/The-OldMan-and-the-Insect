using System.Collections;
using UnityEngine;

//呱：写在最前面 ——> 这个脚本挂载在MainCamara上面！
public class CamaraDelayFollow : MonoBehaviour
{
    #region 玩家相关

    //[玩家位置]
    [Header("Player")]
    [SerializeField]Transform PlayerPos;

    #endregion

    #region 相机相关

    //[相机初始位置]
    private float OriginalX;
    private float OriginalY;
    private float OrigionalZ;

    //[相机手感参数]
    [SerializeField] float DelayTime=1f;
    [SerializeField] float CamaraMoveSpeed= 0.1f;
    
    #endregion

    #region 地图相关

    //[地图]：获取所在地图的边界 坐标 从而限制相机边界
    [SerializeField]  GameObject Map;
    [SerializeField] float MapLeftBoundX;
    [SerializeField]  float MapRightBoundX;
    
    #endregion

    #region bool开关

    //[bool开关] 当前状态更新
    private bool StartFollow;

    #endregion
    
    private float PlayerBeforePosX;
    private Vector3 CamaraGoalPos;

    void Start()
    {
        #region 初始化相机的位置
        
        //呱： 一开始先把相机放到玩家位置去 其实只要
        OriginalX = PlayerPos.transform.position.x;
        OriginalY = PlayerPos.transform.position.y;
        OrigionalZ = transform.position.z;
        
        transform.position = new Vector3(OriginalX , OriginalY, OrigionalZ);

        #endregion

        #region 记录玩家上一次的位置

        PlayerBeforePosX = PlayerPos.transform.position.x;

        #endregion
        
        //呱：初始使用一次延迟 也就是让相机等一会再动
        StartCoroutine(OriginalDelay());

    }
    
    
    void Update()
    {
        // 只有延迟结束后才允许相机移动
        if (StartFollow)
        {
            LetCamaraMove();
        }
    }
    
    //呱：这是设置相机目标位置 和 相机移动的函数
    void LetCamaraMove()
    {
        float targetX = PlayerPos.transform.position.x;
        targetX = Mathf.Clamp(targetX, MapLeftBoundX, MapRightBoundX);
        
        CamaraGoalPos = new Vector3(targetX, OriginalY, OrigionalZ);
        transform.position = Vector3.MoveTowards(transform.position, CamaraGoalPos, CamaraMoveSpeed * Time.deltaTime);
    }
    
    //呱：获取上一帧玩家的x坐标
    void LateUpdate()
    {
        PlayerBeforePosX = PlayerPos.transform.position.x;
    }
    
    //呱：这是初始延迟的协程
    IEnumerator OriginalDelay()
    {
        yield return new WaitForSeconds(DelayTime);
        StartFollow = true;
    }
}