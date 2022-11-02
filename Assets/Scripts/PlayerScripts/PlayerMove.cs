using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IMoveable
{
    [SerializeField] private float horizontalSpeed, forwardSpeed, startSpeed;
    //[SerializeField, Range(0f, 200f)] private float accelerationRate;
    //[SerializeField] private ConstantForce gravityForce;
    [SerializeField] private float maxAllowedPosInX;
    [SerializeField] private Rigidbody rb;

    private bool isTouching = false;
    private InputManager inputManager;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
    }

    private void Start()
    {
        forwardSpeed = startSpeed;
    }

    #region Sliding the ball

    //Vector2 playerInput;
    //playerInput.x = Input.GetAxisRaw("Mouse X");
    //playerInput.y = Input.GetAxisRaw("Mouse Y");

    //playerInput = Vector2.ClampMagnitude(playerInput, 1f);
    //Vector3 desiredVelocity = new Vector3(playerInput.x, playerInput.y, transform.position.z) * speed;

    //float speedChange = accelerationRate * Time.deltaTime;
    //velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, speedChange);
    //velocity.y = Mathf.MoveTowards(velocity.y, desiredVelocity.y, speedChange);

    //Vector3 displacement = velocity * Time.deltaTime;
    //Vector3 newPosition = transform.localPosition + displacement;

    //if (newPosition.x < allowedArea.xMin)
    //{
    //    newPosition.x = allowedArea.xMin;
    //    velocity.x = 0f;
    //}
    //else if (newPosition.x > allowedArea.xMax)
    //{
    //    newPosition.x = allowedArea.xMax;
    //    velocity.x = 0f;
    //}
    //if (newPosition.y < allowedArea.yMin)
    //{
    //    newPosition.y = allowedArea.yMin;
    //    velocity.y = 0f;
    //}
    //else if (newPosition.y > allowedArea.yMax)
    //{
    //    newPosition.y = allowedArea.yMax;
    //    velocity.y = 0f;
    //}

    //transform.localPosition = newPosition;

    #endregion // For Arrow and Hammer movement type

    private void Update()
    {
        GetTouchInfo();
    }

    private void FixedUpdate()
    {
        MoveForward();
        if (IsOutOfRange())
        {
            Move(0);
        }
        else if (isTouching)
        {
            Vector2 displacement = inputManager.PointB - inputManager.PointA;
            Vector3 direction = new Vector3(displacement.normalized.x, displacement.normalized.y, 1f);

            MoveHorizontally(direction);
        }

    }

    private void GetTouchInfo()
    {
        inputManager.TouchInput();
    }

    public void MoveHorizontally(Vector2 direction)
    {
        if (Mathf.Approximately(rb.position.x, -maxAllowedPosInX))
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
        else if (Mathf.Approximately(rb.position.x, maxAllowedPosInX))
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

        //velocity = rb.velocity;
        //float speedChange = accelerationRate * Time.fixedDeltaTime;
        //velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity, speedChange);
        //rb.velocity = velocity;
    }

    public void MoveForward()
    {
        forwardSpeed += Mathf.Epsilon;
        rb.AddForce(Vector3.forward * forwardSpeed);
    }

    public bool IsOutOfRange() => rb.position.x + Mathf.Epsilon < -maxAllowedPosInX || rb.position.x - Mathf.Epsilon > maxAllowedPosInX;


    private void Move(float xPositionChange)
    {
        rb.velocity = new Vector3(xPositionChange * horizontalSpeed, rb.velocity.y, rb.velocity.z);
    }
}
