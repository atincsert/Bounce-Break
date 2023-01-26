using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerMover : MonoBehaviour, IPlayerMover
{
    public static Action OnTrambolineCollision; 

    [SerializeField] private float forwardSpeed;
    [SerializeField] private Vector3 specialDownspeedValues = new Vector3(0f, -100f, 10f);
    [SerializeField] private Vector3 specialUpSpeedValues = new Vector3(0f, 50f, 10f);
    [SerializeField] private float velocityThreshold;
    [SerializeField] private float minAllowedBreakSpeed;
    [SerializeField] private float minForwardSpeed, maxForwardSpeed;

    [SerializeField] private Pickup[] lightningPickups;

    private Rigidbody rb;
    private Vector3 downVelocity;
    private Vector3 upVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lightningPickups = FindObjectsOfType<Pickup>(true);
    }

    private void Start()
    {
        rb.AddForce(Vector3.forward * forwardSpeed, ForceMode.Impulse);
    }

    private void OnEnable()
    {
        // activate lightning pickups
        foreach (Pickup lightningPickup in lightningPickups)
        {
            Debug.Log($"Found pickup");
            lightningPickup.gameObject.SetActive(true);
        }

        OnTrambolineCollision += BounceConditions;
    }

    private void OnDisable()
    {
        // deactivate lightning pickups
        foreach (Pickup lightningPickup in lightningPickups)
        {
            if (!lightningPickup.gameObject.activeInHierarchy) return;

            lightningPickup.gameObject.SetActive(false);
        }

        OnTrambolineCollision -= BounceConditions;
    }

    private void FixedUpdate()
    {
        SpeedUp();
        SpeedUpDownwardsWhenHoldTouch();
        RestrictMaxHeight();
    }

    public bool Breakable()
    {
        if (rb.velocity.z >= minAllowedBreakSpeed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void LookForwardAngle()
    {
        // this is where arrow or hammer looks to the direction of motion with an angle 
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

    private void CheckForUpwardVelocityIncreaseDownForce()
    {
        upVelocity = rb.velocity;
        if (upVelocity.y > velocityThreshold)
        {
            upVelocity -= specialUpSpeedValues * Time.deltaTime;
        }
        else
            upVelocity = rb.velocity;

        rb.velocity = upVelocity;
    }

    private void Bounce()
    {
        rb.velocity += Vector3.up;
    }

    public void BounceConditions()
    {
        Bounce();
    }

    public void RestrictMaxHeight()
    {
        CheckForUpwardVelocityIncreaseDownForce();
    }

    public void SpeedUp()
    {
        forwardSpeed += Time.deltaTime * 0.1f;
        forwardSpeed = Mathf.Clamp(forwardSpeed, minForwardSpeed, maxForwardSpeed);
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, forwardSpeed);
    }
}
