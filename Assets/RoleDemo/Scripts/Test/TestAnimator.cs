using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimator : MonoBehaviour
{
    public Animator animator;
    private AnimatorStateInfo currRoleAnimatorStateInfo { get; set; }
    private bool isTest = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //var randomDic = ScenePoint.Instance.GetIdleAndEvenRandom();
        //Test11();
    }

    private void Test11()
    {
        // var bathePath = ScenePath.Instance.GetEvent9BathePath();
        // var leaveBathePos = ScenePath.Instance.GetEventToiletActPath(bathePath);
        // ScenePath.Instance.GetEventToiletActPoint(leaveBathePos);
    }
    
    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     PlayAnim("Toilet");
        // }
        // else if (Input.GetKeyDown(KeyCode.B))
        // {
        //     PlayAnim("132132");
        // } else if (Input.GetKeyDown(KeyCode.C))
        // {
        //     PlayAnim("B2");
        // }
       
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool(ToAnimatorCondition.ToSwitchDoor.ToString(),true);
            isTest = true;
        }
        
        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        
        if (currRoleAnimatorStateInfo.IsName("OpenSwitchDoor"))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.SwitchDoor);
            Debug.Log("  OpenSwitchDoor ");
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                Debug.Log("  OpenSwitchDoor normalizedTime ");
                //animator.SetBool(ToAnimatorCondition.ToSitdown.ToString(),false);
               
            }
        }
        
        if (currRoleAnimatorStateInfo.IsName("SwitchDoor"))
        {
            Debug.Log("  SwitchDoor ");
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.SwitchDoor);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                Debug.Log("  SwitchDoor normalizedTime ");
                //animator.SetBool(ToAnimatorCondition.ToSitdown.ToString(),false);
               
            }
        }
        
        if (currRoleAnimatorStateInfo.IsName("CloseSwitchDoor"))
        {
            Debug.Log("  CloseSwitchDoor ");
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.SwitchDoor);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                Debug.Log("  CloseSwitchDoor normalizedTime ");
                animator.SetBool(ToAnimatorCondition.ToSwitchDoor.ToString(),false);
                //animator.SetBool(ToAnimatorCondition.ToSitdown.ToString(),false);
               
            }
        }
        
    }

    
    public void PlayAnim(string curAnim)
    {
        if (animator == null)
            return;

        animator.CrossFade(curAnim, 0.2f);
    }
}