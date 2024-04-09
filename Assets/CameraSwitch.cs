using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
public class CameraSwitch : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vcam1;
    [SerializeField] CinemachineVirtualCamera vcam2;
    [SerializeField] bool outCamera = true;
    [SerializeField] InputAction action;

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    private void Start()
    {
        action.performed += _ => SwitchPriority();
    }

    private void SwitchPriority()
    {
        if (outCamera)
        {
            vcam1.Priority = 0;
            vcam2.Priority = 1;
        } else
        {
            vcam1.Priority = 1;
            vcam2.Priority = 0;
        }
        outCamera = !outCamera;
    }
}
