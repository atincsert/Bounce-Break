using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    private const float UILayer = 5, waterLayer = 4;
    private float cleanerSpeed = 5f;
    void Update()
    {
        GoForwardWithConstantSpeed();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == UILayer || other.gameObject.layer == waterLayer || other.gameObject.GetComponent<MeshCollider>()) return;
        Destroy(other.gameObject);
    }

    private void GoForwardWithConstantSpeed()
    {
        transform.Translate(Vector3.forward * cleanerSpeed * Time.deltaTime, Space.World );
    }
}
