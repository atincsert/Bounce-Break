using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeCameraPriority : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera Camera;

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
            Camera.Follow = ball.transform;
        }
        else if (arrowOnScreen)
        {
            Camera.Follow = arrow.transform;
        }
        else if (hammerOnScreen)
        {
            Camera.Follow = hammer.transform;
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

