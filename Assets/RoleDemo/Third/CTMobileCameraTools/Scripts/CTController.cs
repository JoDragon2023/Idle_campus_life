using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace CTMobileCameraTools
{

    public class CTController : MonoBehaviour
    {
        public enum CameraProjection { Perspective, OrthoGraphic };
        public static CTController instance;
        private Camera ctCamera;
        //PAN SETTINGS]
        public bool enablePan = true;
        public float panSpeed = 0.5f;
        public float panInertia = 0.9f;
        //ZOOM SETTINGS
        public bool enableZoom = true;
        public float zoomSensitivity = 0.4f;
        public float zoomMinHeight = 7f;
        public float zoomMaxHeight = 30f;
        public float zoomInertia = 0.5f;
        //WITH ORTHOGRAPHIC CAMERA WE USE THESE SETTINGS FOR ZOOM
        public float zoomProjectionMin = 3f;
        public float zoomProjectionMax = 9f;
        //ROTATE SETTINGS
        public bool enableRotation = true;
        public float rotationSpeed = 3f;
        public float rotationInertia = 0.9f;
        //TILT SETTINGS
        public bool enableTilt = true;
        public float tiltSpeed = 1.7f;
        public float tiltMinAngle = 25f;
        public float tiltMaxAngle = 75f;
        public float tiltInertia = 0.5f;
        //INITIAL CAMERA SETTINGS
        public float initialCameraZoom = 20f;
        public float initialCameraTilt = 45f;
        public float initialCameraRotation = 45f;
        public Vector3 initialCameraPosition;
        //OTHER OPTIONS
        //USER CAN TEST SETTINGS IN THE EDITOR USING MOUSE
        public bool enableEditorEmulation = false;
        public KeyCode editorRotateKey = KeyCode.R;
        public KeyCode editorZoomKey = KeyCode.Z;
        public KeyCode editorTiltKey = KeyCode.T;
        //WHEN GESTURE STARTS ON THIS LAYER IT WILL BE IGNORED
        public bool prioritizeRaycastLayer = false;
        //LAYER TO IGNORE
        public LayerMask prioritizedLayerMask;
        //MIN DELAY BETWEEN GESTURES
        public float delayBetweenGestures = 0.1f;
        //IF TRUE GESTURE OVER UI WILL BE IGNORED
        public bool ignoreTouchOverUI = true;
        //REFERENCE FOR ZOOM TRANSFORM        
        private Transform zoomNode;
        //REFERENCE FOR TILT TRANSFORM
        private Transform tiltNode;
        //WHETHER TO USE UNSCALED OR SCALED TIME.DELTATIME
        public bool useUnscaledDeltaTime = true;
        //CACHE CURRENT GESTURES DELTAS
        private Vector2 accumulatedDrag = Vector2.zero;
        private float accumulatedRotation = 0f;
        private float accumulatedZoom = 0f;
        private float accumulatedTilt = 0f;
        //THIS IS INTERNAL LOCKING OF INPUT
        private bool canControl = true;
        [HideInInspector]
        public CameraProjection cameraProjection = CameraProjection.Perspective;
        //GET SCREEN RESOLUTION INTO ACCOUNT SO WE GET SIMILAR RESULT ACROSS DIFFERENT RESOLUTIONS
        private float screenScaler;
        //TARGET FOLLOW SECTION
        public bool enableCamFollow = false;
        public float camFollowSpeed = 5f;
        public Transform camFollowTarget;
        //IF TRUE WHEN PAN CAM FOLLOW WILL BE CANCELLED
        public bool panCancelsTarget = false;
        //IF TRUE WHEN PAN REACHES BOUNDS CAM FOLLOW WILL BE CANCELLED
        public bool outOfBoundsCancelsTarget = false;
        //USER CONTROLLED INPUT LOCKING
        private static bool lockInput = false;
        //TEMPORARILY CANCELS CAM FOLLOW BUT DOESNT FORGET THE CURRENT TARGET TO FOLLOW/ CAN BE RESUMED
        private static bool pauseCameraFollow = false;
        //PRIVATES
        private Vector2 previousMousePosition = Vector2.zero;
        //DELTA MOVEMENT BETWEEN CURRENT AND PREVIOUS MOUSE POSITION
        private Vector2 mouseDelta = Vector2.zero;
        //WHETHER A BUTTON IS DOWN (EDITOR ONLY)
        private bool isMouseDown = false;
        //ERROR CHECK IN CASE USER DELETED SOME COMPONENTS OF THE SYSTEM OR INITIALIZED IT INCORRECTLY
        private bool hasErrors = false;

        #region  MONO AND CT METHODS
        private void Awake()
        {
            instance = this;
        }

        private void OnBtnRight()
        {
            //Debug.Log(" OnBtnRight ");
            isBtnRotate = false;
            isBtnsuspend = true;
        }

        private void OnBtnMiddle()
        {
            //Debug.Log(" OnBtnMiddle ");
            isBtnsuspend = false;
        }

        private void OnBtnLeft()
        {
            //Debug.Log(" OnBtnLeft ");
            isBtnRotate = true;
            isBtnsuspend = true;
        }

        private void Start()
        {
            GameManager.Instance.btnLeftAct += OnBtnLeft;
            GameManager.Instance.btnRightAct += OnBtnRight;
            GameManager.Instance.btnMiddleAct += OnBtnMiddle;

            //GATHER REFERENCES
            tiltNode = transform.Find("Tilt").transform;
            zoomNode = tiltNode.transform.Find("Zoom").transform;
            ctCamera = GetComponentInChildren<Camera>();

            CheckForErrors();

            if (hasErrors)
            {
                Debug.LogError("Initialization failed: read above errors for more details");
                return;
            }
            //INITIALIZE PARAMETERS
            //GET SCREEN RES INTO CONSIDERATION (FEEL FREE TO WRITE YOUR OWN ALGORITHM HERE)
            screenScaler = 1 / (Screen.width / 2000f);

            //INIT CAMERA BASED ON THE SETTINGS 
            InitCamera();

            //CHECK FOR CORRECT CAMERA SETTINGS
            if (ctCamera.orthographic && cameraProjection.Equals(CameraProjection.Perspective))
            {
                Debug.LogWarning("Your camera projection is set to Orthographic, but your selected Perspective in CTController, switching CT settings to Orthographic");
                cameraProjection = CameraProjection.OrthoGraphic;
            }
            else if (!ctCamera.orthographic && cameraProjection.Equals(CameraProjection.OrthoGraphic))
            {
                Debug.LogWarning("Your camera projection is set to Perspetive, but your selected Orthographic in CTController, switching CT settings to Perspective");
                cameraProjection = CameraProjection.Perspective;
            }

        }
        //CHECK IF ALL COMPONENTS OF THE SYSTEM ARE IN THE SCENE 
        private void CheckForErrors()
        {
            if (!NodeManager.instance)
            {
                Debug.LogError("CTNodeController cannot be found, add prefab to the scene, or enable it, if it's disabled!");
                hasErrors = true;
            }
            if (!tiltNode || !zoomNode)
            {
                Debug.LogError("Some child components of CTCameraController are missing, consider reimporting CTCameraController prefab into the scene");
                hasErrors = true;
            }
            if (!ctCamera)
            {
                Debug.LogError("Camera child component of CTCameraController is missing, consider reimporting CTCameraController prefab into the scene");
                hasErrors = true;
            }
        }
        //APPLY INITIAL SETTINGS
        private void InitCamera()
        {
            //SET INITIAL TILT
            tiltNode.localEulerAngles += new Vector3(initialCameraTilt, 0, 0);

            //SET INITIAL ZOOM
            if (cameraProjection == CameraProjection.Perspective)
            {
                zoomNode.Translate(Vector3.forward * -initialCameraZoom);
            }
            else
            {
                //MOVE CAMERA SOME ARBITRARY DISTANCE UP SO IT DOESNT CLIP THROUGH GEOMETRY 
                zoomNode.Translate(Vector3.forward * -25f);
                ctCamera.orthographicSize = initialCameraZoom;
            }

            //SET INITIAL ROTATION
            transform.localEulerAngles = new Vector3(0, initialCameraRotation, 0);

            //IF USER DEFINED INITIAL POSITION SET IT
            if (initialCameraPosition != Vector3.zero)
                transform.position = initialCameraPosition;
        }
        private void Update()
        {
            //DONT RUN IF HAVE ERRORS
            if (hasErrors)
                return;

            //IF EMULATING IN EDITOR, GATHER MOUSE MOVEMENT VALUES
            if (enableEditorEmulation)
            {
                if (Input.GetMouseButton(0))
                {
                    if (previousMousePosition != Vector2.zero)
                    {
                        mouseDelta = (Vector2)Input.mousePosition - previousMousePosition;
                    }
                    else
                    {
                        mouseDelta = Vector2.zero;
                    }
                    previousMousePosition = Input.mousePosition;
                    isMouseDown = true;
                }
                else
                {
                    previousMousePosition = Vector2.zero;
                    mouseDelta = Vector2.zero;
                    isMouseDown = false;
                }
            }
        }
        private void LateUpdate()
        {
            //DONT RUN IF HAVE ERRORS
            if (hasErrors)
                return;

            //AVOID JITTERY MOVEMENT WHEN ENDING GESTURE
            if (CheckTouchState(TouchPhase.Ended))
            {
                canControl = false;
                Invoke("UnlockControl", delayBetweenGestures);
            }

            //EXECUTE CAMERA FOLLOW
            if (enableCamFollow && !pauseCameraFollow)
                CameraFollow();

            //USER CONTROLLED LOCK INPUT
            if (lockInput) return;

            //IGNORE UI MOVEMENT
            // if (ignoreTouchOverUI)
            //     if (IsPointerOverUIObject())
            //         return;

            //CHECK IF RAYCAST HIT PRIORITIZED OBJECT
            if (prioritizeRaycastLayer)
            {
                if (CheckTouchState(TouchPhase.Began))
                {
                    if (RaycastHitScene())
                    {
                        canControl = false;
                        return;
                    }
                }
            }

            if (isBtnsuspend)
                BtnRotate();
            
            //RUN CORE GESTURES
            if (enablePan)
                Pan();
            if (enableZoom)
                Zoom();
            if (enableRotation)
                Rotate();
            if (enableTilt)
                Tilt();
        }
        //RAYCAST FOR OBJECTS WITH SPECIFIED LAYER
        private bool RaycastHitScene()
        {
            if ((Input.touchCount > 0))
            {

                RaycastHit hit;
                Ray ray = ctCamera.ScreenPointToRay(Input.GetTouch(0).position);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, prioritizedLayerMask))
                {
                    return true;
                }
            }
#if UNITY_EDITOR

            if (enableEditorEmulation && Input.GetMouseButton(0))
            {
                RaycastHit hit;
                Ray ray = ctCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, prioritizedLayerMask))
                {
                    return true;
                }
            }
#endif
            return false;
        }
        //RETURN TIME.DELTATIME BASED ON SETTINGS
        private float GetTimeSinceLastFrame()
        {
            if (useUnscaledDeltaTime)
            {
                return Time.unscaledDeltaTime;
            }
            else
            {
                return Time.deltaTime;
            }
        }
        //RESTORE CONTROL, IT IS CALLED VIA INVOKE
        private void UnlockControl()
        {
            canControl = true;
        }
        //PAN MOTION
        private void Pan()
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && canControl)
            {
                accumulatedRotation = 0;

                accumulatedDrag = Input.GetTouch(0).deltaPosition * panSpeed * screenScaler * GetTimeSinceLastFrame();

                //CANCELS CAM FOLLOW TARGET
                if (panCancelsTarget && enableCamFollow && camFollowTarget)
                    camFollowTarget = null;
            }
            else
            {
                if (!panCancelsTarget && camFollowTarget)
                {
                    accumulatedDrag = Vector2.zero;
                }

            }
#if UNITY_EDITOR

            if (enableEditorEmulation && isMouseDown && !Input.GetKey(editorRotateKey) && !Input.GetKey(editorZoomKey) && !Input.GetKey(editorTiltKey) && canControl)
            {
                accumulatedRotation = 0;

                accumulatedDrag = mouseDelta * panSpeed * screenScaler * GetTimeSinceLastFrame();

                //CANCELS CAM FOLLOW TARGET
                if (panCancelsTarget && enableCamFollow && camFollowTarget)
                    camFollowTarget = null;
            }
            else
            if (!panCancelsTarget && camFollowTarget)
            {
                accumulatedDrag = Vector2.zero;
            }
#endif

            
            if (GuideMager.Instance.isGuide)
            {
                if (GuideMager.Instance.isPan)
                {
                    if (-accumulatedDrag.y > 0.02 || -accumulatedDrag.x > 0.02 
                                                  ||-accumulatedDrag.y < -0.02 || -accumulatedDrag.x < -0.02)
                    {
                
                        GuideMager.Instance.CompleteGuide(GuideStep.Panning);
                        //Debug.Log(" -accumulatedDrag.x  "+ (-accumulatedDrag.x) + "  -accumulatedDrag.y   "+(-accumulatedDrag.y));
                    }
                }
            }
           
            transform.Translate(new Vector3(-accumulatedDrag.x, 0, -accumulatedDrag.y), Space.Self);

            //CLAMP BOUNDS
            float x = Mathf.Clamp(transform.position.x, NodeManager.instance.nodePositions[3].x, NodeManager.instance.nodePositions[1].x);
            float z = Mathf.Clamp(transform.position.z, NodeManager.instance.nodePositions[3].z, NodeManager.instance.nodePositions[1].z);

            //ADJUST POSITION
            transform.position = new Vector3(x, transform.position.y, z);//  Vector3.Lerp(transform.position, new Vector3(x, transform.position.y, z),2f);

            //DECREASE THE INERTIA OVER TIME
            accumulatedDrag *= Mathf.Pow(panInertia / 1000f, GetTimeSinceLastFrame());

            //CLAMP
            if (accumulatedDrag.magnitude < 0.01f)
                accumulatedDrag = Vector2.zero;
        }
        //ZOOM MOTION
        private void Zoom()
        {
            if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved &&
                Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                //CHECK IF MOVING HORIZONTALLY
                if (Mathf.Abs(Input.GetTouch(0).deltaPosition.x) < Mathf.Abs(Input.GetTouch(0).deltaPosition.y) &&
                    Mathf.Abs(Input.GetTouch(1).deltaPosition.x) < Mathf.Abs(Input.GetTouch(1).deltaPosition.y))
                {
                    return;
                }

                //CHECK IF FINGERS ARE IN OPPOSITE SCREEN REGIONS
                if ((Input.GetTouch(0).position.x < Screen.width / 2 &&
                     Input.GetTouch(1).position.x < Screen.width / 2) ||
                    (Input.GetTouch(0).position.x > Screen.width / 2 &&
                     Input.GetTouch(1).position.x > Screen.width / 2))
                {
                    return;
                }

                //CHECK IF FINGERS MOVE IN OPPOSITE DIRECTIONS
                if (Input.GetTouch(0).deltaPosition.x * Input.GetTouch(1).deltaPosition.x < 0 && canControl)
                {

                    //GET DELTA MOVEMENT
                    float delta = screenScaler * Mathf.Max(Mathf.Abs(Input.GetTouch(0).deltaPosition.x),
                        Mathf.Abs(Input.GetTouch(1).deltaPosition.x)) * GetTimeSinceLastFrame();

                    //SET SIGN (ZOOM IN OR ZOOM OUT)
                    if ((Input.GetTouch(0).deltaPosition.x > 0 && Input.GetTouch(0).position.x < Screen.width / 2) ||
                        (Input.GetTouch(0).deltaPosition.x < 0 && Input.GetTouch(0).position.x > Screen.width / 2))
                        delta *= -1;

                    accumulatedZoom = zoomSensitivity * delta;
                    //FOR ORTHOGRAPHIC CAMERA WE DO REVERSE MOTION SO MULTIPLY BY -1
                    if (cameraProjection.Equals(CameraProjection.OrthoGraphic))
                        accumulatedZoom *= -1;
                }

            }
#if UNITY_EDITOR

            if (enableEditorEmulation && isMouseDown && Input.GetKey(editorZoomKey) && canControl)
            {
                float delta = screenScaler * mouseDelta.x * GetTimeSinceLastFrame();

                accumulatedZoom = zoomSensitivity * delta;

                //FOR ORTHOGRAPHIC CAMERA WE DO REVERSE MOTION SO MULTIPLY BY -1
                if (cameraProjection.Equals(CameraProjection.OrthoGraphic))
                    accumulatedZoom *= -1;
            }
#endif
            if (Mathf.Abs(accumulatedZoom) > 0.01f)
            {
                if (cameraProjection.Equals(CameraProjection.Perspective))
                {
                    if ((-zoomNode.transform.localPosition.z < zoomMinHeight && accumulatedZoom > 0) ||
                        (-zoomNode.transform.localPosition.z > zoomMaxHeight && accumulatedZoom < 0))
                    {
                        accumulatedZoom = 0;
                    }

                    if (GuideMager.Instance.isGuide)
                    {
                        if (GuideMager.Instance.isZoom)
                        {
                            if (accumulatedZoom > 0.02 || accumulatedZoom < -0.02)
                            {
                                GuideMager.Instance.CompleteGuide(GuideStep.Zooming);
                                //Debug.Log(" zoomNode  Translate  "+accumulatedZoom);
                            }
                        }
                    }

                    zoomNode.Translate(Vector3.forward * accumulatedZoom);
                }
                else
                {
                    if ((ctCamera.orthographicSize + accumulatedZoom < zoomProjectionMax && accumulatedZoom > 0) ||
                        (ctCamera.orthographicSize + accumulatedZoom > zoomProjectionMin && accumulatedZoom < 0))
                    {
                        ctCamera.orthographicSize += accumulatedZoom;

                        //MOVE THE CAMERA OUT TO AVOID CLIPPING
                        zoomNode.transform.localPosition = new Vector3(zoomNode.transform.localPosition.x,
                            zoomNode.transform.localPosition.y, ctCamera.orthographicSize * -5f);
                    }
                    else
                    {
                        accumulatedZoom = 0;
                    }

                }

                //DECREASE THE INERTIA OVER TIME
                accumulatedZoom *= Mathf.Pow(zoomInertia / 1000f, GetTimeSinceLastFrame());

                //CLAMP
                if (Mathf.Abs(accumulatedZoom) < 0.01f)
                    accumulatedZoom = 0f;
            }
        }


        private bool isBtnRotate = false;
        private bool isBtnsuspend = false;
        
        private void BtnRotate()
        {
            float delta = screenScaler * 1f * GetTimeSinceLastFrame();

            if (isBtnRotate)
            {
                delta *= -1;
            }

            accumulatedRotation = rotationSpeed * delta;
            
            
            if (GuideMager.Instance.isGuide)
            {
                if (GuideMager.Instance.isRotate)
                {
                    if (accumulatedRotation > 0.04 || accumulatedRotation < -0.04)
                    {
                        GuideMager.Instance.CompleteGuide(GuideStep.Rotating);
                        // Debug.Log(" Rotate  accumulatedRotation  " + accumulatedRotation);
                    }
                }
            }

            transform.localEulerAngles += new Vector3(0, accumulatedRotation, 0);

            //DECREASE THE INERTIA OVER TIME
            accumulatedRotation *= Mathf.Pow(rotationInertia / 1000f, GetTimeSinceLastFrame());

            //CLAMP
            if (Mathf.Abs(accumulatedRotation) < 0.02f)
                accumulatedRotation = 0f;
        }
        //ROTATE MOTION
        private void Rotate()
        {
            if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                //CHECK IF MOVING VERTICALLY
                if (Mathf.Abs(Input.GetTouch(0).deltaPosition.y) < Mathf.Abs(Input.GetTouch(0).deltaPosition.x) && Mathf.Abs(Input.GetTouch(1).deltaPosition.y) < Mathf.Abs(Input.GetTouch(1).deltaPosition.x))
                {
                    return;
                }

                //CHECK IF FINGERS ARE IN OPPOSITE REGIONS OF THE SCREEN
                if ((Input.GetTouch(0).position.x < Screen.width / 2 && Input.GetTouch(1).position.x < Screen.width / 2) || (Input.GetTouch(0).position.x > Screen.width / 2 && Input.GetTouch(1).position.x > Screen.width / 2))
                {
                    return;
                }

                //CHECK IF FINGERS ARE MOVING IN OPPOSITE DIRECTIONS
                if (Input.GetTouch(0).deltaPosition.y * Input.GetTouch(1).deltaPosition.y < 0 && canControl)
                {

                    //GET DELTA MOVEMENT
                    float delta = screenScaler * Mathf.Max(Mathf.Abs(Input.GetTouch(0).deltaPosition.y), Mathf.Abs(Input.GetTouch(1).deltaPosition.y)) * GetTimeSinceLastFrame();

                    //SET SIGN (ROTATE LEFT OR RIGHT)
                    if ((Input.GetTouch(0).deltaPosition.y > 0 && Input.GetTouch(0).position.x < Screen.width / 2) || (Input.GetTouch(0).deltaPosition.y < 0 && Input.GetTouch(0).position.x > Screen.width / 2))
                        delta *= -1;

                    accumulatedRotation = rotationSpeed * delta;
                }

            }
#if UNITY_EDITOR

            if (enableEditorEmulation && isMouseDown && Input.GetKey(editorRotateKey) && canControl)
            {
                float delta = screenScaler * mouseDelta.y * GetTimeSinceLastFrame();

                accumulatedRotation = rotationSpeed * delta;
            }
#endif
            
            if (GuideMager.Instance.isGuide)
            {
                if (GuideMager.Instance.isRotate)
                {
                    if (accumulatedRotation > 0.04 || accumulatedRotation < -0.04)
                    {
                        GuideMager.Instance.CompleteGuide(GuideStep.Rotating);
                        // Debug.Log(" Rotate  accumulatedRotation  " + accumulatedRotation);
                    }
                }
            }
            transform.localEulerAngles += new Vector3(0, accumulatedRotation, 0);

            //DECREASE THE INERTIA OVER TIME
            accumulatedRotation *= Mathf.Pow(rotationInertia / 1000f, GetTimeSinceLastFrame());

            //CLAMP
            if (Mathf.Abs(accumulatedRotation) < 0.02f)
                accumulatedRotation = 0f;

        }
        //TILT MOTION
        private void Tilt()
        {
            if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                //CHECK IF MOVING VERTICALLY
                if (Mathf.Abs(Input.GetTouch(0).deltaPosition.y) < Mathf.Abs(Input.GetTouch(0).deltaPosition.x) && Mathf.Abs(Input.GetTouch(1).deltaPosition.y) < Mathf.Abs(Input.GetTouch(1).deltaPosition.x))
                {
                    return;
                }

                //CHECK IF FINGERS LAY ON OPPOSITE REGIONS OF THE SCREEN
                if ((Input.GetTouch(0).position.x < Screen.width / 2 && Input.GetTouch(1).position.x < Screen.width / 2) || (Input.GetTouch(0).position.x > Screen.width / 2 && Input.GetTouch(1).position.x > Screen.width / 2))
                {
                    return;
                }

                //CHECK IF MOVE IN THE SAME DIRECTION
                if (Input.GetTouch(0).deltaPosition.y * Input.GetTouch(1).deltaPosition.y > 0 && canControl)
                {
                    //GET DELTA MOVEMENT 
                    float delta = screenScaler * Mathf.Max(Mathf.Abs(Input.GetTouch(0).deltaPosition.y), Mathf.Abs(Input.GetTouch(1).deltaPosition.y)) * GetTimeSinceLastFrame();

                    //SET SIGN
                    if (Input.GetTouch(0).deltaPosition.y > 0)
                        delta *= -1;

                    accumulatedTilt = tiltSpeed * delta;

                }

            }
#if UNITY_EDITOR

            if (enableEditorEmulation && isMouseDown && Input.GetKey(editorTiltKey) && canControl)
            {
                float delta = screenScaler * -mouseDelta.y * GetTimeSinceLastFrame();

                accumulatedTilt = tiltSpeed * delta;

            }
#endif

            if (GuideMager.Instance.isGuide)
            {
                if (GuideMager.Instance.isTile)
                {
                    if (accumulatedTilt > 0.1 || accumulatedTilt < -0.1)
                    {
                        GuideMager.Instance.CompleteGuide(GuideStep.Lift);
                        //Debug.Log(" Tilt  accumulatedTilt  " + accumulatedTilt);
                    }
                }
            }
            
            tiltNode.localEulerAngles += new Vector3(accumulatedTilt, 0, 0);

            //DECREASE THE INERTIA OVER TIME
            accumulatedTilt *= Mathf.Pow(tiltInertia / 1000f, GetTimeSinceLastFrame());

            //CLAMP JITTERY MOVEMENT
            if (Mathf.Abs(accumulatedTilt) < 0.02f)
                accumulatedTilt = 0f;

            if (tiltNode.localEulerAngles.x > tiltMaxAngle)
                tiltNode.localEulerAngles = new Vector3(tiltMaxAngle, 0, 0);

            if (tiltNode.localEulerAngles.x < tiltMinAngle)
                tiltNode.localEulerAngles = new Vector3(tiltMinAngle, 0, 0);
        }
        private void CameraFollow()
        {
            //IF DONT HAVE TARGET RETURN
            if (!camFollowTarget) return;

            //PAN ACTION OVERRIDES CAM FOLLOW
            if (accumulatedDrag != Vector2.zero) return;

            transform.position = Vector3.Lerp(transform.position, camFollowTarget.position, GetTimeSinceLastFrame() * camFollowSpeed);

            //CHECK OUT OF BOUNDS CANCELS TARGET
            if (outOfBoundsCancelsTarget)
            {
                if (transform.position.x < NodeManager.instance.nodePositions[3].x || transform.position.x > NodeManager.instance.nodePositions[1].x
                || transform.position.z < NodeManager.instance.nodePositions[3].z || transform.position.z > NodeManager.instance.nodePositions[1].z)
                {
                    float x = Mathf.Clamp(transform.position.x, NodeManager.instance.nodePositions[3].x, NodeManager.instance.nodePositions[1].x);
                    float z = Mathf.Clamp(transform.position.z, NodeManager.instance.nodePositions[3].z, NodeManager.instance.nodePositions[1].z);

                    transform.position = new Vector3(x, transform.position.y, z);
                    camFollowTarget = null;
                }
            }


        }
        //CHECKS IF TOUCH WAS OVER UI
        private bool IsPointerOverUIObject()
        {
            if (!EventSystem.current)
            {
                Debug.LogError("In order to ignore gestures over UI, EventSystem is required in the scene. On Top Bar go to GameObject > UI > EventSystem to add one to the scene");
                return false;
            }

            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
        //CHECK IS ANY OR ALL OF THE TOUCHES MATCH REQUESTED STATE
        private bool CheckTouchState(TouchPhase inputTouchPhase, bool needAllToMatch = false)
        {
#if UNITY_EDITOR
            //NOTE: CURRENTLY THIS DOESNT SUPPORT EDITOR MOUSE DETECTION FOR CANCELLED STATIONARY AND MOVED
            if (inputTouchPhase.Equals(TouchPhase.Began) && Input.GetMouseButtonDown(0))
                return true;

            else if (inputTouchPhase.Equals(TouchPhase.Ended) && Input.GetMouseButtonUp(0))
                return true;

#endif
            //CHECK ALL TOUCHES
            for (int i = 0; i < Input.touchCount; i++)
            {
                //IF ANY MATCHES ENQUIRED STATE AND NEED JUST 1 TO MATCH
                if (Input.GetTouch(i).phase.Equals(inputTouchPhase) && !needAllToMatch)
                {
                    return true;
                }
                //ELSE IF NEED ALL TO MATCH AND 1 DOESNT, RETURN FALSE;
                else
                {
                    if (needAllToMatch)
                        return false;
                }
            }
            //IF NO TOUCHES OR DIFFERENT STATE RETURN FALSE
            return false;
        }
        #endregion
        #region USER API
        /// <summary>
        /// Sets transform target to follow
        /// </summary>
        /// <param name="target">Transform target to follow</param>
        public static void SetTarget(Transform target)
        {
            if (CTController.instance.enableCamFollow)
                CTController.instance.camFollowTarget = target;
            else
                Debug.LogWarning("Camera follow is Disabled in options, but you are still trying to set the target");
        }
        /// <summary>
        /// Pauses camera follow until ResumeCameraFollow is called
        /// </summary>
        public static void PauseCameraFollow()
        {
            pauseCameraFollow = true;
        }
        /// <summary>
        /// Resumes camera follow after it was paused by PauseCameraFollow()
        /// </summary>
        public static void ResumeCameraFollow()
        {
            pauseCameraFollow = false;
        }
        /// <summary>
        /// Checks if camera follow is currently paused
        /// </summary>
        /// <returns>
        /// true - if camera follow has been paused 
        /// false = if not
        /// </returns>
        public static bool IsCameraFollowPaused()
        {
            return pauseCameraFollow;
        }
        /// <summary>
        /// Locks user input
        /// </summary>
        public static void LockInput()
        {
            lockInput = true;
        }
        /// <summary>
        /// Unlocks user input
        /// </summary>
        public static void UnlockInput()
        {
            lockInput = false;
        }
        /// <summary>
        /// Sets miminum bounds of the camera pan area
        /// </summary>
        /// <param name="x"> minimum bounds x coordinate</param>
        /// <param name="z"> minimum bounds z coordinate</param>
        public static void SetBoundsMin(float x, float z)
        {
            if (x >= NodeManager.instance.nodePositions[1].x || z >= NodeManager.instance.nodePositions[1].z)
            {
                Debug.LogError("Minimum bouns value cannot be greater than maximum, change maximum bounds using SetBoundsMax first");
                return;
            }
            NodeManager.instance.nodePositions[3] = new Vector3(x, NodeManager.instance.nodePositions[3].y, z);
            NodeManager.instance.nodePositions[0] = new Vector3(x, NodeManager.instance.nodePositions[0].y, NodeManager.instance.nodePositions[0].z);
            NodeManager.instance.nodePositions[2] = new Vector3(NodeManager.instance.nodePositions[2].x, NodeManager.instance.nodePositions[0].y, z);
        }
        /// <summary>
        /// Minimum bounds of camera pan area
        /// </summary>
        /// <returns>Vector3 minimum bounds coordinates, y value is always 0</returns>
        public static Vector3 GetBoundsMin()
        {
            return new Vector3(NodeManager.instance.nodePositions[3].x, 0, NodeManager.instance.nodePositions[3].z);
        }
        /// <summary>
        /// Sets maximum bounds of the camera pan area
        /// </summary>
        /// <param name="x"> maximum bounds x coordinate</param>
        /// <param name="z"> maximum bounds z coordinate</param>
        public static void SetBoundsMax(float x, float z)
        {
            if (x <= NodeManager.instance.nodePositions[3].x || z <= NodeManager.instance.nodePositions[3].z)
            {
                Debug.LogError("Maximum bouns value cannot be smaller than minimum, change minimum bounds using SetBoundsMin first");
                return;
            }
            NodeManager.instance.nodePositions[1] = new Vector3(x, NodeManager.instance.nodePositions[1].y, z);
            NodeManager.instance.nodePositions[2] = new Vector3(x, NodeManager.instance.nodePositions[2].y, NodeManager.instance.nodePositions[2].z);
            NodeManager.instance.nodePositions[0] = new Vector3(NodeManager.instance.nodePositions[0].x, NodeManager.instance.nodePositions[0].y, z);

        }
        /// <summary>
        /// Maximum bounds of camera pan area
        /// </summary>
        /// <returns>Vector3 maximum bounds coordinates, y value is always 0</returns>
        public static Vector3 GetBoundsMax()
        {
            return new Vector3(NodeManager.instance.nodePositions[1].x, 0, NodeManager.instance.nodePositions[1].z);
        }
        #endregion
    }

}