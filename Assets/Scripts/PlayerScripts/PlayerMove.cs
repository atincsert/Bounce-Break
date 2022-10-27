using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IMoveable
{
    [SerializeField, Range(0f, 200f)] private float speed;
    [SerializeField, Range(0f, 200f)] private float accelerationRate;
    [SerializeField] private ConstantForce gravityForce;
    [SerializeField] private float maxAllowedPos;

    private Rigidbody rb;
    private Vector3 velocity; 
    private float desiredVelocity;
    private Vector3 currentPos;

    public float Speed { get => speed; set => speed = value; }
    public float DesiredVelocity { get => desiredVelocity; set => desiredVelocity = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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

    private void FixedUpdate()
    {
        MoveForward();
        MoveHorizontally();
        RestrictMovementPos();
    }

    public void MoveHorizontally()
    {
        velocity = rb.velocity;
        float speedChange = accelerationRate * Time.fixedDeltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity, speedChange);
        rb.velocity = velocity;
    }

    public void MoveForward()
    {
        rb.AddForce(transform.forward * speed * Time.deltaTime);
    }

    public void RestrictMovementPos()
    {
        currentPos = transform.position;

        currentPos.x = Mathf.Clamp(transform.position.x, -maxAllowedPos, maxAllowedPos);

        transform.position = currentPos;
    }
}
