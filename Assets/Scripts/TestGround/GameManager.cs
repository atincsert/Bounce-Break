using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        BallMovement.OnWaterTouch += Restart;
    }

    private void OnDisable()
    {
        BallMovement.OnWaterTouch -= Restart;
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
