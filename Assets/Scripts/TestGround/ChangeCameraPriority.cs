using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeCameraPriority : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera ballCamera;
    [SerializeField] private CinemachineVirtualCamera arrowCamera;
    [SerializeField] private CinemachineVirtualCamera hammerCamera;

    private bool isBallCamera = true;

    private void Start()
    {
        ballCamera.Priority = 1;
        // if arrow / hammer is selected change the priority here
    }

    private void SwitchPriority()
    {
        if (ballCamera.Priority == 1)
        {
            arrowCamera.Priority = 0;
            hammerCamera.Priority = 0;
        }
        else 
        {
            if (arrowCamera.Priority == 1)
            {
                ballCamera.Priority = 0;
                hammerCamera.Priority = 0;
            }
            if (hammerCamera.Priority == 1)
            {
                ballCamera.Priority = 0;
                arrowCamera.Priority = 0;
            }
        }

        isBallCamera = false;
    }
}
