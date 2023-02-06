using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoleAnim : MonoBehaviour
{
    public RandomEventAct randomEventAct;
    private AnimatorStateInfo currRoleAnimatorStateInfo { get; set; }
    private Animator animator;
    private BedAnim bedAnim;
    
    private void Awake()
    {
        animator = transform.GetComponentInChildren<Animator>();
    }

    void Start()
    {
        GameManager.Instance.TeacherAttendAct += TeacherAttendAct;
        GameManager.Instance.StudentAttendAct += StudentAttendAct;
        GameManager.Instance.AttendEndAct += AttendEndAct;
        GameManager.Instance.LoopAttendAct += LoopAttendAct;
        
        GameManager.Instance.ClassroomOneAct += ClassroomOneAct;
        GameManager.Instance.ClassroomTwoAct += ClassroomTwoAct;
        GameManager.Instance.ClassroomThreeAct += ClassroomThreeAct;
        GameManager.Instance.ClassroomForAct += ClassroomForAct;
        
        PlayAnim();
    }
    private void ClassroomOneAct()
    {
        if (randomEventAct == RandomEventAct.Event2AttendClass)
        {
            if (transform.name == "Role3")
            {
                animator.SetBool(ToAnimatorCondition.ToStudyIdle.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToStudy.ToString(), true);
            }
        }
    }
    
    private void ClassroomTwoAct()
    {
        if (randomEventAct == RandomEventAct.Event2AttendClass)
        {
            if (transform.name == "Role5")
            {
                animator.SetBool(ToAnimatorCondition.ToStudyIdle.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToStudy.ToString(), true);
            }
        }
    }
    
    private void ClassroomThreeAct()
    {
        if (randomEventAct == RandomEventAct.Event2AttendClass)
        {
            if (transform.name == "Role1")
            {
                animator.SetBool(ToAnimatorCondition.ToStudyIdle.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToStudy.ToString(), true);
            }
        }
    }
    
    private void ClassroomForAct()
    {
        if (randomEventAct == RandomEventAct.Event2AttendClass)
        {
            if (transform.name == "Role4")
            {
                animator.SetBool(ToAnimatorCondition.ToStudyIdle.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToStudy.ToString(), true);
            }
        }
    }

    private void TeacherAttendAct()
    {
        if (randomEventAct == RandomEventAct.Event4AttendClass)
        {
            if (transform.name == "Role2")
            {
                animator.SetBool(ToAnimatorCondition.ToStudyCourseIdle.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToStudy.ToString(), true);
            }
        }
    }

    private void StudentAttendAct()
    {
        if (randomEventAct == RandomEventAct.Event4AttendClass)
        {
            if (transform.name == "Role4")
            {
                animator.SetBool(ToAnimatorCondition.ToStudyCourseIdle.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToStudy.ToString(), true);
            }
        }
    }
    
    private void AttendEndAct()
    {
        if (randomEventAct == RandomEventAct.Event4AttendClass)
        {
            if (transform.name == "Role1")
            {
                animator.SetBool(ToAnimatorCondition.ToStudyCourseIdle.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToStudy.ToString(), true);
            }
        }
    }
    
    private void LoopAttendAct()
    {
        if (randomEventAct == RandomEventAct.Event4AttendClass)
        {
            if (transform.name == "Role7")
            {
                animator.SetBool(ToAnimatorCondition.ToStudyCourseIdle.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToStudy.ToString(), true);
            }
        }
    }
   

    // Update is called once per frame
    void Update()
    {
        if (animator == null) return;
        
        if (bedAnim != null)
            bedAnim.UpdateBedAnim();
        
        
        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Drink.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Drink);
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.StudentHammer.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.StudentHammer);
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.StudentMicroscope.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.StudentMicroscope);
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.StudentMicroscope.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.StudentMicroscope);
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Study.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Study);
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.TeacherLecture.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.TeacherLecture);
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.StudyIdle.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.StudyIdle);
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.StudyCourseIdle.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.StudyIdle);
        }
    }

    private void PlayAnim()
    {
        switch (randomEventAct)
        {
            case RandomEventAct.None:
                break;
            case RandomEventAct.Main:
                break;
            case RandomEventAct.Idle:
                break;
            case RandomEventAct.Event1Sit:
                animator.SetBool(ToAnimatorCondition.ToStudyCourseIdle.ToString(), true);
                break;
            case RandomEventAct.Event1EatDrink:
                animator.SetBool(ToAnimatorCondition.ToDrink.ToString(), true);
                break;
            case RandomEventAct.Event2AttendClass:
                animator.SetBool(ToAnimatorCondition.ToStudyIdle.ToString(), true);
                break;
            case RandomEventAct.Event2Stand:
                break;
            case RandomEventAct.Event3Sleep:
                // 首先上床
                // 床的动画。
                // 
                
                bedAnim = GameManager.Instance.GetBedAnim(EventRandomPath.Path1);
                
                break;
            case RandomEventAct.Event3SwitchDoor:
                break;
            case RandomEventAct.Event4AttendClass:
                animator.SetBool(ToAnimatorCondition.ToStudyCourseIdle.ToString(), true);
                break;
            case RandomEventAct.Event4TeacherAttendClass:
                animator.SetBool(ToAnimatorCondition.ToTeacherLecture.ToString(), true);
                break;
            case RandomEventAct.Event5Sleep:
                break;
            case RandomEventAct.Event5SwitchDoor:
                break;
            case RandomEventAct.Event6Toilet:
                break;
            case RandomEventAct.Event7Bathe:
                break;
            case RandomEventAct.Event8Toilet:
                break;
            case RandomEventAct.Event9Bathe:
                break;
            case RandomEventAct.Event10Toilet:
                break;
            case RandomEventAct.Event11Bathe:
                break;
            case RandomEventAct.Event12LaboratoryStudentHammer:
                animator.SetBool(ToAnimatorCondition.ToStudentHammer.ToString(), true);
                break;
            case RandomEventAct.Event12LaboratoryMicroscope:
                animator.SetBool(ToAnimatorCondition.ToStudentMicroscope.ToString(), true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.TeacherAttendAct -= TeacherAttendAct;
        GameManager.Instance.StudentAttendAct -= StudentAttendAct;
        GameManager.Instance.AttendEndAct -= AttendEndAct;
        GameManager.Instance.LoopAttendAct -= LoopAttendAct;
        
        GameManager.Instance.ClassroomOneAct -= ClassroomOneAct;
        GameManager.Instance.ClassroomTwoAct -= ClassroomTwoAct;
        GameManager.Instance.ClassroomThreeAct -= ClassroomThreeAct;
        GameManager.Instance.ClassroomForAct -= ClassroomForAct;
    }
}
