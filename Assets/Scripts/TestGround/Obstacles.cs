using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class Obstacles : MonoBehaviour
{
    private const int glassObstacleLayer = 7, woodObstacleLayer = 8, ballLayer = 9, arrowLayer = 10, hammerLayer = 11;

    [SerializeField] private float glassSlowRate, woodSlowRate;

    private PointSystem pointSystem;
    private float currentZPos;

    private void Awake()
    {
        pointSystem = FindObjectOfType<PointSystem>();
    }

    private void Start()
    {
        currentZPos = transform.position.z;
    }

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
                pointSystem.endZPos = currentZPos;
                other.gameObject.SetActive(false);
                BallMovement.OnWaterTouch?.Invoke();
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
                pointSystem.endZPos = currentZPos;
                other.gameObject.SetActive(false);
                ArrowMovement.OnArrowDeath?.Invoke();
            }
        }
        else if (other.gameObject.layer == hammerLayer)
        {
            if (FindObjectOfType<Pickup>().HasEffect == true)
            {
                gameObject.SetActive(false);
                return;
            }
            // if hammer has its unique power on, don't apply any of the slow effects
            if (gameObject.layer == glassObstacleLayer)
            {
                other.GetComponent<Rigidbody>().AddForce(Vector3.back * glassSlowRate, ForceMode.Impulse);
                gameObject.SetActive(false);
            }
            if (gameObject.layer == woodObstacleLayer)
            {
                if (other.GetComponent<HammerMover>().Breakable() == false)
                {
                    pointSystem.endZPos = currentZPos;
                    other.gameObject.SetActive(false);
                    HammerMover.OnHammerDeath?.Invoke();
                }
                other.GetComponent<Rigidbody>().AddForce(Vector3.back * woodSlowRate, ForceMode.Impulse);
                gameObject.SetActive(false);
            }

        }
    }
}
