using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaboratoryAnim : MonoBehaviour
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
    
    private bool isStart = false;
    
    public void UpdateBedAnim()
    {
        if (isStart)
        {
            currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (currRoleAnimatorStateInfo.IsName("Microscope"))
            {
                if (currRoleAnimatorStateInfo.normalizedTime > 1)
                {
                    isStart = false;
                    animator.enabled = false;
                }
            }
        
            if (currRoleAnimatorStateInfo.IsName("Science_center_Hammer"))
            {
                if (currRoleAnimatorStateInfo.normalizedTime > 1)
                {
                    isStart = false;
                    animator.enabled = false;
                }
            }
        }
    }

    
    public void PlayAnim()
    {
        animator.enabled = true;
    }
    
    public void CloseAnim()
    {
        animator.enabled = false;
    }
}
