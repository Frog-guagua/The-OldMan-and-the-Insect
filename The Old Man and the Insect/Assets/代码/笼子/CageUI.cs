using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CageUI : MonoBehaviour
{   
    public Button slot;
    
    public int slotCount = 20;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            Button newslot = Instantiate(slot, this.transform);
            newslot.transform.SetParent(this.transform);
            CageSlot cageSlot=newslot.GetComponent<CageSlot>();
            cageSlot.slotID = i;
            cageSlot.Data = new InsectData();
            cageSlot.Data.insectId = 0;//0代表空
            CageManager.Instance.slotList.Add(newslot);
            //生成背包格子

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void slotOnClick()//处理点击逻辑
    {
        
    }
}
