using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedAnim : MonoBehaviour
{
    public EventRandomPath eventRandomPath;
    /// <summary>
    /// 当前角色动画
    /// </summary>
    public Animator animator;
    /// <summary>
    /// 当前动画状态信息
    /// </summary>
    private AnimatorStateInfo currRoleAnimatorStateInfo { get; set; }

    public void UpdateBedAnim()
    {
        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currRoleAnimatorStateInfo.IsName(BedAnimatorName.BedOne.ToString()))
        {
            animator.SetInteger(BedAnimatorCondition.CurrState.ToString(), (int)BedAnimState.BedOne);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                animator.SetBool(BedAnimatorCondition.ToBedOne.ToString(), false);
            }
        }
        
        if (currRoleAnimatorStateInfo.IsName(BedAnimatorName.BedTwo.ToString()))
        {
            animator.SetInteger(BedAnimatorCondition.CurrState.ToString(), (int)BedAnimState.BedTwo);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                animator.SetBool(BedAnimatorCondition.ToBedTwo.ToString(), false);
            }
        }
        
        if (currRoleAnimatorStateInfo.IsName(BedAnimatorName.BedThree.ToString()))
        {
            animator.SetInteger(BedAnimatorCondition.CurrState.ToString(), (int)BedAnimState.BedThree);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                animator.SetBool(BedAnimatorCondition.ToBedThree.ToString(), false);
            }
        }
    }

    
    public void PlayAnim(BedAnimatorName bedAnimatorName)
    {
        switch (bedAnimatorName)
        {
            case BedAnimatorName.BedOne:
                animator.SetBool(BedAnimatorCondition.ToBedOne.ToString(), true);
                break;
            case BedAnimatorName.BedTwo:
                animator.SetBool(BedAnimatorCondition.ToBedTwo.ToString(), true);
                break;
            case BedAnimatorName.BedThree:
                animator.SetBool(BedAnimatorCondition.ToBedThree.ToString(), true);
                break;
        }
        
    }
}
