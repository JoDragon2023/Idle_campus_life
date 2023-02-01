using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TestSequence : MonoBehaviour
{     
    private Sequence clickTween;

    public void PlayerAnim()
    {
        transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.1f).onComplete+= () =>
        {
            transform.gameObject.SetActive(true);
            
            if (clickTween != null)
                return;

            clickTween = DOTween.Sequence();
      
            Vector3 v3Scale1 = new Vector3(0.5f,0.5f,0.5f);
            Vector3 v3Scale2 = new Vector3(1.4f,1.4f,1.4f);
            Vector3 v3 = Vector3.one;
        
            float v3Scale1Time = 0.05f;//第一步时间
            float v3Scale2Time = 0.3f;//第二步时间
            float v3Time = 0.3f;//第三步时间
            clickTween.Append(transform.DOScale(v3Scale1, v3Scale1Time));
            clickTween.Append(transform.DOScale(v3Scale2, v3Scale2Time));
            clickTween.Append(transform.DOScale(v3, v3Time));
            clickTween.AppendCallback(() =>
            {
                clickTween = null;
            });
            
        };
    }
    private void Awake()
    {
        transform.gameObject.SetActive(false);
    }

    public void Show()
    {
        PlayerAnim();
    }
    
}
