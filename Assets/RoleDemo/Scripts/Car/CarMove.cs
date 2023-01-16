using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarMove : MonoBehaviour
{
    public GameObject carOne;
    public GameObject carTwo;
    public GameObject carThree;
    
    public int carTimeOneMax;
    public int carTimeTwoMax;
    public int carTimeThreeMax;
    
    public EventRandomPath currPath;
    private Vector3[] movePath;
    private float carOneTime;
    private float carTwoTime;
    private float carThreeTime;
    private bool animIdleOne = false;
    private bool animIdleTwo = false;
    private bool animIdleThree = false;

    private Vector3 carOnePos;
    private Vector3 carTwoPos;
    private Vector3 carThreePos;


    private int moveTime = 15;
    
    // Start is called before the first frame update
    void Start()
    {
        carOnePos = carOne.transform.localPosition;
        carTwoPos = carTwo.transform.localPosition;
        carThreePos = carThree.transform.localPosition;
        
        carOne.gameObject.SetActive(false);
        carTwo.gameObject.SetActive(false);
        carThree.gameObject.SetActive(false);
        movePath = ScenePath.Instance.GetCarPath(currPath);
        animIdleOne = true;
        animIdleTwo = true;
        animIdleThree = true;
    }

    private void Update()
    {
        if (animIdleOne)
        {
            carOneTime += Time.deltaTime;
            if (carOneTime > carTimeOneMax)
            {
                carOneTime = 0;
                carOne.gameObject.SetActive(true);
                animIdleOne = false;
                carOne.transform.DOPath(movePath, moveTime, PathType.Linear)
                    .SetEase(Ease.Linear).onComplete = () =>
                {
                    animIdleOne = true;
                    carOne.transform.localPosition = carOnePos;
                    carOne.gameObject.SetActive(false);
                };
            }
        }
        
        if (animIdleTwo)
        {
            carTwoTime += Time.deltaTime;
            if (carTwoTime > carTimeTwoMax)
            {
                carTwoTime = 0;
                carTwo.gameObject.SetActive(true);
                animIdleTwo = false;
                carTwo.transform.DOPath(movePath, moveTime, PathType.Linear)
                    .SetEase(Ease.Linear).onComplete = () =>
                {
                    animIdleTwo = true;
                    carTwo.transform.localPosition = carTwoPos;
                    carTwo.gameObject.SetActive(false);
                };
            }
        }
        
        if (animIdleThree)
        {
            carThreeTime += Time.deltaTime;
            if (carThreeTime > carTimeThreeMax)
            {
                carThreeTime = 0;
                carThree.gameObject.SetActive(true);
                animIdleThree = false;
                carThree.transform.DOPath(movePath, moveTime, PathType.Linear)
                    .SetEase(Ease.Linear).onComplete = () =>
                {
                    animIdleThree = true;
                    carThree.transform.localPosition = carThreePos;
                    carThree.gameObject.SetActive(false);
                };
            }
        }
    }
}
