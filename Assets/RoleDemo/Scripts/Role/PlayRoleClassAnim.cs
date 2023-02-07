using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 阶梯教室 上课动画
/// </summary>
public class PlayRoleClassAnim : MonoBehaviour
{
    public EventRandomPath eventRandomPath;
    private AnimatorStateInfo currRoleAnimatorStateInfo { get; set; }
    private Animator animator;
    private float startTime = 0.2f;
    private bool isSleepOne;
    private string bookAnimName = "";
    private RoleAnimator roleAnimator;

    private float bookShowTime = 0.5f;
    private GameObject bookObj;
    private BookAnim bookAnim;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        roleAnimator = transform.GetComponent<RoleAnimator>();
        bookObj = roleAnimator.book;
        bookAnim = bookObj.transform.GetComponent<BookAnim>();
        isSleepOne = false;
        switch (eventRandomPath)
        {
            case EventRandomPath.Path1:
                startTime = 0.6f;
                break;
            case EventRandomPath.Path2: 
                startTime = 0.8f;
                break;
            case EventRandomPath.Path3:
                startTime = 1f;
                break;
            case EventRandomPath.Path4:
                startTime = 1.2f;
                bookShowTime = 0.6f;
                break;
        }

        transform.DOScale(Vector3.one, startTime).onComplete = () => { PlayAnim();};
        
        //第一步 播放 掏 书动作 ，第二步 ，播放 写字 动画。
        
    }

    private void PlayAnim()
    {
        animator.SetBool(RoleClassTypeName.ToTakeout.ToString(), true);
        transform.DOScale(Vector3.one, bookShowTime).onComplete = () => { bookObj.gameObject.SetActive(true); };
    }
    //3213
    
    // Update is called once per frame
    void Update()
    {
        if (bookAnim != null)
        {
            bookAnim.UpdateBedAnim();
        }
        
        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currRoleAnimatorStateInfo.IsName(RoleClassAnimName.Takeout.ToString()))
        {
            animator.SetInteger(RoleClassTypeName.CurrState.ToString(), (int)BookType.Four);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isSleepOne)
                {
                    isSleepOne = true;
                    animator.SetBool(RoleClassTypeName.ToTakeout.ToString(), false);
                    switch (eventRandomPath)
                    {
                        case EventRandomPath.Path1:
                            animator.SetBool(RoleClassTypeName.ToStudy03.ToString(), true);
                            bookAnim.PlayAnim(BedAnimatorName.BedThree);
                            break;
                        case EventRandomPath.Path2: 
                            animator.SetBool(RoleClassTypeName.ToStudy02.ToString(), true);
                            bookAnim.PlayAnim(BedAnimatorName.BedTwo);
                            break;
                        case EventRandomPath.Path3:
                            animator.SetBool(RoleClassTypeName.ToStudy03.ToString(), true);
                            bookAnim.PlayAnim(BedAnimatorName.BedThree);
                            break;
                        case EventRandomPath.Path4:
                            animator.SetBool(RoleClassTypeName.ToStudy01.ToString(), true);
                            bookAnim.PlayAnim(BedAnimatorName.BedOne);
                            break;
                    }
                }
            }
        }
        
        
        if (currRoleAnimatorStateInfo.IsName(RoleClassAnimName.Study01.ToString()))
        {
            animator.SetInteger(RoleClassTypeName.CurrState.ToString(), (int)BookType.One);
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleClassAnimName.Study02.ToString()))
        {
            animator.SetInteger(RoleClassTypeName.CurrState.ToString(), (int)BookType.Two);
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleClassAnimName.Study03.ToString()))
        {
            animator.SetInteger(RoleClassTypeName.CurrState.ToString(), (int)BookType.Three);
        }
    }
}
