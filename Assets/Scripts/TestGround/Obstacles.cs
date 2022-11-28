using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.layer == 7)
        {
            //other.GetComponent<Rigidbody>().velocity += Vector3.back * 10f;
            other.GetComponent<Rigidbody>().AddForce(Vector3.back * 2f, ForceMode.Impulse);
            gameObject.SetActive(false);
        }
        if (gameObject.layer == 8)
        {
            //other.GetComponent<Rigidbody>().velocity += Vector3.back * 50f;
            other.GetComponent<Rigidbody>().AddForce(Vector3.back * 10f, ForceMode.Impulse);
            gameObject.SetActive(false);
        }
    }
}
