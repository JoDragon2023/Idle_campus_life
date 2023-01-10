// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using DG;
// using DG.Tweening;
//
// public class TestNpc : MonoBehaviour
// {
//     public Animator animator;
//     public Transform[] pathPos;
//     private Vector3 [] posArry;
//     // Start is called before the first frame update
//     void Start()
//     { 
//         posArry = new Vector3[pathPos.Length];
//         for (int i = 0; i < pathPos.Length; i++)
//         {
//             posArry[i] = pathPos[i].position;
//         }
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.A))
//         {
//             PlayAnim(RoleState.Run);
//         }else if (Input.GetKeyDown(KeyCode.D))
//         {
//             PlayAnim(RoleState.Build);
//         }
//         else if (Input.GetKeyDown(KeyCode.C))
//         {
//             PlayAnim(RoleState.Collecton);
//         }else if (Input.GetKeyDown(KeyCode.W))
//         {
//             PlayAnim(RoleState.Run);
//             this.transform.DOPath(posArry, 6,PathType.CatmullRom).SetEase(Ease.Linear).SetLookAt(0).onComplete = OnComplete;
//         }
//     }
//
//     private void OnComplete()
//     {
//         PlayAnim(RoleState.Collecton);
//         
//         transform.LookAt(pathPos[pathPos.Length - 1]);
//     }
//
//
//     public void PlayAnim(RoleState roleState)
//     {
//         if (animator == null)
//             return;
//
//         string curAnim = GetAnimName(roleState);
//         animator.CrossFade(curAnim, 0.2f);
//     }
//     
//     private string GetAnimName(RoleState roleState)
//     {
//         string animName;
//
//         switch (roleState)
//         {
//             case RoleState.Idle:
//                 animName = "idle";
//                 break;
//             case RoleState.Run:
//                 animName = "run";
//                 break;
//             case RoleState.Collecton:
//                 animName = "collection";
//                 break;
//             case RoleState.Eat:
//                 animName = "eat";
//                 break;
//             case RoleState.Jump:
//                 animName = "jump";
//                 break;
//             case RoleState.Build:
//                 animName = "build";
//                 break;
//             default:
//                 animName = "";
//                 break;
//         }
//
//         return animName;
//     }
// }
