using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMoveForward : MonoBehaviour
{
    public static event Action OnWaterTouch;

    [SerializeField] private float forwardSpeed;

    private Rigidbody rb;
    private Vector3 downVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveForward();
        SpeedUpWhenHoldTouch();
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            rb.velocity += Vector3.up;
        }
        if (collision.gameObject.layer == 4)
        {
            //Game Over send an event to restart of game over screen etc.
            OnWaterTouch?.Invoke();
        }
    }

    private void SpeedUpWhenHoldTouch()
    {
        if (Input.GetMouseButton(0))
        {
            downVelocity = rb.velocity;
            downVelocity += new Vector3(0f, -100f, 0f) * Time.deltaTime;
            rb.velocity = downVelocity;
        }

        downVelocity = rb.velocity;
    }
}
