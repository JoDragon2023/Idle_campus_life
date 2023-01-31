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
        if (clickTween != null)
            return;

        clickTween = DOTween.Sequence();
      
        Vector3 v3Scale1 = new Vector3(0.5f,0.5f,0.5f);
        Vector3 v3Scale2 = new Vector3(0.8f,0.8f,0.8f);
        Vector3 v3 = transform.localScale;
        
        float v3Scale1Time = 0.2f;
        float v3Scale2Time = 0.2f;
        float v3Time = 0.2f;
        clickTween.Append(transform.DOScale(v3Scale1, v3Scale1Time));
        clickTween.Append(transform.DOScale(v3Scale2, v3Scale2Time));
        clickTween.Append(transform.DOScale(v3, v3Time));
        clickTween.AppendCallback(() =>
        {
            clickTween = null;
        });
    }

    private void Awake()
    {
        transform.gameObject.SetActive(false);
    }

    public void Show()
    {
        
        transform.gameObject.SetActive(true);
    }
    
}
