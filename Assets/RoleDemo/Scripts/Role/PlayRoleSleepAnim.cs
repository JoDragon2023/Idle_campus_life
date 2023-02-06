using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 播放 睡觉动画处理
/// </summary>
public class PlayRoleSleepAnim : MonoBehaviour
{
    private AnimatorStateInfo currRoleAnimatorStateInfo { get; set; }
    private Animator animator;
    private BedAnim bedAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        EnterSleepAct();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSleepAct(Time.deltaTime);
    }
    
    private string loopAnimNameStr;
    private string loopAnimName;
    private string loopIdleAnimName;
    private int loopAnimState;
    
    private bool isSleepOne = false;
    private bool isSleepTwo = false;
    private bool isSleepThree = false;
    private bool isUpdateSleep = false;
    private bool isStartSleep = false;

    private Vector3[] sleepPath;
    private bool animIdleOne = false;
    private bool animIdleTwo = false;
    private bool isLoopAnim;
    
    private float durTime;
    
    //上床 等待 播放 躺着 前 时间
    private float sleepOneTime = 4;

    //上床 等待 播放 躺着 后 时间
    private float sleepTwoTime = 4;
    //继续播放动画 间隔时间
    private float loopAnimTime = 1f;
    
    public GameObject di;
    
    public void EnterSleepAct()
    {
        isSleepOne = false;
        isSleepTwo = false;
        isSleepThree = false;
        isUpdateSleep = false;
        isStartSleep = false;
      
        animIdleOne = false;
        animIdleTwo = false;
        loopAnimNameStr = RoleAnimatorName.SleepTwo.ToString();
        loopAnimName = ToAnimatorCondition.ToSleepTwo.ToString();
        loopIdleAnimName = ToAnimatorCondition.ToSleepIdle.ToString();
        loopAnimState = (int)RoleAniState.SleepTwo;
        isLoopAnim = false;
        
        EnterSleep();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void UpdateSleepAct(float deltaTime)
    {
        if (bedAnim != null)
            bedAnim.UpdateBedAnim();

        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.SleepOne.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.SleepOne);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isSleepOne)
                {
                    isSleepOne = true;
                    animIdleOne = true;
                    animator.SetBool(ToAnimatorCondition.ToSleepOne.ToString(), false);
                    animator.SetBool(loopIdleAnimName, true);
                }
            }
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.SleepThree.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.SleepThree);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isSleepThree)
                {
                    //第五步，播放下床动画结束 退出房间，
                    isSleepThree = true;
                    animator.SetBool(ToAnimatorCondition.ToSleepThree.ToString(), false);
                    di.SetActive(true);
                    LoopPlayAnim();
                }
            }
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Walk_01.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Run);
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.SleepIdle.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.SleepIdle);
        }

        LoopRoleSleepAnim(deltaTime);
    }
    
    
    /// <summary>
    /// 重复播放动画
    /// </summary>
    /// <param name="deltaTime"></param>
    private void LoopRoleSleepAnim(float deltaTime)
    {
        if (currRoleAnimatorStateInfo.IsName(loopAnimNameStr))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), loopAnimState);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isLoopAnim)
                {
                    durTime = 0;
                    isLoopAnim = true;
                    animIdleTwo = true;
                    //第四步  关闭上课动画  播放待机动画 
                    animator.SetBool(loopAnimName, false);
                    animator.SetBool(loopIdleAnimName, true);
                    if (bedAnim != null)
                        bedAnim.PlayAnim(BedAnimatorName.BedTwo);
                }
            }
        }

        if (animIdleOne)
        {
            durTime += deltaTime;
            if (durTime > sleepOneTime)
            {
                animIdleOne = false;
                isLoopAnim = false;
                durTime = 0;
                animator.SetBool(loopIdleAnimName, false);
                animator.SetBool(loopAnimName, true);
                if (bedAnim != null)
                    bedAnim.PlayAnim(BedAnimatorName.BedTwo);
            }
        }


        if (animIdleTwo)
        {
            durTime += deltaTime;
            if (durTime > sleepTwoTime)
            {
                animIdleTwo = false;
                durTime = 0;
                animator.SetBool(loopIdleAnimName, false);
                animator.SetBool(loopAnimName, false);
                //第四步，床上待机完毕，开始 播放下床动画，
                animator.SetBool(ToAnimatorCondition.ToSleepThree.ToString(), true);
                if (bedAnim != null)
                    bedAnim.PlayAnim(BedAnimatorName.BedThree);
            }
        }
    }
    
    private void EnterSleep()
    {
        //第一步 播放上床动画
        animator.SetBool(ToAnimatorCondition.ToSleepOne.ToString(), true);
        di.SetActive(false);
        if (bedAnim != null)
            bedAnim.PlayAnim(BedAnimatorName.BedOne);
    }

    private void LoopPlayAnim()
    {
        transform.DOScale(Vector3.one, loopAnimTime).onComplete = () => { EnterSleepAct();};
    }
}
