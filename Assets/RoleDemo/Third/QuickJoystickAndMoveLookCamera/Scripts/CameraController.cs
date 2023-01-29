using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class CameraController : MonoBehaviour, IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    public Transform player;
    public Texture pointerTex;

    public float piovtY = 0;
    public float distance = 5.0f; // distance from target (used with zoom)
    public float minDistance = 2f;
    public float maxDistance = 7f;
    public float zoomSpeed = 0.2f;
    public float xSpeed = 0.3f;
    public float ySpeed = 0.3f;
    public float yMinLimit = -15f;
    public float yMaxLimit = 90f;

    private Vector3 pivotOffset; // offset from target's pivot
    private float x = 0;
    private float y = 0;
    private float targetX = 0;
    private float targetY = 0;
    private float targetDistance = 0;
    private float xVelocity = 1f;
    private float yVelocity = 1f;
    private float zoomVelocity = 1f;
    private CharacterController cameraCC;
    private Transform cameraTransform;

    private int firstPointerId = -1;
    private int secondPointerId = -1;
    private Vector2 firstDragPos;
    private Vector2 secondDragPos;
    private bool isFirstZoom = true;
    private float firstDragOffset;
    //private RectTransform pointer;
    private GameObject firstPointerObj;
    private GameObject secondPointerObj;
    private void Start()
    {

        //Charactercontroller parameters
        cameraTransform = Camera.main.transform;
        if (!cameraTransform.GetComponent<CharacterController>())
        {
            cameraCC = cameraTransform.gameObject.AddComponent<CharacterController>();
        }
        else
        {
            cameraCC = cameraTransform.GetComponent<CharacterController>();
        }
        cameraCC.radius = 0.5f;
        cameraCC.height = 0.5f;

        //major parameter
        pivotOffset = new Vector3(0, piovtY, 0);
        Vector3 angles = cameraTransform.eulerAngles;
        targetX = x = angles.x;
        targetY = y = ClampAngle(angles.y, yMinLimit, yMaxLimit);
        targetDistance = distance;

        Canvas canvas = (Canvas)FindObjectOfType(typeof(Canvas));

        firstPointerObj = new GameObject();
        firstPointerObj.transform.parent = canvas.transform;
        RectTransform firstPointer = firstPointerObj.AddComponent<RectTransform>();
        firstPointer.gameObject.AddComponent<RawImage>().texture=pointerTex;
        firstPointer.rect.Set(0,0,100,100);
        firstPointer.pivot.Set(0.5f,0.5f);
        firstPointerObj.SetActive(false);
        firstPointer.sizeDelta = new Vector2(Screen.height, Screen.height) * 0.25f * 0.4f;

        secondPointerObj = new GameObject();
        secondPointerObj.transform.parent = canvas.transform;
        RectTransform secondPointer = secondPointerObj.AddComponent<RectTransform>();
        secondPointer.gameObject.AddComponent<RawImage>().texture = pointerTex;
        secondPointer.rect.Set(0, 0, 100, 100);
        secondPointer.pivot.Set(0.5f, 0.5f);
        secondPointerObj.SetActive(false);
        secondPointer.sizeDelta = new Vector2(Screen.height, Screen.height) * 0.25f * 0.4f;




    }

    public void OnDrag(PointerEventData eventData)
    {
        if (firstPointerId != -1 && secondPointerId != -1)
        {
            if (eventData.pointerId == firstPointerId)
            {
                firstDragPos = eventData.position;
                firstPointerObj.transform.position = eventData.position;
            }

            if (eventData.pointerId == secondPointerId)
            {
                secondDragPos = eventData.position;
                secondPointerObj.transform.position = eventData.position;
            }

            float tempDragOffset = Mathf.Abs(firstDragPos.x - secondDragPos.x);
            if (isFirstZoom)
            {
                firstDragOffset = tempDragOffset;
                isFirstZoom = false;
            }

            if (tempDragOffset > firstDragOffset)
            {
                targetDistance -= zoomSpeed;
                firstDragOffset = tempDragOffset;
                targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);
            }
            else if (tempDragOffset < firstDragOffset)
            {
                firstDragOffset = tempDragOffset;
                targetDistance += zoomSpeed;
                targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);
            }
        }
        else if (firstPointerId != -1 || secondPointerId != -1)
        {
            if(firstPointerId!=-1){

                firstPointerObj.transform.position = eventData.position;
            }

            if(secondPointerId!=-1){

                secondPointerObj.transform.position = eventData.position;
            }

            targetX += eventData.delta.x * xSpeed;
            targetY -= eventData.delta.y * ySpeed;

            targetY = ClampAngle(targetY, yMinLimit, yMaxLimit);
        }


    }

    private float ClampAngle(float angle, float min, float max)
    {
        angle= angle < -360 ? angle + 360 : angle;
        angle = angle > 360 ? angle - 360 : angle;
        return Mathf.Clamp(angle, min, max);
    }

    public void CameraSet()
    {
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        x = Mathf.SmoothDampAngle(x, targetX, ref xVelocity, 0.3f);
        y = Mathf.SmoothDampAngle(y, targetY, ref yVelocity, 0.3f);
        Quaternion rotation = Quaternion.Euler(y, x, 0);

        distance = Mathf.SmoothDamp(distance, targetDistance, ref zoomVelocity, 0.3f);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + player.position + pivotOffset;
        cameraTransform.rotation = rotation;

        if (position != cameraTransform.position)
        {
            cameraCC.Move(position - cameraTransform.position);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (firstPointerId == -1)
        {
            firstPointerId = eventData.pointerId;
            firstPointerObj.SetActive(true);
            firstPointerObj.transform.position = eventData.position;
            print("bd=" + eventData.position);


        }
        else if (secondPointerId == -1)
        {
            secondPointerId = eventData.pointerId;
            secondPointerObj.SetActive(true);
            secondPointerObj.transform.position = eventData.position;

        }


        print("pointerDrown=" + eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == firstPointerId)
        {
            firstPointerId = -1;
            firstPointerObj.SetActive(false);
        }

        if (eventData.pointerId == secondPointerId)
        {
            secondPointerId = -1;
            secondPointerObj.SetActive(false);
        }


        print("pointerUp=" + eventData.position);
    }

}
