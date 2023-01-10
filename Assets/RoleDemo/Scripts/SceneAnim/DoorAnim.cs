using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorAnim : MonoBehaviour
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public RandomEvent randomEvent;
    
    /// <summary>
    /// 门的类型
    /// </summary>
    public DoorAnimType doorAnimType;

    [HideInInspector]
    public bool isOpen;
    
    private int rotate;
    
    public void OpenAnim()
    {
        if (isOpen) return;
        
        rotate = 120;
        GetEventAnimRotate();
        if (doorAnimType == DoorAnimType.Classroom)
            rotate = -30;
        
        transform.DORotate(new Vector3(0, rotate, 0), 0.2f, RotateMode.Fast);
        isOpen = true;
    }

    private int GetEventAnimRotate()
    {
        rotate = 120;
        switch (randomEvent)
        {
            case RandomEvent.None:
                break;
            case RandomEvent.Event1:
                break;
            case RandomEvent.Event2:
                break;
            case RandomEvent.Event3:
                rotate = 25;
                break;
            case RandomEvent.Event4:
                break;
            case RandomEvent.Event5:
                break;
            case RandomEvent.Event6:
                break;
            case RandomEvent.Event7:
                break;
            case RandomEvent.Event8:
            case RandomEvent.Event9:
                rotate = -60;
                break;
            case RandomEvent.Event10:
                rotate = -120;
                break;
            case RandomEvent.Event11:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return rotate;
    }
    
    
    public void CloseAnim()
    {
        if (!isOpen) return;
        
        rotate = 0;
        if (doorAnimType == DoorAnimType.Classroom)
            rotate = 90;
        
        transform.DORotate(new Vector3(0, rotate, 0), 0.2f, RotateMode.Fast);
        isOpen = false;
    }
}
