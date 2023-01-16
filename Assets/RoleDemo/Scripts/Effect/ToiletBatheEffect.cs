using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletBatheEffect : MonoSingleton<ToiletBatheEffect>,IDisposable
{
    public List<GameObject> Event6ToiletList;
    public List<GameObject> Event7BatheList;
   
    public List<GameObject> Event8ToiletList;
    public List<GameObject> Event9BatheList;
    
    public List<GameObject> Event10ToiletList;
    public List<GameObject> Even11BatheList;

    private GameObject effect;

    public void ShowEffect()
    {
        if (effect != null)
        {
            effect.SetActive(true);
        }
    }
    
    public void CloseEffect()
    {
        if (effect != null)
        {
            effect.SetActive(false);
        }
    }
    
    public void GetEffect(RandomEvent randomEvent,EventRandomPath eventRandomPath)
    {
        int index = (int)eventRandomPath;
        switch (randomEvent)
        {
            case RandomEvent.Event6:
                effect =  Event6ToiletList[index - 1];
                break;
            case RandomEvent.Event7:
                effect =  Event7BatheList[index - 1];
                break;
            case RandomEvent.Event8:
                effect =  Event8ToiletList[index - 1];
                break;
            case RandomEvent.Event9:
                effect =  Event9BatheList[index - 1];
                break;
            case RandomEvent.Event10:
                effect =  Event10ToiletList[index - 1];
                break;
            case RandomEvent.Event11:
                effect =  Even11BatheList[index - 1];
                break;
        }
    }
    
    private void OnDestroy()
    {
        if (effect != null)
        {
            effect = null;
        }
    }
    
}
