using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool isTouching = false;
    private Vector2 pointA;
    private Vector2 pointB;

    public Vector2 PointA { get => pointA; set => pointA = value; }
    public Vector2 PointB { get => pointB; set => pointB = value; }

    private void Update()
    {
        //GetInput();
        TouchInput();
    }

    public void TouchInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isTouching = true;
            pointA = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, 0, 0));
        }
        else if (Input.GetMouseButton(0))
        {
            pointA = pointB;
            isTouching = true;
            pointB = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, 0, 0));
        }
        else
        {
            isTouching = false;
        }
    }

    //private void GetInput()
    //{

    //    if (Input.GetMouseButton(0))
    //    {
    //        playerInput = Input.GetAxisRaw("Mouse X");
    //        CalculateMouseInput(playerInput);
    //    }
    //}

    //private void CalculateMouseInput(float playerInput)
    //{
    //    if (playerInput + Mathf.Epsilon < 0f)
    //    {
    //        playerInput = -1f;
    //        playerMove.DesiredVelocity = playerInput * playerMove.Speed;
    //    }
    //    else if (playerInput - Mathf.Epsilon > 0f)
    //    {
    //        playerInput = 1f;
    //        playerMove.DesiredVelocity = playerInput * playerMove.Speed;
    //    }
    //    else
    //    {
    //        playerInput = 0f;
    //        playerMove.DesiredVelocity = 0f;
    //    }
    //}
}
