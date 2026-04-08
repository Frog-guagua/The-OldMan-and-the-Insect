using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CageUI : MonoBehaviour
{
    private static CageUI _instance; // 静态变量，用于保存唯一实例

    public Button slot;

    public int slotCount = 20;

    // 私有构造函数，防止外部直接调用构造函数
    private CageUI() { }

    // 提供一个公共的静态属性，以便其他类可以访问这个实例
    public static CageUI Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("CageUI instance is not initialized yet.");
            }
            
            return _instance;
        }
    }

    // 在Awake方法中初始化实例
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // 保证实例不会被销毁
        }
       
        setInactive();
    }

    // Start is called before the first frame update
    void Start()
    {    Image image=this.GetComponent<Image>();
        Color color = image.color;
        color.a = 1f;
        image.color = color;
        for (int i = 0; i < slotCount; i++)
        {
            Button newslot = Instantiate(slot, this.transform);
            newslot.transform.SetParent(this.transform);
            CageSlot cageSlot = newslot.GetComponent<CageSlot>();
            cageSlot.slotID = i;
            cageSlot.Data = new InsectData();
            cageSlot.Data.insectId = 0; // 0代表空
            CageManager.Instance.slotList.Add(newslot);
            //生成背包格子
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void slotOnClick() // 处理点击逻辑
    {

    }

    public void setAct() { this.gameObject.SetActive(true); }
    public void setInactive() { this.gameObject.SetActive(false); }
}