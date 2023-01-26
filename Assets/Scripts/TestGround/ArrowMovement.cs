using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour, IRestrictable
{
    public static Action OnArrowDeath;

    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float minPosChangeInXViaTouch, maxPosChangeInXViaTouch;
    [SerializeField] private float minPosChangeInYViaTouch, maxPosChangeInYViaTouch;
 

    private bool isTouching = false;
    private Vector2 pointA;
    private Vector2 pointB;
    private Rigidbody rb;
    private Vector2 mouseButtonUpPos;
    private Vector2 mousePos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        forwardSpeed = 5f;
        rb.velocity = Vector3.forward * forwardSpeed;
        //rb.AddForce(Vector3.forward * forwardSpeed, ForceMode.Impulse);
    }

    private void Update()
    {
        RestrictPosition();
        UpdateSpeed(forwardSpeed);

        //if (Input.GetMouseButtonDown(0))
        //{
        //    pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));           
        //}
        if (Input.GetMouseButton(0))
        {
            isTouching = true;
            mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //pointB = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.transform.position.z));
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.transform.position.z)); // new line
        }
        else
        {
            var lastMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(lastMousePos.x, lastMousePos.y, Camera.main.transform.position.z)); // new line 
            isTouching = false;
        }
    }

    private void FixedUpdate()
    {
        if (isTouching)
        {
            Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
            rb.velocity = new Vector3(direction.x * speed, direction.y * speed, /*rb.velocity.z*/forwardSpeed);
            //Move(direction);
        }
        else
        {
            Vector3 stopVerticallyAndHorizontally = new Vector3(0, 0, /*rb.velocity.z*/forwardSpeed);
            rb.velocity = stopVerticallyAndHorizontally;
            //Move(stopVerticallyAndHorizontally);
        }
    }


    //private void FixedUpdate()
    //{
    //    if (isTouching)
    //    {
    //        Vector2 offset = pointB - pointA;
    //        Vector2 direction = Vector2.ClampMagnitude(offset, 1f);
    //        Move(direction * -1);
    //    }
    //    else
    //    {
    //        //New Line
    //        if (Input.GetMouseButtonUp(0))
    //            mouseButtonUpPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

    //        Vector3 stopVerticallyAndHorizontally = new Vector3(mouseButtonUpPos.x, mouseButtonUpPos.y, rb.velocity.z);
    //        Move(stopVerticallyAndHorizontally);
    //    }
    //}

    //private void Move(Vector2 direction)
    //{
    //    float maxDistance = 1f;
    //    Vector2 movement = direction * speed + (Vector2)Vector3.forward * forwardSpeed;
    //    float distance = Vector2.Distance((Vector2)transform.position, (Vector2)transform.position + movement);
    //    if (distance > maxDistance)
    //    {
    //        movement = movement.normalized * maxDistance;
    //    }
    //    rb.velocity = movement;
    //}

    private void UpdateSpeed(float currentForwardSpeed)
    {
        forwardSpeed += Time.deltaTime * 0.1f; // Increase the speed by 0.1 every second
        forwardSpeed = Mathf.Clamp(forwardSpeed, 5, 50); // Clamp the speed between 0 and 10
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, forwardSpeed);
    }


    public void RestrictPosition()
    {
        float xClampedPos = Mathf.Clamp(transform.position.x, minPosChangeInXViaTouch, maxPosChangeInXViaTouch);
        float yClampedPos = Mathf.Clamp(transform.position.y, minPosChangeInYViaTouch, maxPosChangeInYViaTouch);

        StickToEdges(xClampedPos, yClampedPos);
        transform.position = new Vector3(xClampedPos, yClampedPos, transform.position.z);
    }

    private void StickToEdges(float clampedX, float clampedY)
    {
        if (transform.position.x == clampedX)
        {
            Mathf.Clamp(transform.position.x, minPosChangeInXViaTouch, maxPosChangeInXViaTouch);
        }
        if (transform.position.y == clampedY)
        {
            Mathf.Clamp(transform.position.y, minPosChangeInYViaTouch, maxPosChangeInYViaTouch);
        }
    }

}
