using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeCameraPriority : MonoBehaviour
{
    public CinemachineVirtualCamera ballCamera;
    public CinemachineVirtualCamera arrowCamera;
    public CinemachineVirtualCamera hammerCamera;

    public GameObject ball;
    public GameObject arrow;
    public GameObject hammer;

    bool ballOnScreen;
    bool arrowOnScreen;
    bool hammerOnScreen;

    void Update()
    {
        // Check if the ball, arrow, or hammer is active and on screen
        ballOnScreen = ball.activeInHierarchy && IsOnScreen(ball);
        arrowOnScreen = arrow.activeInHierarchy && IsOnScreen(arrow);
        hammerOnScreen = hammer.activeInHierarchy && IsOnScreen(hammer);

        // Set the camera priorities based on which object is on screen
        if (ballOnScreen)
        {
            ballCamera.Priority = 10;
            arrowCamera.Priority = 0;
            hammerCamera.Priority = 0;
        }
        else if (arrowOnScreen)
        {
            ballCamera.Priority = 0;
            arrowCamera.Priority = 10;
            hammerCamera.Priority = 0;
        }
        else if (hammerOnScreen)
        {
            ballCamera.Priority = 0;
            arrowCamera.Priority = 0;
            hammerCamera.Priority = 10;
        }
    }

    bool IsOnScreen(GameObject obj)
    {
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        
        if (renderer == null)
        {
            Debug.Log($"renderer is null");
            return false;
        }

        return renderer.isVisible;
    }
}

