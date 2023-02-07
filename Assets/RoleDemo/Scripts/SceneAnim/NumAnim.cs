using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NumAnim : MonoBehaviour
{
    public Text oneText;
    public Text twoText;
    private Sequence mScoreSequence;
    private int mOneOldScore = 3000;
    private int mTwoOldScore = 5000;
    
    void Start()
    {
        oneText.text = "3000";
        twoText.text = "5000";
        mScoreSequence = DOTween.Sequence();
        mScoreSequence.SetAutoKill(false);
    }
    
    public void UpdatePanelInfo(bool isOne)
    {
        var newScore = 0;
        if (isOne)
        {
            mScoreSequence.Append(DOTween.To(delegate (float value) {
                var temp = Math.Floor(value);
                oneText.text = temp + "";
            }, mOneOldScore, newScore, 1f));
            
            mOneOldScore = newScore;
        }
        else
        {
            mScoreSequence.Append(DOTween.To(delegate (float value) {
                var temp = Math.Floor(value);
                twoText.text = temp + "";
            }, mTwoOldScore, newScore, 1f));
            
            mTwoOldScore = newScore;
        }
    }
}
