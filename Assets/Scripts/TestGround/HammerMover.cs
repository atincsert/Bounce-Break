using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerMover : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;

    private bool isTouching = false;
    private Vector2 pointA;
    private Vector2 pointB;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        if (Input.GetMouseButton(0))
        {
            isTouching = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        else
        {
            isTouching = false;
        }
    }

    private void FixedUpdate()
    {
        if (isTouching)
        {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1f);
            Move(direction * -1);
        }
        else
        {
            Vector3 stopVerticallyAndHorizontally = new Vector3(0, 0, rb.velocity.z);
            Move(stopVerticallyAndHorizontally);
        }
    }

    private void Move(Vector2 direction)
    {
        //transform.Translate(direction * speed * Time.deltaTime);
        Vector3 newVel = rb.velocity;
        newVel = new Vector3(direction.x * speed, direction.y * speed, rb.velocity.z);
        rb.velocity = newVel;

    }
}
