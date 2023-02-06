using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayRoleToiletAnim : MonoBehaviour
{
    public EventRandomPath eventRandomPath;
    private float intervalTime = 0.2f;
    //继续播放动画 间隔时间
    private float loopAnimTime = 2f;
    private AnimatorStateInfo currRoleAnimatorStateInfo { get; set; }
    private Animator animator;

    private float startTime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        animator.SetBool(ToAnimatorCondition.ToStand.ToString(), true);
        switch (eventRandomPath)
        {
            case EventRandomPath.Path2:
                startTime = 0.5f;
                break;
            case EventRandomPath.Path3:
                startTime = 1f;
                break;
        }
        
        transform.DOScale(Vector3.one, startTime).onComplete = () => { EnterToiletAct();};
    }

    // Update is called once per frame
    void Update()
    {
        UpdateToiletAct(Time.deltaTime);
    }
    
    private float durTime;
    private bool animIdleOne = false;
    private bool isToiletOne = false;
    private bool isToiletTwo = false;
    private bool isToiletThree = false;
    private Vector3[] toiletPath;
    private int rotate = 180;
    private ToiletDoorAnim toiletDoorAnim;
    // 上厕所 蹲坑 时间
    private int nextToiletTime = 10;
    private GameObject toiletBathe;

    public void EnterToiletAct()
    {
        isToiletOne = false;
        isToiletTwo = false;
        isToiletThree = false;
        animIdleOne = false;
        EnterToilet();
    }

    public void UpdateToiletAct(float deltaTime)
    {
        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.ToiletOne.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Toilet);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isToiletOne)
                {
                    isToiletOne = true;
                    animator.SetBool(ToAnimatorCondition.ToOpenDoor_Toilet.ToString(), false);
                    animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
                    EntryToiletAnim();
                }
            }
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Stand.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Stand);
        }
        

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.ToiletTwo.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Toilet);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isToiletTwo)
                {
                    isToiletTwo = true;
                    animIdleOne = true;
                    ShowToiletBatheEffect();
                }
            }
        }

        if (animIdleOne)
        {
            durTime += deltaTime;
            if (durTime > nextToiletTime)
            {
                animIdleOne = false;
                durTime = 0;
                animator.SetBool(ToAnimatorCondition.ToToilet.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToToilet_GetUp.ToString(), true);
                if (toiletDoorAnim != null)
                    toiletDoorAnim.OpenAnim(true);
            }
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.ToiletThree.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.ToiletGetUP);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isToiletThree)
                {
                    isToiletThree = true;
                    animator.SetBool(ToAnimatorCondition.ToToilet_GetUp.ToString(), false);
                    animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
                    if (toiletDoorAnim != null)
                        toiletDoorAnim.CloseAnim(true);

                    leaveToilet();
                }
            }
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Walk_01.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Run);
        }
    }

    private void leaveToilet()
    {
        CloseToiletBatheEffect();
        
        transform.DOPath(ScenePath.Instance.GetEventToiletActPointTwo(toiletPath), 0.5f, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
            LoopPlayAnim();
        };
    }
    
    private void LoopPlayAnim()
    {
        transform.DORotate(Vector3.zero, 0.2f, RotateMode.Fast).onComplete = () =>
        {
            animator.SetBool(ToAnimatorCondition.ToStand.ToString(), true);
            transform.DOScale(Vector3.one, loopAnimTime).onComplete = () => { EnterToiletAct();};
        };
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private void EnterToilet()
    {
        toiletBathe = ToiletBatheEffect.Instance.GetEffect(RandomEvent.Event8, eventRandomPath);
        toiletDoorAnim = GameManager.Instance.GetToiletDoorAnim(RandomEvent.Event8, eventRandomPath);
        toiletPath = ScenePath.Instance.GetEvent8ToiletPath(eventRandomPath);
        
        animator.SetBool(ToAnimatorCondition.ToStand.ToString(), false);
        animator.SetBool(ToAnimatorCondition.ToOpenDoor_Toilet.ToString(), true);
        if (toiletDoorAnim != null)
        {
            toiletDoorAnim.OpenAnim();
        }
    }

    /// <summary>
    /// 进入厕所
    /// </summary>
    private void EntryToiletAnim()
    {
        rotate = 180;
        transform.DOPath(ScenePath.Instance.GetEventToiletActPoint(toiletPath), 0.5f, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            if (toiletDoorAnim != null)
                toiletDoorAnim.CloseAnim();

            transform.DORotate(new Vector3(0, rotate, 0), 0.2f, RotateMode.Fast).onComplete = () =>
            {
                animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToToilet.ToString(), true);
            };
        };
    }

    private void ShowToiletBatheEffect()
    {
        if (toiletBathe != null)
        {
            toiletBathe.SetActive(true);
        }
    }
    
    private void CloseToiletBatheEffect()
    {
        if (toiletBathe != null)
        {
            toiletBathe.SetActive(false);
        }
    }
}
