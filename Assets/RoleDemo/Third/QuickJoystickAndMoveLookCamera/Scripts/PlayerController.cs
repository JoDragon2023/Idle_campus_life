using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform player;
    public CameraController cameraController;
    public RectTransform background;
    public RectTransform pointer;
    public float moveSpeed = 4f;

    private CharacterController playerCC;
    private Transform cameraTransform;
    private Vector2 centerPos;
    private Vector2 beginPos;
    private Vector2 dragPos;
    private float r;
    private Animation anim;

    private void Start()
    {
        anim = player.GetComponent<Animation>();

        transform.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height * 0.5f, Screen.height * 0.5f);
        background.sizeDelta = new Vector2(Screen.height, Screen.height) * 0.25f;
        pointer.sizeDelta = background.sizeDelta * 0.45f;

        //Charactercontroller parameters
        if (!player.GetComponent<CharacterController>())
        {
            playerCC = player.gameObject.AddComponent<CharacterController>();
        }
        else
        {
            playerCC = player.GetComponent<CharacterController>();
        }
        playerCC.radius = 0.5f;
        playerCC.height = 2f;
        playerCC.center = new Vector3(0, 1f, 0);


        cameraTransform = Camera.main.transform;
        centerPos = pointer.position;
        r = background.sizeDelta.x / 2;

    }

    private void FixedUpdate()
    {
       
        if (Vector2.Distance(dragPos, beginPos) > 10) // joystick move
        {
            Vector2 v2 = (dragPos - beginPos).normalized;
            float angle = Mathf.Atan2(v2.x, v2.y) * Mathf.Rad2Deg;
            angle = angle < 0 ? 360 + angle : angle;
            playerCC.transform.eulerAngles = new Vector3(0, cameraTransform.eulerAngles.y + angle, 0);
            playerCC.Move(player.forward * Time.deltaTime * moveSpeed);

            if (anim)//your ainmation set
            {
                anim.CrossFade("forward");
            }
        }
        else // joystick stop
        {


            if (anim)//your ainmation set
            {
                anim.CrossFade("standing");
            }
        }

        //Simulated drop
        if (!playerCC.isGrounded)
        {
            playerCC.Move(new Vector3(0, -10f * Time.deltaTime, 0));
        }

        cameraController.CameraSet();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        beginPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragPos = eventData.position;
        Vector2 dir = dragPos - beginPos;
        pointer.position = Vector2.Distance(dragPos, beginPos) > r ? (centerPos + dir.normalized * r) : (centerPos + dir);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragPos = Vector2.zero;
        beginPos = Vector2.zero;
        pointer.position = centerPos;
    }

}
