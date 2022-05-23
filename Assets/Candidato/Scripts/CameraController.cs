using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
    private CinemachineComponentBase camBody;

    private float yaw = 0.0f; 
    private float pitch = 0.0f;

    private float d = 0f;

    private void Awake()
    {
        EnhancedTouchSupport.Enable();
    }

    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        camBody = cam.GetCinemachineComponent(CinemachineCore.Stage.Body);
    }

    public void Zoom(InputAction.CallbackContext context)
    {
#if UNITY_ANDROID || UNITY_IOS

        if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeFingers.Count > 1)
        {
            Vector2 finger1 = context.ReadValue<Vector2>();// Touchscreen.current.touches[0].position.ReadValue();
            Vector2 finger2 = Touchscreen.current.touches[1].position.ReadValue(); //UnityEngine.InputSystem.EnhancedTouch.Touch.activeFingers[1].screenPosition;// 

            if (d - (finger1 - finger2).sqrMagnitude > 0)
            {
                (camBody as CinemachineFramingTransposer).m_CameraDistance += context.ReadValue<Vector2>().normalized.magnitude;
            }

            if (d - (finger1 - finger2).sqrMagnitude < 0)
            {
                (camBody as CinemachineFramingTransposer).m_CameraDistance -= context.ReadValue<Vector2>().normalized.magnitude;
            }


            d = (finger1 - finger2).sqrMagnitude;
        }
        
#else
        
        (camBody as CinemachineFramingTransposer).m_CameraDistance -= context.ReadValue<Vector2>().normalized.y;

#endif



        float camDist = (camBody as CinemachineFramingTransposer).m_CameraDistance;

        if (camDist <= 0f)
        {
            (camBody as CinemachineFramingTransposer).m_CameraDistance = 0f;
        }

        if (camDist > 30)
        {
            (camBody as CinemachineFramingTransposer).m_CameraDistance = 30f;

        }

    }

    public void RightClickLook(InputAction.CallbackContext context)
    {
#if UNITY_ANDROID || UNITY_IOS

        if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeFingers.Count <= 1)
        {
#endif
            Vector2 look = context.ReadValue<Vector2>();

            pitch += look.x * 0.1f;
            yaw += look.y * 0.1f;

            yaw = yaw <= 0f ? 0f : yaw;
            yaw = yaw >= 90f ? 90f : yaw;

            cam.transform.rotation = Quaternion.Euler(yaw, pitch, 0.0f);

#if UNITY_ANDROID || UNITY_IOS

        }
#endif
    }

    public IEnumerator Focus(Transform tx)
    {
        PlayerController.state = PlayerController.PlayingState.Pause;
        cam.Follow = tx;
        yield return new WaitForSeconds(2.0f);
        cam.Follow = cam.LookAt;
        PlayerController.state = PlayerController.PlayingState.Playing;
}
}
