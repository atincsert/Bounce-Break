using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public static event Action OnWaterTouch;

    [SerializeField] private float forwardSpeed;
   
    private Rigidbody rb;
    private Vector3 downVelocity;
    private Vector3 specialDownspeedValues = new Vector3(0, -100f, 10f);

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.AddForce(Vector3.forward * forwardSpeed, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        SpeedUpDownwardsWhenHoldTouch();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 4)
        {
            //Game Over send an event to restart of game over screen etc.
            OnWaterTouch?.Invoke();
        }
        if (collision.gameObject.layer == 6)
        {
            rb.velocity += Vector3.up;
        }               
    }

    private void SpeedUpDownwardsWhenHoldTouch()
    {
        if (Input.GetMouseButton(0))
        {
            downVelocity = rb.velocity;
            downVelocity += specialDownspeedValues * Time.deltaTime;
            rb.velocity = downVelocity;
        }

        downVelocity = rb.velocity;
    }
}
