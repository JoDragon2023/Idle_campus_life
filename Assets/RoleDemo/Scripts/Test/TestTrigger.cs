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
    private float durOneTime;
    private float durTwoTime;
    private float durThreeTime;

    private float oneTime = 1;
    private float twoTime = 2;
    private float threeTime = 3;
    
    private bool oneEvent = false;
    private bool twoEvent = false;
    private bool threeEvent = false;
    
    private float idleEffectTime = 2.5f;
    private float durEffectTime;
    private RoleAnimator roleAnimator;
    
    private float durupdateTime;
    private int id = 0;
    public Action CreateAct;
    private bool isCreate = false;
    private bool isUpdate = false;
    private bool isEffect = false;
    private Sequence clickTween;
    private  List<TestSequence> sequenceEventList = new List<TestSequence>();

    private float seat1Time = 0.8f;
    private float seat2Time = 0.7f;
    private float seat3Time = 0.6f;
    private float seat4Time = 0.5f;
    
    private float decorate1Time = 0.4f;
    private float decorate2Time = 0.3f;
    private float decorate3Time = 0.3f;
    private float decorate4Time = 0.2f;
    private float decorate5Time = 0.1f;
    private float decorate6Time = 0.1f;
    private float decorate7Time = 0.1f;
    private float decorate8Time = 0.1f;
    private float decorate9Time = 0.1f;
    
    // 烟雾特效显示 效果参数
    private float seatEffect1Time = 0.7f;
    private float seatEffect2Time = 0.65f;
    private float seatEffect3Time = 0.6f;
    private float seatEffect4Time = 0.55f;

    private float decorateEffect1Time = 0.4f;
    private float decorateEffect2Time = 0.3f;
    private float decorateEffect3Time = 0.2f;
    private float decorateEffect4Time = 0.2f;
    private float decorateEffect5Time = 0.13f;
    private float decorateEffect6Time = 0.13f;
    private float decorateEffect7Time = 0.13f;
    private float decorateEffect8Time = 0.13f;
    private float decorateEffect9Time = 0.13f;


    private PlayAnimType currPlayType = PlayAnimType.None;
    
    private void Start()
    {
        
        roleAnimator = transform.GetComponent<RoleAnimator>();
        classRoom.classroom.transform.DOLocalMoveY(-2f, 0.2f);
        isEffect = false;
        for (int i = 0; i < classRoom.classRoomList.Count ; i++)
        {
            classRoom.classRoomList[i].GetComponent<TestSequence>().role = gameObject;
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

    #region 房间 物品显示 处理

    private void Show()
    {
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
                        ShowDecorateEffect();
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

    
     private void ShowEffect()
    {
        //第一个座位出现事件
        transform.DOScale(Vector3.one,  seatEffect1Time).onComplete = () =>
        {
            sequenceEventList[0].ShowEffect();
            //第二个座位出现
            transform.DOScale(Vector3.one, seatEffect2Time).onComplete = () =>
            {
                sequenceEventList[1].ShowEffect();
                //第三个座位出现
                transform.DOScale(Vector3.one, seatEffect3Time).onComplete = () =>
                {
                    sequenceEventList[2].ShowEffect();
                    
                    //第四个座位出现
                    transform.DOScale(Vector3.one, seatEffect4Time ).onComplete = () =>
                    {
                        sequenceEventList[3].ShowEffect();
                    };
                };
            };
        };
        
    }

    /// <summary>
    /// 显示装饰
    /// </summary>
    private void ShowDecorateEffect()
    {
        //第一个座位出现事件
        transform.DOScale(Vector3.one, decorateEffect1Time).onComplete = () =>
        {
            sequenceEventList[4].ShowEffect();
            //第二个座位出现
            transform.DOScale(Vector3.one, decorateEffect2Time).onComplete = () =>
            {
                sequenceEventList[5].ShowEffect();
                //第三个座位出现
                transform.DOScale(Vector3.one, decorateEffect3Time).onComplete = () =>
                {
                    sequenceEventList[6].ShowEffect();
                    
                    //第四个座位出现
                    transform.DOScale(Vector3.one, decorateEffect4Time).onComplete = () =>
                    {
                        sequenceEventList[7].ShowEffect();
                        
                        //第四个座位出现
                        transform.DOScale(Vector3.one, decorateEffect5Time).onComplete = () =>
                        {
                            sequenceEventList[8].ShowEffect();
                        
                            //第四个座位出现
                            transform.DOScale(Vector3.one, decorateEffect6Time).onComplete = () =>
                            {
                                sequenceEventList[9].ShowEffect();
                                //第四个座位出现
                                transform.DOScale(Vector3.one, decorateEffect7Time).onComplete = () =>
                                {
                                    sequenceEventList[10].ShowEffect();
                                    //第四个座位出现
                                    transform.DOScale(Vector3.one, decorateEffect8Time).onComplete = () =>
                                    {
                                        sequenceEventList[11].ShowEffect();
                                        //第四个座位出现
                                        transform.DOScale(Vector3.one, decorateEffect9Time).onComplete = () =>
                                        {
                                            sequenceEventList[12].ShowEffect();
                        
                        
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

    #endregion


    #region 阶梯教室

    private void PlayClassroom()
    {
        classRoom.classroom.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.1f).onComplete+= () =>
        {
            classRoom.classroom.SetActive(true);
            
            if (clickTween != null)
                return;
        
            clickTween = DOTween.Sequence();
        
            Vector3 v3Scale1 = new Vector3(0.5f,0.5f,0.5f);
            Vector3 v3Scale2 = new Vector3(1f,1.4f,1.4f);
            Vector3 v3 = Vector3.one;
        
            float v3Scale1Time = 0.05f;//第一步时间
            float v3Scale2Time = 0.6f;//第二步时间
            float v3Time = 0.3f;//第三步时间
            clickTween.Append(classRoom.classroom.transform.DOScale(v3Scale1, v3Scale1Time));
            clickTween.Append(classRoom.classroom.transform.DOScale(v3, v3Time));
            clickTween.AppendCallback(() =>
            {
                clickTween = null;
            });
            
            classRoom.classroom.transform.DOLocalMoveY(2f, 0.6f).onComplete += () =>
            {
                classRoom.maxSmokeEffect.SetActive(true);
                classRoom.classroom.transform.DOLocalMoveY(0.55f, 0.1f);
            };
        };
        
    }
    #endregion
    
    private void TriggerEvent(PlayAnimType playAnimType, Collider collider)
    {
        if (collider.name == "Cube")
        {
            switch (playAnimType)
            {
                case PlayAnimType.One:
                    if (!oneEvent)
                    {
                        oneEvent = true;
                        classRoom.numAnim.UpdatePanelInfo(true);
                    }
                    break;
                case PlayAnimType.Two:
                    if (!twoEvent)
                    {
                        twoEvent = true;
                        transform.DOScale(Vector3.one, 0.2f).onComplete = () =>
                        {
                            classRoom.floorOne.SetActive(false);
                        };
                        
                        roleAnimator.moneyEffect.gameObject.SetActive(true);
                        transform.DOScale(Vector3.one, 4).onComplete = () =>
                        {
                            roleAnimator.moneyEffect.gameObject.SetActive(false);
                        };
                    }
                    break;
                case PlayAnimType.Three:
                    if (!threeEvent)
                    {
                        threeEvent = true;
                        ShowEffect();
                        Show();
                    }
                    break;
            }
            
        }
        else  if (collider.name == "ClassRoom")
        {
            switch (playAnimType)
            {
                case PlayAnimType.One:
                    if (!oneEvent)
                    {
                        oneEvent = true;
                        classRoom.numAnim.UpdatePanelInfo(false);
                    }
                    break;
                case PlayAnimType.Two:
                    if (!twoEvent)
                    {
                        twoEvent = true;
                        transform.DOScale(Vector3.one, 0.2f).onComplete = () =>
                        {
                            classRoom.floorTwo.SetActive(false);
                        };
                        
                        roleAnimator.moneyEffect.gameObject.SetActive(true);
                        transform.DOScale(Vector3.one, 4).onComplete = () =>
                        {
                            roleAnimator.moneyEffect.gameObject.SetActive(false);
                        };
                    }
                    break;
                case PlayAnimType.Three:
                    if (!threeEvent)
                    {
                        threeEvent = true;
                        PlayClassroom();
                    }
                    break;
            }
        }
    }
    
    
    // 开始接触
    void OnTriggerEnter(Collider collider) {
        
        oneEvent = false;
        twoEvent = false;
        threeEvent = false;
        durOneTime = 0;
        durTwoTime = 0;
        durThreeTime = 0;
        
        if (collider.name == "Cube")
        {
            classRoom.doorAnim.OpenAnim();
        }
    }
    
    // 接触结束
    void OnTriggerExit(Collider collider) {
        durOneTime = 0;
        durTwoTime = 0;
        durThreeTime = 0;
    }
    
    // 接触持续中
    void OnTriggerStay(Collider collider) {

        durOneTime += Time.deltaTime;
        durTwoTime += Time.deltaTime;
        durThreeTime += Time.deltaTime;
        
        if (durOneTime > oneTime)
        { 
            durOneTime = 0;
            TriggerEvent(PlayAnimType.One, collider);
        }
        
        if (durTwoTime > twoTime)
        { 
            durTwoTime = 0;
            TriggerEvent(PlayAnimType.Two, collider);
        }
        
        if (durThreeTime > threeTime)
        { 
            durThreeTime = 0;
            TriggerEvent(PlayAnimType.Three, collider);
        }
        
    }
}
