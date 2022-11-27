using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    //[SerializeField] private float moveSpeed;

    //public void TouchMove()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //        if (mousePos.x > 1)
    //        {
    //            transform.Translate(moveSpeed, 0f, 0f);
    //        }
    //        else if (mousePos.x < 1)
    //        {
    //            transform.Translate(-moveSpeed, 0f, 0f);
    //        }
    //    }
    //}

    [SerializeField] private Rigidbody rb;

    [SerializeField] private float sideMovementSpeed;
    [SerializeField] private float forwardSpeed, startSpeed;

    [SerializeField] private float maxXPos;
    [SerializeField] private float minXPos;

    private Vector2 pointA;
    private Vector2 pointB;
    private bool isTouching = false;

    private void Start()
    {
        forwardSpeed = startSpeed;
    }

    private void Update()
    {
        GetTouchInfo();
    }

    private void GetTouchInfo()
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

    private void FixedUpdate()
    {
        IncreaseForwardSpeed();

        if (IsOutsideOfTheRange())
        {
            Move(0f);
            rb.position = new Vector3(Mathf.Clamp(rb.position.x, minXPos, maxXPos), rb.position.y, rb.position.z);
        }
        else if (isTouching)
        {
            Vector2 displacement = pointB - pointA;
            Vector3 direction = new Vector3(displacement.normalized.x, displacement.normalized.y, 1f);

            SetMoveDirection(direction);           
        }
    }

    private void SetMoveDirection(Vector3 direction)
    {
        if (Mathf.Approximately(rb.position.x, minXPos))
        {
            if (direction.x > 0 + Mathf.Epsilon)
            {
                Move(direction.x);
            }
            else
            {
                Move(0);
            }
        }
        else if (Mathf.Approximately(rb.position.x, maxXPos))
        {
            if (direction.x < 0)
            {
                Move(direction.x);
            }
            else
            {
                Move(0);
            }
        }
        else
        {
            Move(direction.x);
        }
    }

    private void IncreaseForwardSpeed()
    {
        forwardSpeed += Mathf.Epsilon;
        rb.AddForce(Vector3.forward * forwardSpeed);
    }

    private bool IsOutsideOfTheRange() => rb.position.x + Mathf.Epsilon < minXPos || rb.position.x - Mathf.Epsilon > maxXPos;

    private void Move(float xChange)
    {
        rb.velocity = new Vector3(xChange * sideMovementSpeed, rb.velocity.y, rb.velocity.z);
    }
}
