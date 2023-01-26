using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerMover : MonoBehaviour, IRestrictable
{
    public static Action OnHammerDeath; 

    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float minPosChangeInXViaTouch, maxPosChangeInXViaTouch;
    [SerializeField] private float minPosChangeInYViaTouch, maxPosChangeInYViaTouch;
    [SerializeField] private float minAllowedBreakSpeed;
    
    [SerializeField] private Pickup[] lightningPickups;

    private bool isTouching = false;
    private Vector2 pointA;
    private Vector2 pointB;
    private Rigidbody rb;
    private Vector2 mousePos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lightningPickups = FindObjectsOfType<Pickup>(true);
    }

    private void Start()
    {
        forwardSpeed = 5f;
        rb.velocity = Vector3.forward * forwardSpeed;
        //rb.AddForce(Vector3.forward * forwardSpeed, ForceMode.Impulse);
    }

    private void OnEnable()
    {
        // activate lightning pickups
        foreach (Pickup lightningPickup in lightningPickups)
        {
            Debug.Log($"Found pickup");
            lightningPickup.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        // deactivate lightning pickups
        foreach (Pickup lightningPickup in lightningPickups)
        {
            if (!lightningPickup.gameObject.activeInHierarchy) return;

            lightningPickup.gameObject.SetActive(false);
        }
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
            //pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.transform.position.z));
        }
        else
        {
            var lastMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(lastMousePos.x, lastMousePos.y, Camera.main.transform.position.z));
            isTouching = false;
        }

    }

    private void FixedUpdate()
    {
        if (isTouching)
        {
            Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
            rb.velocity = new Vector3(direction.x * speed, direction.y * speed, forwardSpeed);
        }
        else
        {
            Vector3 stopVerticallyAndHorizontally = new Vector3(0, 0, forwardSpeed);
            rb.velocity = stopVerticallyAndHorizontally;
        }
    }

    //private void FixedUpdate()
    //{
    //    Breakable();

    //    if (isTouching)
    //    {
    //        Vector2 offset = pointB - pointA;
    //        Vector2 direction = Vector2.ClampMagnitude(offset, 1f);
    //        Move(direction * -1);
    //    }
    //    else
    //    {
    //        Vector3 stopVerticallyAndHorizontally = new Vector3(0, 0, rb.velocity.z);
    //        Move(stopVerticallyAndHorizontally);
    //    }
    //}

    //private void Move(Vector2 direction)
    //{
    //    transform.Translate(direction * speed * Time.deltaTime);
    //    Vector3 newVel = rb.velocity;
    //    newVel = new Vector3(direction.x * speed, direction.y * speed, rb.velocity.z);
    //    rb.velocity = newVel;
    //}

    private void UpdateSpeed(float currentForwardSpeed)
    {
        foreach (Pickup lightningPickup in lightningPickups)
        {
            if (lightningPickup.HasEffect) return;
        }
        forwardSpeed += Time.deltaTime * 0.1f;
        forwardSpeed = Mathf.Clamp(forwardSpeed, 5, 50);
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, forwardSpeed);
    }

    public void RestrictPosition()
    {
        float xClampedPos = Mathf.Clamp(rb.position.x, minPosChangeInXViaTouch, maxPosChangeInXViaTouch);
        float yClampedPos = Mathf.Clamp(rb.position.y, minPosChangeInYViaTouch, maxPosChangeInYViaTouch);

        transform.position = new Vector3(xClampedPos, yClampedPos, transform.position.z);
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
}
