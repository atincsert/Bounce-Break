using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private float playerInput;
    private PlayerMove playerMove;

    private void Awake()
    {
        playerMove = FindObjectOfType<PlayerMove>();
    }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {

#if !UNITY_EDITOR

        // Touch Controls

#endif

        if (Input.GetMouseButton(0))
        {
            playerInput = Input.GetAxisRaw("Mouse X");
            CalculateMouseInput(playerInput);
        }
    }

    private void CalculateMouseInput(float playerInput)
    {
        if (playerInput + Mathf.Epsilon < 0f)
        {
            playerInput = -1f;
            playerMove.DesiredVelocity = playerInput * playerMove.Speed;
        }
        else if (playerInput - Mathf.Epsilon > 0f)
        {
            playerInput = 1f;
            playerMove.DesiredVelocity = playerInput * playerMove.Speed;
        }
        else
        {
            playerInput = 0f;
            playerMove.DesiredVelocity = 0f;
        }
    }
}
