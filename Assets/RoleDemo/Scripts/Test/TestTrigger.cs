using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    private  List<TestSequence> sequenceEventList = new List<TestSequence>();

    private float seat1Time = 1;
    private float seat2Time = 1;
    private float seat3Time = 1;
    private float seat4Time = 1;
    
    private float decorate1Time = 0.5f;
    private float decorate2Time = 0.5f;
    private float decorate3Time = 0.5f;
    private float decorate4Time = 0.5f;
    private float decorate5Time = 0.5f;
    private float decorate6Time = 0.5f;
    private float decorate7Time = 0.5f;
    private float decorate8Time = 0.5f;
    private float decorate9Time = 0.5f;
    
    private void Start()
    {
        for (int i = 0; i < classRoom.classRoomList.Count ; i++)
        {
            sequenceEventList.Add(classRoom.classRoomList[i].GetComponent<TestSequence>());
        }
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
        // for (int i = 0; i < sequenceEventList.Count; i++)
        // {
        //     sequenceEventList[i].Show();
        // }

        //第一个座位出现事件
        transform.DOScale(Vector3.one, seat1Time).onComplete = () =>
        {
            sequenceEventList[0].Show();
            //第二个座位出现
            transform.DOScale(Vector3.one, seat2Time).onComplete = () =>
            {
                sequenceEventList[1].Show();
                //第三个座位出现
                transform.DOScale(Vector3.one, seat3Time).onComplete = () =>
                {
                    sequenceEventList[2].Show();
                    
                    //第四个座位出现
                    transform.DOScale(Vector3.one, seat4Time).onComplete = () =>
                    {
                        sequenceEventList[3].Show();
                    
                        //第四个座位 出现
                        isUpdate = true;

                        ShowDecorate();
                    };
                };
            };
        };
        
    }

    /// <summary>
    /// 显示装饰
    /// </summary>
    private void ShowDecorate()
    {
        //第一个座位出现事件
        transform.DOScale(Vector3.one, decorate1Time).onComplete = () =>
        {
            sequenceEventList[4].Show();
            //第二个座位出现
            transform.DOScale(Vector3.one, decorate2Time).onComplete = () =>
            {
                sequenceEventList[5].Show();
                //第三个座位出现
                transform.DOScale(Vector3.one, decorate3Time).onComplete = () =>
                {
                    sequenceEventList[6].Show();
                    
                    //第四个座位出现
                    transform.DOScale(Vector3.one, decorate4Time).onComplete = () =>
                    {
                        sequenceEventList[7].Show();
                        
                        //第四个座位出现
                        transform.DOScale(Vector3.one, decorate5Time).onComplete = () =>
                        {
                            sequenceEventList[8].Show();
                        
                            //第四个座位出现
                            transform.DOScale(Vector3.one, decorate6Time).onComplete = () =>
                            {
                                sequenceEventList[9].Show();
                                //第四个座位出现
                                transform.DOScale(Vector3.one, decorate7Time).onComplete = () =>
                                {
                                    sequenceEventList[10].Show();
                                    //第四个座位出现
                                    transform.DOScale(Vector3.one, decorate8Time).onComplete = () =>
                                    {
                                        sequenceEventList[11].Show();
                                        //第四个座位出现
                                        transform.DOScale(Vector3.one, decorate9Time).onComplete = () =>
                                        {
                                            sequenceEventList[12].Show();
                        
                        
                                        };
                                    };
                                };
                            };
                        };
                    };
                };
            };
        };
        
        
    }
    
    // 开始接触
    void OnTriggerEnter(Collider collider) {
        //TriggerEnter?.Invoke(collider.transform.name);
        //Debug.Log("开始接触");
        
        if (collider.name == "Role") return;
        classRoom.doorAnim.OpenAnim();
        durTime = 0;
    }
    
    // 接触结束
    void OnTriggerExit(Collider collider) {
        //Debug.Log("接触结束");
        durTime = 0;
    }
    
    // 接触持续中
    void OnTriggerStay(Collider collider) {

        durTime += Time.deltaTime;
        if (durTime > idleTime)
        { 
            //Debug.Log("接触持续 3 秒");
            durTime = 0;
            if (id == 0)
            {
                Show();
            }
            id++;
        }
    }
}
