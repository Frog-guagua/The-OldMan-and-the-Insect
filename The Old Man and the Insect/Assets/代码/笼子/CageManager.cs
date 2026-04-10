using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsectData//这个玩意作为正式使用的虫虫数据集
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

public sealed class CageManager : MonoBehaviour
{
    // 单例

    #region

    private static CageManager _instance;

    public static CageManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CageManager>();
                if (_instance == null)
                {
                    var singletonObject = new GameObject("CageManagerSingleton");
                    _instance = singletonObject.AddComponent<CageManager>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    #endregion

   

    public Dictionary<int, InsectData> insectInCage = new Dictionary<int, InsectData>();
    
    private List<InsectData> _insectDataList; // 私有字段用于实际存储insectDataList

    // 新增属性，用于当访问insectDataList时动态更新其内容
    public List<InsectData> insectDataList//这个列表是insectdata纯享，与dictionary实时同步
    {
        get
        {
            // 在这里更新_insectDataList，使其与insectInCage的内容一致
            _insectDataList = new List<InsectData>(insectInCage.Values);
            return _insectDataList;
        }
       
    }

    // 键和值分别为格子id和该格子内存的数据。
    //格子id为for循环生成每个格子时的那个i，具体见cageui
    public List<Button> slotList = new List<Button>(); //格子列表，方便对应格子id,刷新背包
    public InsectData currentChosenData = new InsectData(); //这个玩意用来存当前选中的虫虫数据，想必将来有大用

    public int slotCount = 20;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
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
        data.Image = insectData.Image;
        data.insectAtklevel=insectData.insectAtklevel;
        data.insetHplevel = insectData.insetHplevel;
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
   
    public void ReplaceInsects(List<InsectData> newInsects)
    {
        // 首先清空现有的昆虫
        insectInCage.Clear();

        // 重新填充昆虫数据
        for (int i = 0; i < slotCount; i++)
        {
            insectInCage.Add(i, newInsects[i]);
        }
    }
}
