using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        BallMoveForward.OnWaterTouch += Restart;
    }

    private void OnDisable()
    {
        BallMoveForward.OnWaterTouch -= Restart;
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
