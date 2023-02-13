using System;
using UnityEngine;

public class ArrowMovement : MonoBehaviour, IPlayerMover
{
    public static Action OnTrambolineCollision;

    [SerializeField] private float forwardSpeed;
    [SerializeField] private Vector3 specialDownspeedValues = new Vector3(0f, -100f, 10f);
    [SerializeField] private Vector3 specialUpSpeedValues = new Vector3(0f, 50f, 10f);
    [SerializeField] private float velocityThreshold;
    [SerializeField] private float minForwardSpeed, maxForwardSpeed;
    //[SerializeField] private float magnitude = 10f;

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

    private void Update()
    {
        SetLookForwardAngle();
    }

    private void FixedUpdate()
    {
        SpeedUp();
        SpeedUpDownwardsWhenHoldTouch();
        RestrictMaxHeight();
    }

    private void SetLookForwardAngle()
    {
        var direction = rb.velocity.normalized;
        var d = transform.InverseTransformDirection(direction);
        //float speed = 10f;
        //transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.LookRotation(d, Vector3.up), speed * Time.deltaTime);
        transform.localRotation *= Quaternion.LookRotation(d, transform.up);
        //this is where arrow or hammer looks to the direction of motion with an angle
        //if (rb.velocity.y < -Mathf.Epsilon)
        //{
        //    transform.LookAt(Vector3.down);
        //}
        //else if (rb.velocity.y > Mathf.Epsilon)
        //{
        //    transform.LookAt(Vector3.up);
        //}
    }

    private void SpeedUpDownwardsWhenHoldTouch()
    {
        if (Input.GetMouseButton(0))
        {
            downVelocity = rb.velocity;
            downVelocity += specialDownspeedValues * Time.deltaTime;
            //magnitude += 0.5f;
            rb.velocity = downVelocity;
        }
        //if (Input.GetMouseButtonUp(0))
        //{
        //    magnitude = 10f;
        //}

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
        if (rb == null) return;
        
        rb.velocity += Vector3.up;
        //var newRbDirection = Vector3.Reflect(transform.InverseTransformDirection(rb.velocity.normalized), Vector3.up);
        //rb.velocity = newRbDirection.normalized * magnitude;
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
