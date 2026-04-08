using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct InsectData//这个玩意作为正式使用的虫虫数据集
{
    public int insectId;
    public string insectName;
    public float insectHP;
    public float insectAtk;
    public float pointsComsumption;
    public string description;
}

public sealed class CageManager : MonoBehaviour
{
    // 单例

    #region

    private static readonly CageManager _instance = new CageManager();

    private CageManager()
    {
    }

    public static CageManager Instance => _instance;

    #endregion

    public GameObject canvas;

    public Dictionary<int, InsectData> insectInCage = new Dictionary<int, InsectData>();

    // 键和值分别为格子id和该格子内存的数据。
    //格子id为for循环生成每个格子时的那个i，具体见cageui
    public List<Button> slotList = new List<Button>(); //格子列表，方便对应格子id,刷新背包
    public InsectData currentChosenData = new InsectData(); //这个玩意用来存当前选中的虫虫数据，想必将来有大用

    public int slotCount = 20;

    private void Awake()
    {
        if (_instance != this && _instance != null)
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            insectInCage.Add(i, new InsectData());
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    // 增加昆虫
    public void AddInsect(InsectData insectData)
    {
        foreach (var kvp in insectInCage)
        {
            if (kvp.Value.insectId == 0) //  insectId 为 0 表示空格子
            {
                insectInCage[kvp.Key] = insectData;
                refreshSlot(kvp.Key);
                return;
            }
        }
    }

    public void AddInsect(InsectDataSO insectData) //这是一个重载，可以直接传so文件。
        //后续如果方便的话可能会将所有等级数据全部写成so
    {
        InsectData data = new InsectData();
        data.insectId = insectData.insectId;
        data.insectAtk = insectData.insectAtk;
        data.insectHP = insectData.insectHP;
        data.insectName = insectData.insectName;
        data.description = insectData.description;
        data.pointsComsumption = insectData.pointsComsumption;
        foreach (var kvp in insectInCage)
        {
            if (kvp.Value.insectId == 0) // 假设 insectId 为 0 表示空格子
            {
                insectInCage[kvp.Key] = data;
                refreshSlot(kvp.Key);
                return;
            }
        }
    }

    // 删除昆虫
    public void RemoveInsect(int slotID)
    {
        if (insectInCage.ContainsKey(slotID))
        {
            insectInCage[slotID] = new InsectData(); // 清空数据
            refreshSlot(slotID);
        }
    }

    // 查询昆虫
    public InsectData GetInsect(int slotID)
    {
        if (insectInCage.ContainsKey(slotID))
        {
            return insectInCage[slotID];
        }
        else
        {
            return new InsectData();
        }
    }

    // 修改昆虫
    public void UpdateInsect(int slotID, InsectData newInsectData)
    {
        if (insectInCage.ContainsKey(slotID))
        {
            insectInCage[slotID] = newInsectData;
            refreshSlot(slotID);
        }
    }

    public void refreshSlot(int slotid) //更新格子ui
    {
        CageSlot cageSlot = slotList[slotid].GetComponent<CageSlot>();
        cageSlot.refreshSlot();
    }

    
}
