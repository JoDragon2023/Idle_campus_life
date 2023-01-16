using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatheDoorAnim : MonoBehaviour
{
    public static BatheDoorAnim Instance;
    public List<GameObject> Event7BatheList;
    public List<GameObject> Event9BatheList;
    public List<GameObject> Even11BatheList;
    
    private GameObject bathe;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetBatheDoor(RandomEvent randomEvent,EventRandomPath eventRandomPath)
    {
        int index = (int)eventRandomPath;
        switch (randomEvent)
        {
            case RandomEvent.Event7:
                bathe =  Event7BatheList[index - 1];
                break;
            case RandomEvent.Event9:
                bathe =  Event9BatheList[index - 1];
                break;
            case RandomEvent.Event11:
                bathe =  Even11BatheList[index - 1];
                break;
        }
        return bathe;
    }
    
}
