using UnityEngine;
using CTMobileCameraTools;
using UnityEditorInternal;
#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(CTController))]
public class CTControllerEditor : Editor
{
    private static Texture bannerTexture;
    public override void OnInspectorGUI()
    {
        if(!bannerTexture)
        bannerTexture = Resources.Load("banner") as Texture;
        CTController cc = (CTController)target;

        GUILayout.Box(bannerTexture);
        EditorGUILayout.HelpBox("Any setting's SPEED defines to velocity of the motion", MessageType.Info);
        EditorGUILayout.HelpBox("Any setting's INERTIA defines how long the given movement will continue after end of the gesture", MessageType.Info);


        //CAMERA PROJECTION
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Camera Projection Type", EditorStyles.boldLabel);

        EditorGUI.BeginChangeCheck();
        CTController.CameraProjection cameraProjection = (CTController.CameraProjection)EditorGUILayout.EnumPopup("Camera Projection", cc.cameraProjection);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change Camera Projection Type");
            cc.cameraProjection = cameraProjection;
        }

        //CAMERA PAN
        EditorGUILayout.LabelField("Camera Pan Settings", EditorStyles.boldLabel);
        EditorGUI.BeginChangeCheck();

        bool enablePan = EditorGUILayout.Toggle("Enable Pan", cc.enablePan);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Toggle Pan");
            cc.enablePan = enablePan;
        }

        if (cc.enablePan)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUI.BeginChangeCheck();

            float panSpeed = EditorGUILayout.FloatField("Pan Speed", cc.panSpeed);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Pan Speed");
                cc.panSpeed = panSpeed;
            }
            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();
            float panInertia = EditorGUILayout.FloatField("Pan Inertia", cc.panInertia);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Pan Inertia");
                cc.panInertia = panInertia;
            }

            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();

        }

        //CAMERA ZOOM 
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (cc.cameraProjection.Equals(CTController.CameraProjection.Perspective))
            EditorGUILayout.LabelField("Perspective Camera Zoom Settings", EditorStyles.boldLabel);
        else
            EditorGUILayout.LabelField("Orthographic Camera Zoom Settings", EditorStyles.boldLabel);

        EditorGUI.BeginChangeCheck();
        bool enableZoom = EditorGUILayout.Toggle("Enable Zoom", cc.enableZoom);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Toggle Zoom");
            cc.enableZoom = enableZoom;
        }
        if (cc.enableZoom)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUI.BeginChangeCheck();

            float zoomSensitivity = EditorGUILayout.FloatField("Zoom Speed", cc.zoomSensitivity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Zoom Sensitivity");
                cc.zoomSensitivity = zoomSensitivity;
            }

            EditorGUILayout.Space();

            if (cc.cameraProjection.Equals(CTController.CameraProjection.Perspective))
            {

                EditorGUILayout.LabelField("Minimum distance to which camera can zoom", EditorStyles.helpBox);

                EditorGUI.BeginChangeCheck();

                float zoomMinHeight = EditorGUILayout.FloatField("Min Zoom", cc.zoomMinHeight);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Change Min Zoom");
                    cc.zoomMinHeight = zoomMinHeight;
                }
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Maximum distance to which camera can zoom", EditorStyles.helpBox);

                EditorGUI.BeginChangeCheck();
                float zoomMaxHeight = EditorGUILayout.FloatField("Max Zoom", cc.zoomMaxHeight);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Change Max Zoom");
                    cc.zoomMaxHeight = zoomMaxHeight;
                }
            }
            else
            {
                EditorGUILayout.LabelField("Minimum projection size of orthographic camera", EditorStyles.helpBox);
                EditorGUI.BeginChangeCheck();

                float zoomProjectionMin = EditorGUILayout.FloatField("Min Projection Size", cc.zoomProjectionMin);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Change Min Projection");
                    cc.zoomProjectionMin = zoomProjectionMin;
                }

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Maximum projection size of orthographic camera", EditorStyles.helpBox);

                EditorGUI.BeginChangeCheck();

                float zoomProjectionMax = EditorGUILayout.FloatField("Max Projection Size", cc.zoomProjectionMax);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Change Max Projection");
                    cc.zoomProjectionMax = zoomProjectionMax;
                }
            }

            EditorGUILayout.Space();
            EditorGUI.BeginChangeCheck();

            float zoomInertia = EditorGUILayout.FloatField("Zoom Inertia", cc.zoomInertia);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Zoom Inertia");
                cc.zoomInertia = zoomInertia;
            }
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();

        }
        //ROTATION SETTINGS 
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Camera Rotation Settings", EditorStyles.boldLabel);

        EditorGUI.BeginChangeCheck();

        bool enableRotation = EditorGUILayout.Toggle("Enable Rotation", cc.enableRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Toggle Rotation");
            cc.enableRotation = enableRotation;
        }
        if (cc.enableRotation)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();

            float rotationSpeed = EditorGUILayout.FloatField("Rotation Speed", cc.rotationSpeed);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Rotation Speed");
                cc.rotationSpeed = rotationSpeed;
            }

            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();

            float rotationInertia = EditorGUILayout.FloatField("Rotation Inertia", cc.rotationInertia);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Rotation Inertia");
                cc.rotationInertia = rotationInertia;
            }

            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();

        }

        //TILT SETTINGS
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Camera Tilt Settings", EditorStyles.boldLabel);
        EditorGUI.BeginChangeCheck();

        bool enableTilt = EditorGUILayout.Toggle("Enable Tilt", cc.enableTilt);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Toggle Tilt");
            cc.enableTilt = enableTilt;
        }
        if (cc.enableTilt)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUI.BeginChangeCheck();

            float tiltSpeed = EditorGUILayout.FloatField("Tilt Speed", cc.tiltSpeed);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Tilt Speed");
                cc.tiltSpeed = tiltSpeed;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Minimum Tilt angle (towards horizontal plain)", EditorStyles.helpBox);

            EditorGUI.BeginChangeCheck();

            float tiltMinAngle = EditorGUILayout.FloatField("Min Angle", cc.tiltMinAngle);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Tilt Min Angle");
                cc.tiltMinAngle = tiltMinAngle;
            }
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Maximum Tilt angle (towards horizontal plain)", EditorStyles.helpBox);

            EditorGUI.BeginChangeCheck();

            float tiltMaxAngle = EditorGUILayout.FloatField("Max Angle", cc.tiltMaxAngle);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Tilt Max Angle");
                cc.tiltMaxAngle = tiltMaxAngle;
            }

            EditorGUILayout.Space();
            EditorGUI.BeginChangeCheck();

            float tiltInertia = EditorGUILayout.FloatField("Tilt Inertia", cc.tiltInertia);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Tilt Inertia");
                cc.tiltInertia = tiltInertia;
            }
            EditorGUILayout.Space();

            EditorGUILayout.EndVertical();

        }
        //INITIAL SETTINGS
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Initial Camera Settings", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Initial Tilt angle when starting the scene", EditorStyles.helpBox);

        EditorGUI.BeginChangeCheck();

        float initialCameraTilt = EditorGUILayout.FloatField("Initial Tilt Angle", cc.initialCameraTilt);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change Initial Tilt Angle");
            cc.initialCameraTilt = initialCameraTilt;
        }

        EditorGUILayout.Space();
        if (cc.cameraProjection.Equals(CTController.CameraProjection.Perspective))
            EditorGUILayout.LabelField("How far away the camera will zoom at start of the scene (needs to be between zoom bounds)", EditorStyles.helpBox);
        else
            EditorGUILayout.LabelField("Initial camera projection size (needs to be between projection bounds)", EditorStyles.helpBox);
            EditorGUI.BeginChangeCheck();

        float initialCameraZoom = EditorGUILayout.FloatField("Initial Camera Zoom", cc.initialCameraZoom);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change Initial Camera Zoom");
            cc.initialCameraZoom = initialCameraZoom;
        }
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Initial Y camera rotation angle when starting the scene", EditorStyles.helpBox);

        EditorGUI.BeginChangeCheck();

        float initialCameraRotation = EditorGUILayout.FloatField("Initial Rotation Angle", cc.initialCameraRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change Initial Rotation Angle");
            cc.initialCameraRotation = initialCameraRotation;
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Initial camera position when starting the scene, leave at (0,0,0) to ignore", EditorStyles.helpBox);


        EditorGUI.BeginChangeCheck();


        Vector3 initialCameraPosition = EditorGUILayout.Vector3Field("Initial Camera Position", cc.initialCameraPosition);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change initial Camera Position");
            cc.initialCameraPosition = initialCameraPosition;
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //OTHER SETTINGS

        EditorGUILayout.LabelField("Other Settings", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Time before new gesture can be executed in seconds", EditorStyles.helpBox);
        EditorGUI.BeginChangeCheck();

        float delayBetweenGestures = EditorGUILayout.FloatField("Delay between user gestures", cc.delayBetweenGestures);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change Delay between user gestures");
            cc.delayBetweenGestures = delayBetweenGestures;
        }
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("When enabled, gestures over Unity UI will be ignored", EditorStyles.helpBox);

        EditorGUI.BeginChangeCheck();

        bool ignoreTouchOverUI = EditorGUILayout.Toggle("Ignore Gestures Over UI", cc.ignoreTouchOverUI);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Toggle Ignore Gestures Over UI");
            cc.ignoreTouchOverUI = ignoreTouchOverUI;
        }
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("When enabled, changing Time.timescale will not affect camera gesture animation", EditorStyles.helpBox);

        EditorGUI.BeginChangeCheck();

        bool useUnscaledDeltaTime = EditorGUILayout.Toggle("Use Unscaled Delta Time", cc.useUnscaledDeltaTime);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Toggle Use Unscaled Delta Time");
            cc.useUnscaledDeltaTime = useUnscaledDeltaTime;
        }
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("When enabled, on start of every gesture a raycast with speciied layer will be cast against the scene, if hit, any gesture will be ignored", EditorStyles.helpBox);

        EditorGUI.BeginChangeCheck();

        bool prioritizeRaycastLayer = EditorGUILayout.Toggle("Priority Raycast Layer", cc.prioritizeRaycastLayer);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Toggle Priority Raycast Layer");
            cc.prioritizeRaycastLayer = prioritizeRaycastLayer;
        }
        if (cc.prioritizeRaycastLayer)
        {
            EditorGUI.BeginChangeCheck();
            var layersSelection = EditorGUILayout.MaskField("Raycast layers to prioritize", LayerMaskToField(cc.prioritizedLayerMask), InternalEditorUtility.layers);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(cc, "Change Raycast Layers");
                cc.prioritizedLayerMask = FieldToLayerMask(layersSelection);
            }
            EditorGUILayout.Space();
        }

        EditorGUILayout.EndVertical();

        //CAMERA FOLLOW SETTINGS
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Camera Follow Settings", EditorStyles.boldLabel);

        EditorGUI.BeginChangeCheck();

        bool enableCamFollow = EditorGUILayout.Toggle("Enable Camera Follow", cc.enableCamFollow);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Toggle Enable Camera Follow");
            cc.enableCamFollow = enableCamFollow;
        }
        if (cc.enableCamFollow)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();

            float camFollowSpeed = EditorGUILayout.FloatField("Camera Follow Speed", cc.camFollowSpeed);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Camera Follow Speed");
                cc.camFollowSpeed = camFollowSpeed;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Drag here transform to follow, alternatively you can set it via script", EditorStyles.helpBox);

            EditorGUI.BeginChangeCheck();

            var camFollowTarget = (Transform)EditorGUILayout.ObjectField(cc.camFollowTarget, typeof(Transform), true);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Camera Follow Target");
                cc.camFollowTarget = camFollowTarget;
            }
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("When Pan is enabled and user makes Pan gesture, it cancels camera follow and clears the target", EditorStyles.helpBox);

            EditorGUI.BeginChangeCheck();

            bool panCancelsTarget = EditorGUILayout.Toggle("Panning Cancels Target", cc.panCancelsTarget);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Toggle Panning Cancels Target");
                cc.panCancelsTarget = panCancelsTarget;
            }
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("When camera reaches bounds target following will be cancelled", EditorStyles.helpBox);

            EditorGUI.BeginChangeCheck();

            bool outOfBoundsCancelsTarget = EditorGUILayout.Toggle("Out Of Bounds Cancels Target", cc.outOfBoundsCancelsTarget);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Toggle Out Of Bounds Cancels Target");
                cc.outOfBoundsCancelsTarget = outOfBoundsCancelsTarget;
            }

            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
        }
        //EDITOR EMULATION SETTINGS
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Editor Emulation Settings", EditorStyles.boldLabel);

        EditorGUI.BeginChangeCheck();

        bool enableEditorEmulation = EditorGUILayout.Toggle("Enable Emulation In Editor", cc.enableEditorEmulation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Toggle Enable Emulation In Editor");
            cc.enableEditorEmulation = enableEditorEmulation;
        }
        if (cc.enableEditorEmulation)
        {

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Hold this key and swipe vertically to simulate Rotation gesture", EditorStyles.helpBox);

            EditorGUI.BeginChangeCheck();

            KeyCode editorRotateKey = (KeyCode)EditorGUILayout.EnumPopup("Rotate key", cc.editorRotateKey);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Rotate key ");
                cc.editorRotateKey = editorRotateKey;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Hold this key and swipe horizontally to simulate Zoom gesture", EditorStyles.helpBox);

            EditorGUI.BeginChangeCheck();

            KeyCode editorZoomKey = (KeyCode)EditorGUILayout.EnumPopup("Zoom key", cc.editorZoomKey);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Zoom key ");
                cc.editorZoomKey = editorZoomKey;
            }
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Hold this key and swipe vertically to simulate Tilt gesture", EditorStyles.helpBox);

            EditorGUI.BeginChangeCheck();

            KeyCode editorTiltKey = (KeyCode)EditorGUILayout.EnumPopup("Tilt key", cc.editorTiltKey);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Tilt key ");
                cc.editorTiltKey = editorTiltKey;
            }


            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
        }

    }
    private LayerMask FieldToLayerMask(int field)
    {
        LayerMask mask = 0;
        var layers = InternalEditorUtility.layers;
        for (int c = 0; c < layers.Length; c++)
        {
            if ((field & (1 << c)) != 0)
            {
                mask |= 1 << LayerMask.NameToLayer(layers[c]);
            }
        }
        return mask;
    }
    // Converts a LayerMask to a field value
    private int LayerMaskToField(LayerMask mask)
    {
        int field = 0;
        var layers = InternalEditorUtility.layers;
        for (int c = 0; c < layers.Length; c++)
        {
            if ((mask & (1 << LayerMask.NameToLayer(layers[c]))) != 0)
            {
                field |= 1 << c;
            }
        }
        return field;
    }

}
#endif