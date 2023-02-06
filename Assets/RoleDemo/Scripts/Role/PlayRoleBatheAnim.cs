using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayRoleBatheAnim : MonoBehaviour
{
    public EventRandomPath curRandomPath;
    private AnimatorStateInfo currRoleAnimatorStateInfo { get; set; }
    private Animator animator;
    private float startTime = 0.2f;
    private bool isBatheOne = false;
    private bool isBatheTwo = false;
    private bool isBatheThree = false;
    private bool isBatheFour = false;
    private Vector3[] bathePath;
    private int batheRotate = 180;
    private int batheRotateDoor = 180;
    private int roleNakedType = 1;
    private int batheDoorRotate = -90;
    private float batheTime = 10;
    private float batheIdleTime;
    private float durTime;
    private int batheIdleTimeMax = 3;
    private BatheStateAnim batheStateAnim;
    private GameObject batheDoorObj;
    
    private bool animIdleOne = false;
    private bool animIdleTwo = false;
    private RoleAnimator roleAnimator;
    private GameObject toiletBathe;
    //继续播放动画 间隔时间
    private float loopAnimTime = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        roleAnimator = transform.GetComponent<RoleAnimator>();
        animator.SetBool(ToAnimatorCondition.ToStand.ToString(), true);
        switch (curRandomPath)
        {
            case EventRandomPath.Path2:
                startTime = 0.5f;
                roleNakedType = 1;
                break;
            case EventRandomPath.Path3:
                startTime = 1f;
                roleNakedType = 2;
                break;
        }
        
        transform.DOScale(Vector3.one, startTime).onComplete = () => { EnterBatheAct();};
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBatheAct(Time.deltaTime);
    }
 
    public void EnterBatheAct()
    {
        isBatheOne = false;
        isBatheTwo = false;
        isBatheThree = false;
        isBatheFour = false;
        animIdleOne = false;
        animIdleTwo = false;
        batheIdleTime = 0;
        batheStateAnim = BatheStateAnim.Open;
        EnterBathe();
    }

    private void ShowRoleBatheModel(bool isNaked)
    {
        if (roleNakedType == 1)
        {
            animator = roleAnimator.nakedOneAnimator;
            roleAnimator.nakedOne.SetActive(isNaked);
            //roleAnimator.emojiCoolOne.SetActive(isNaked);
            roleAnimator.role.SetActive(!isNaked);
        }
        else if (roleNakedType == 2)
        {
            animator = roleAnimator.nakedTwoAnimator;
            roleAnimator.nakedTwo.SetActive(isNaked);
            //roleAnimator.emojiCoolTwo.SetActive(isNaked);
            roleAnimator.role.SetActive(!isNaked);
        }

        if (!isNaked)
        {
            animator = roleAnimator.roleAnimator;
        }
    }

    private void PlayEffect()
    {
        transform.DOScale(Vector3.one, 2.4f).onComplete = () =>
        {
            roleAnimator.effect.SetActive(true);
            transform.DOScale(Vector3.one, 2f).onComplete = () => { roleAnimator.effect.SetActive(false); };
        };
    }
    
    public void UpdateBatheAct(float deltaTime)
    {
        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Stand.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Stand);
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.BatheOne.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.BatheOne);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isBatheOne)
                {
                    isBatheOne = true;
                    animIdleTwo = true;
                    if (batheStateAnim == BatheStateAnim.Open)
                    {
                        animator.SetBool(ToAnimatorCondition.ToBatheOne.ToString(), false);
                        ShowRoleBatheModel(true);
                        animator.SetBool(ToAnimatorCondition.ToStand.ToString(), true);
                        OpenBatheDoorAnim();
                    }
                    else if (batheStateAnim == BatheStateAnim.Close)
                    {
                        animator.SetBool(ToAnimatorCondition.ToBatheOne.ToString(), false);
                        ShowRoleBatheModel(false);
                        animator.SetBool(ToAnimatorCondition.ToStand.ToString(), true);
                    }
                   
                }
            }
        }

        if (animIdleTwo)
        {
            batheIdleTime += deltaTime;
            if (batheIdleTime > batheIdleTimeMax)
            {
                animIdleTwo = false;
                batheIdleTime = 0;
                animator.SetBool(ToAnimatorCondition.ToStand.ToString(), false);
                if (batheStateAnim == BatheStateAnim.Open)
                {
                    animator.SetBool(ToAnimatorCondition.ToBatheTwo.ToString(), true);
                }
                else if (batheStateAnim == BatheStateAnim.Close)
                {
                    //animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
                    leaveBathe();
                }
            }
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.BatheTwo.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.BatheTwo);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isBatheTwo)
                {
                    isBatheTwo = true;
                    animator.SetBool(ToAnimatorCondition.ToBatheTwo.ToString(), false);
                    animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
                    EntryBatheAnim();
                }
            }
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.BatheThree.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.BatheThree);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isBatheThree)
                {
                    isBatheThree = true;
                    animIdleOne = true;
                }
            }
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.BatheFour.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.BatheFour);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isBatheFour)
                {
                    isBatheFour = true;
                    isBatheOne = false;
                    batheStateAnim = BatheStateAnim.Close;
                    animator.SetBool(ToAnimatorCondition.ToBatheFour.ToString(), false);
                    animator.SetBool(ToAnimatorCondition.ToBatheOne.ToString(), true);
                    PlayEffect();
                }
            }
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Walk_01.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Run);
        }

        if (animIdleOne)
        {
            durTime += deltaTime;
            if (durTime > batheTime)
            {
                animIdleOne = false;
                durTime = 0;
                animator.SetBool(ToAnimatorCondition.ToBatheThree.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
                CloseToiletBatheEffect();
                //往门口的点走
                leaveBatheDoor();
            }
        }
    }

    /// <summary>
    /// 走到洗澡间门口 播放关门动画
    /// </summary>
    private void leaveBatheDoor()
    {
        batheRotateDoor = 0;
        var leaveBathePos = ScenePath.Instance.GetEventToiletActPath(bathePath);
        transform.DOPath(ScenePath.Instance.GetEventToiletActPoint(leaveBathePos), 0.5f, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            transform.DORotate(new Vector3(0, batheRotateDoor, 0), 0.2f, RotateMode.Fast).onComplete = () =>
            {
                animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToBatheFour.ToString(), true);
                CloseBatheDoorAnim();
            };
        };
    }

    /// <summary>
    /// 退出洗澡间
    /// </summary>
    private void leaveBathe()
    {
        LoopPlayAnim();
    }
    
    private void LoopPlayAnim()
    {
        transform.DORotate(Vector3.zero, 0.2f, RotateMode.Fast).onComplete = () =>
        {
            animator.SetBool(ToAnimatorCondition.ToStand.ToString(), true);
            transform.DOScale(Vector3.one, loopAnimTime).onComplete = () => { EnterBatheAct();};
        };
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private void EnterBathe()
    {
        batheDoorObj = BatheDoorAnim.Instance.GetBatheDoor(RandomEvent.Event9,curRandomPath);
        toiletBathe = ToiletBatheEffect.Instance.GetEffect(RandomEvent.Event9,curRandomPath);
        bathePath = ScenePath.Instance.GetEvent9BathePath(curRandomPath);
        
        animator.SetBool(ToAnimatorCondition.ToStand.ToString(), false);
        animator.SetBool(ToAnimatorCondition.ToBatheOne.ToString(), true);
        PlayEffect();
    }

    /// <summary>
    /// 进入洗澡间
    /// </summary>
    private void EntryBatheAnim()
    {
        batheRotate = 180;

        transform.DOPath(ScenePath.Instance.GetEventToiletActPoint(bathePath), 0.5f, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            transform.DORotate(new Vector3(0, batheRotate, 0), 0.2f, RotateMode.Fast).onComplete = () =>
            {
                ShowToiletBatheEffect();
                animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToBatheThree.ToString(), true);
            };
        };
    }

    private void OpenBatheDoorAnim()
    {
        transform.DOScale(Vector3.one, 3.6f).onComplete = () =>
        {
            batheDoorObj.transform.DOLocalRotate(new Vector3(0, batheDoorRotate, 0), 0.5f);
        };
    }
    
    private void CloseBatheDoorAnim()
    {
        transform.DOScale(Vector3.one, 1f).onComplete = () =>
        {
            batheDoorObj.transform.DOLocalRotate(Vector3.zero, 0.5f);
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
