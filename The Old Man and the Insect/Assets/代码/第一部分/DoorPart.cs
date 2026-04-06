using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorPart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collision2D other)
    {   
        print("door");
        if (other.gameObject.CompareTag("Player"))
        {   //这里大概率需要完善，加入动画等
            LevelStateManager.Instance.SwitchState(LevelState.dialogue);
        }
    }
}
