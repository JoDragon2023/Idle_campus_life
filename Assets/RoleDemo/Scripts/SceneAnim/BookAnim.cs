using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookAnim : MonoBehaviour
{
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
        if (currRoleAnimatorStateInfo.IsName(BookAnimName.Note01.ToString()))
        {
            animator.SetInteger(BookTypeName.CurrState.ToString(), (int)BookType.One);
        }
        
        if (currRoleAnimatorStateInfo.IsName(BookAnimName.Note02.ToString()))
        {
            animator.SetInteger(BookTypeName.CurrState.ToString(), (int)BookType.Two);
        }
        
        if (currRoleAnimatorStateInfo.IsName(BookAnimName.Note03.ToString()))
        {
            animator.SetInteger(BookTypeName.CurrState.ToString(), (int)BookType.Three);
        }
    }

    
    public void PlayAnim(BedAnimatorName bedAnimatorName)
    {
        switch (bedAnimatorName)
        {
            case BedAnimatorName.BedOne:
                animator.SetBool(BookTypeName.ToBookOne.ToString(), true);
                break;
            case BedAnimatorName.BedTwo:
                animator.SetBool(BookTypeName.ToBookTwo.ToString(), true);
                break;
            case BedAnimatorName.BedThree:
                animator.SetBool(BookTypeName.ToBookThree.ToString(), true);
                break;
        }
        
    }
}
