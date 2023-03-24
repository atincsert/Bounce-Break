using System;
using UnityEngine;

public class BallMovement : MonoBehaviour, IPlayerMover
{
    public static Action OnTrambolineCollision;

    [SerializeField] private float forwardSpeed;
    [SerializeField] private Vector3 specialDownspeedValues = new Vector3(0f, -100f, 10f);
    [SerializeField] private Vector3 specialUpSpeedValues = new Vector3(0f, 50f, 10f);
    [SerializeField] private float velocityThreshold;
    [SerializeField] private float minForwardSpeed, maxForwardSpeed;

    private Rigidbody rb;
    private Vector3 downVelocity;
    private Vector3 upVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.AddForce(Vector3.forward * forwardSpeed, ForceMode.Impulse);
    }

    private void OnEnable()
    {
        OnTrambolineCollision += Bounce;
    }

    private void OnDisable()
    {
        OnTrambolineCollision -= Bounce;
    }

    private void FixedUpdate()
    {
        SpeedUp();
        SpeedUpDownwardsWhenHoldTouch();
        RestrictMaxHeight();
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
        upVelocity = rb.velocity;
        if (upVelocity.y > velocityThreshold)
        {
            upVelocity -= specialUpSpeedValues * Time.deltaTime;
        }
        else
            upVelocity = rb.velocity;

        rb.velocity = upVelocity;
    }

    public void Bounce()
    {
        rb.velocity += Vector3.up;
        // new code
        //var customGravity = new Vector3(0, -100, 0);
        //Physics.gravity = customGravity;
    }

    public void RestrictMaxHeight()
    {
        CheckForUpwardVelocityIncreaseDownForce();
    }

    public void SpeedUp()
    {
        forwardSpeed += Time.deltaTime * 0.25f;
        forwardSpeed = Mathf.Clamp(forwardSpeed, minForwardSpeed, maxForwardSpeed);
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, forwardSpeed);
    }
}
