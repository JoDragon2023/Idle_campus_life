using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ToiletDoorAnim : MonoBehaviour
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public RandomEvent randomEvent;
    /// <summary>
    /// 门的路径
    /// </summary>
    public EventRandomPath doorAnimType;
    //厕所两边的门
    public Transform toiletDoorLeft;
    public Transform toiletDoorRight;
    
    [HideInInspector]
    public bool isOpen;
    private int rotate = 90;
    private float openDelayTiem = 0.2f;

    public void OpenAnim(bool isDelay = false)
    {
        if (isOpen) return;
        openDelayTiem = 0.5f;
        if (isDelay)
            openDelayTiem = 1.3f;
        
        StartCoroutine(PlayOpenAnim());
        isOpen = true;
    }

    IEnumerator PlayOpenAnim()
    {
        yield return new WaitForSeconds(openDelayTiem);
        toiletDoorLeft.DOLocalRotate(new Vector3(0, -rotate, 0), 0.5f);
        toiletDoorRight.DOLocalRotate(new Vector3(0, rotate, 0), 0.5f);
    }
    
    IEnumerator PlayCloseAnim()
    {
        yield return new WaitForSeconds(1f);
        toiletDoorLeft.DOLocalRotate(Vector3.zero, 0.2f);
        toiletDoorRight.DOLocalRotate(Vector3.zero, 0.2f);
    }
    
    public void CloseAnim(bool isDelay = false)
    {
        if (!isOpen) return;
        
        if (isDelay)
            StartCoroutine(PlayCloseAnim());
        else
        {
            toiletDoorLeft.DOLocalRotate(Vector3.zero, 0.2f);
            toiletDoorRight.DOLocalRotate(Vector3.zero, 0.2f);
        }
        isOpen = false;
    }
}
