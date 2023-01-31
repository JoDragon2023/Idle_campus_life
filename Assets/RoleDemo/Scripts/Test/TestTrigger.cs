using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TestTrigger : MonoBehaviour
{
    public ClassRoom classRoom;
    public Action<string> TriggerEnter;
    private float idleTime = 3;
    private float durTime;
    
    private float durupdateTime;
    private int id = 0;
    public Action CreateAct;
    private bool isCreate = false;
    private bool isUpdate = false;

    [FormerlySerializedAs("doorEventAry")] [HideInInspector]
    public TestSequence[] sequenceEventAry;
    
    private void Awake()
    {
        sequenceEventAry =  Resources.FindObjectsOfTypeAll<TestSequence>();
    }

    public void Update()
    {
        if (!isUpdate) return;
        durupdateTime += Time.deltaTime;
        if (durupdateTime >  GameManager.Instance.CreateRoleTime)
        { 
            isUpdate = false;
            durupdateTime = 0;
            SceneObjMgr.Instance.CreateRoleAct();
        }
       
    }

    private void Show()
    {
        for (int i = 0; i < sequenceEventAry.Length; i++)
        {
            sequenceEventAry[i].Show();
        }

        isUpdate = true;
    }
    

    // 开始接触
    void OnTriggerEnter(Collider collider) {
        //TriggerEnter?.Invoke(collider.transform.name);
        Debug.Log("开始接触");
        
        if (collider.name == "Role") return;
        classRoom.doorAnim.OpenAnim();
        durTime = 0;
    }
    
    // 接触结束
    void OnTriggerExit(Collider collider) {
        Debug.Log("接触结束");
        durTime = 0;
    }
    
    // 接触持续中
    void OnTriggerStay(Collider collider) {

        durTime += Time.deltaTime;
        if (durTime > idleTime)
        { 
            Debug.Log("接触持续 3 秒");
            durTime = 0;
            if (id == 0)
            {
                Show();
            }
            id++;
        }
    }
}
