using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class HammerMovement : MonoBehaviour, IPlayerMover
{
    public static Action OnTrambolineCollision;

    [SerializeField] private float forwardSpeed;
    [SerializeField] private Vector3 specialDownspeedValues = new Vector3(0f, -100f, 10f);
    [SerializeField] private Vector3 specialUpSpeedValues = new Vector3(0f, 50f, 10f);
    [SerializeField] private float velocityThreshold;
    [SerializeField] private Vector3 minAllowedBreakSpeed;
    [SerializeField] private float minForwardSpeed, maxForwardSpeed;
    //[SerializeField] private float magnitude = 10f;
    [SerializeField] private Pickup[] lightningPickups;
    public float currentAcceleration;
    private float accelerationSpeed = 0.009f;
    private float maxSpeed = 30;

    private Rigidbody rb;
    private Vector3 downVelocity;
    private Vector3 upVelocity;
    Vector3 velocity, desiredVelocity; // new
    private bool hasEffect;
    public bool HasEffect { get => hasEffect; set => hasEffect = value; }

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
        //foreach (Pickup lightningPickup in lightningPickups)
        //{
        //    lightningPickup.gameObject.SetActive(true);
        //}

        OnTrambolineCollision += Bounce;
        Pickup.OnPickedUp += InitializeHaste;
    }

    private void OnDisable()
    {
        Pickup.OnPickedUp -= InitializeHaste;
        //if (lightningPickups == null || lightningPickups.Length <= 0) return;

        // deactivate lightning pickups
        //foreach (Pickup lightningPickup in lightningPickups)
        //{
        //    if (lightningPickup != null && lightningPickup.gameObject != null && lightningPickup.gameObject.activeInHierarchy)
        //        lightningPickup.gameObject.SetActive(false);
        //}

        OnTrambolineCollision -= Bounce;
    }

    private void LateUpdate()
    {
        SetLookForwardAngle();
    }

    private void FixedUpdate()
    {
        if (hasEffect) return;

        SpeedUp();
        SpeedUpDownwardsWhenHoldTouch();
        RestrictMaxHeight();
    }

    public bool Breakable()
    {
        if (rb.velocity.z >= minAllowedBreakSpeed.z || rb.velocity.y >= minAllowedBreakSpeed.y)
        {
            return true;
        }
        return false;
    }

    private void SetLookForwardAngle()
    {
        // this is where arrow or hammer looks to the direction of motion with an angle 
        var direction = rb.velocity.normalized;
        var d = transform.InverseTransformDirection(direction);
        transform.localRotation *= Quaternion.LookRotation(d, transform.up);
    }

    private void SpeedUpDownwardsWhenHoldTouch()
    {
        if (!GameManager.IsGameRunning) return;

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
        // Before setting the velocity, check if that velocity is above or below the clamped velocity threshold

        upVelocity = rb.velocity;
        if (upVelocity.y > velocityThreshold)
        {
            upVelocity -= specialUpSpeedValues * Time.deltaTime;
        }
        else
            upVelocity = rb.velocity;

        rb.velocity = upVelocity;
    }

    private void InitializeHaste()
    {
        StartCoroutine(HasteEffect());
        // start the circular progress bar 5s to show power up is active
    }

    private IEnumerator HasteEffect()
    {
        foreach (Pickup pickup in lightningPickups)
        {
            /*Pickup _pickup =*/
            pickup.GetComponent<Pickup>(); /*GetComponent<Pickup>();*/
            rb.useGravity = false;
            rb.velocity = Vector3.forward * /*_pickup*/pickup.LightningSpeedValue;
            hasEffect = true;
            yield return new WaitForSeconds(/*_pickup*/pickup.LightningPowerTimeLimit);
            rb.useGravity = true;
            hasEffect = false;
        }
    }

    public void Bounce()
    {
        if (rb == null) return;

        rb.velocity += Vector3.up;
        //var newRbDirection = Vector3.Reflect(transform.InverseTransformDirection(rb.velocity.normalized), Vector3.up);
        //rb.velocity = newRbDirection.normalized * magnitude;
    }

    public void RestrictMaxHeight()
    {
        CheckForUpwardVelocityIncreaseDownForce();
    }

    public void DecelerateWhenCollided(float slowRate)
    {
        currentAcceleration = Mathf.Max(0.1f, currentAcceleration - slowRate);
    }

    public void SpeedUp()
    {
        currentAcceleration += accelerationSpeed * Time.deltaTime;
        currentAcceleration = Mathf.Clamp01(currentAcceleration);

        Vector3 calculatedVelocity = new Vector3(0, rb.velocity.y, currentAcceleration * maxSpeed);

        rb.velocity = calculatedVelocity;
    }
}