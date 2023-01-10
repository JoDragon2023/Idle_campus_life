using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pathfinding;
using UnityEngine;

public enum MoveType
{
    None,
    One,
    Two
}

public class TestRoleMove : MonoBehaviour
{
    public Animator animator;
    public Transform[] testTransforms;
    private Vector3[] testTransformArry;
    private AnimatorStateInfo currRoleAnimatorStateInfo { get; set; }
    public Transform target;
    // public Transform model;
    private bool isTest;
    private float distance;
    private MoveType currMoveType = MoveType.One;
    private bool isDistance = false;
    
    private CharacterController characterController;
    private SimpleSmoothModifier simpleSmoothModifier ;
    private AIDestinationSetter aiDestination ;
    private AIPath aiPath;
    
    private bool isEntryToilet = false;
    private bool isleaveToilet = false;
    private bool isThree = false;
    
    // Start is called before the first frame update
    void Start()
    {
        testTransformArry = new Vector3[testTransforms.Length];
        for (int i = 0; i < testTransforms.Length; i++)
        {
            testTransformArry[i] = testTransforms[i].position;
        }
    }

    private void Awake()
    {
        LoadGoCallBack();
    }


    private void LoadGoCallBack()
    {
        characterController = transform.GetComponent<CharacterController>();
        simpleSmoothModifier = transform.GetComponent<SimpleSmoothModifier>();
        aiDestination = transform.GetComponent<AIDestinationSetter>();
        aiPath = transform.GetComponent<AIPath>();
       
    }

    private void SetAIComponent(bool isEnabled)
    {
        characterController.enabled = isEnabled;
        simpleSmoothModifier.enabled = isEnabled;
        aiDestination.enabled = isEnabled;
        aiPath.enabled = isEnabled;
    }
    

    private void LookAtForward(int angle)
    {
        //随机一个待机方向
        // int angle = Mgr.Time.ServerTimestamp % 2 == 1 ? 200 : 145;
        transform.rotation = Quaternion.Euler(0, angle, 0);
        //transform.forward = - Vector3.forward;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            target.position = testTransformArry[0];
            isDistance = true;
            Debug.Log("  KeyCode.A  ");
        }
        
        distance = (transform.position - target.transform.position).magnitude;
        if (distance < 0.1f)
        {
            if (isDistance)
            {
                isDistance = false;
                isEntryToilet = false;
                Debug.Log("  isDistance  ");
                //animator.SetBool(ToAnimatorCondition.ToToilet_GetUp.ToString(), true);
                animator.SetBool(ToAnimatorCondition.ToOpenDoor_Toilet.ToString(), true);
            }
        }
        
        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.ToiletOne.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Toilet);
            Debug.Log("  ToiletOne  ");
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
               
                if (!isEntryToilet)
                { 
                    Debug.Log("  ToiletOne  normalizedTime ");
                    isEntryToilet = true;
                    animator.SetBool(ToAnimatorCondition.ToOpenDoor_Toilet.ToString(), false);
                    animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
                    SetAIComponent(false);
                    EntryToilet();
                }
            }
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.ToiletTwo.ToString()))
        {
            Debug.Log("  ToiletTwo  ");
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Toilet);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isThree)
                {
                    Debug.Log("  ToiletTwo normalizedTime ");
                    isThree = true;
                    animator.SetBool(ToAnimatorCondition.ToToilet.ToString(), false);
                    animator.SetBool(ToAnimatorCondition.ToToilet_GetUp.ToString(), true);
                }
               
            }
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.ToiletThree.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.ToiletGetUP);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isleaveToilet)
                {
                    isleaveToilet = true;
                    animator.SetBool(ToAnimatorCondition.ToToilet_GetUp.ToString(), false);
                    animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
                    leaveToilet();
                    
                }
            }
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Walk_01.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Run);
        }
       
    }

    private void RotateComplete()
    {
        
        
    }

    private void leaveToilet()
    {
        Vector3[] entryToilet = new Vector3[1];
        entryToilet[0] = testTransformArry[0];
        transform.DOPath(entryToilet, 0.5f, PathType.CatmullRom).SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            //开启 寻路
            //前往Idle 状态
            //随机点，应该是 全是Idle 状态
        };
        
    }

    /// <summary>
    /// 进入厕所
    /// </summary>
    private void EntryToilet()
    {
        Debug.Log("  EntryToilet  ");
        Vector3[] entryToilet = new Vector3[1];
        entryToilet[0] = testTransformArry[testTransformArry.Length - 1];
         transform.DOPath(entryToilet, 0.5f, PathType.CatmullRom).SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
         {
             Debug.Log("  onComplete  DOPath ");
             transform.DORotate(new Vector3(0, 180, 0), 0.2f, RotateMode.Fast).onComplete = () =>
             {
                 animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
                 animator.SetBool(ToAnimatorCondition.ToToilet.ToString(), true);
                 Debug.Log("  onComplete  DORotate ");
             };

         };
    }
    
    
    
}
