using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private const int glassObstacleLayer = 7, woodObstacleLayer = 8, ballLayer = 9, arrowLayer = 10, hammerLayer = 11;

    [SerializeField] private float glassSlowRate, woodSlowRate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ballLayer)
        {
            if (gameObject.layer == glassObstacleLayer)
            {
                //other.GetComponent<Rigidbody>().velocity += Vector3.back * 10f;
                other.GetComponent<Rigidbody>().AddForce(Vector3.back * glassSlowRate, ForceMode.Impulse);
                gameObject.SetActive(false);
            }
            if (gameObject.layer == woodObstacleLayer)
            {
                //other.GetComponent<Rigidbody>().velocity += Vector3.back * 50f;
                other.GetComponent<Rigidbody>().AddForce(Vector3.back * woodSlowRate, ForceMode.Impulse);
                gameObject.SetActive(false);
            }
        }
        else if (other.gameObject.layer == arrowLayer)
        {
            if (gameObject.layer == glassObstacleLayer)
            {
                gameObject.SetActive(false);
            }
            if (gameObject.layer == woodObstacleLayer)
            {
                other.gameObject.SetActive(false);
            }
        }
        else if (other.gameObject.layer == hammerLayer)
        {
            // if hammer has its unique power on, don't apply any of the slow effects
            if (gameObject.layer == glassObstacleLayer)
            {
                other.GetComponent<Rigidbody>().AddForce(Vector3.back * glassSlowRate, ForceMode.Impulse);
                gameObject.SetActive(false);
            }
            if (gameObject.layer == woodObstacleLayer)
            {
                other.GetComponent<Rigidbody>().AddForce(Vector3.back * woodSlowRate, ForceMode.Impulse);
                gameObject.SetActive(false);
            }
        }
    }
}
