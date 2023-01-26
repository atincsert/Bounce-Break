using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class Obstacles : MonoBehaviour
{
    private const int glassObstacleLayer = 7, woodObstacleLayer = 8, ballLayer = 9, arrowLayer = 10, hammerLayer = 11;

    [SerializeField] private bool isGlass;
    [SerializeField] private bool isWood;
    [SerializeField] private float glassSlowRate, woodSlowRate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ballLayer)
        {
            if (gameObject.layer == glassObstacleLayer)
            {
                //other.GetComponent<Rigidbody>().velocity += Vector3.back * 10f;
                if (isGlass)
                {
                    other.GetComponent<Rigidbody>().AddForce(Vector3.back * glassSlowRate, ForceMode.Impulse);                
                }
                gameObject.SetActive(false);
            }
            if (gameObject.layer == woodObstacleLayer)
            {
                //other.GetComponent<Rigidbody>().velocity += Vector3.back * 50f;
                PointSystem.endZPos = transform.position.z;
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
                PointSystem.endZPos = transform.position.z;
                ArrowMovement.OnArrowDeath?.Invoke();
                other.gameObject.SetActive(false);
            }
        }
        else if (other.gameObject.layer == hammerLayer)
        {
            //if (FindObjectOfType<Pickup>().HasEffect == true)
            //{
            //    gameObject.SetActive(false);
            //    return;
            //}
            // if hammer has its unique power on, don't apply any of the slow effects
            if (gameObject.layer == glassObstacleLayer)
            {
                if (isGlass)
                {
                    other.GetComponent<Rigidbody>().AddForce(Vector3.back * glassSlowRate, ForceMode.Impulse);
                }
                gameObject.SetActive(false);
            }
            if (gameObject.layer == woodObstacleLayer)
            {
                if (other.GetComponent<HammerMover>().Breakable() == false)
                {
                    PointSystem.endZPos = transform.position.z;
                    HammerMover.OnHammerDeath?.Invoke();
                    other.gameObject.SetActive(false);
                }
                if (isWood)
                {
                    other.GetComponent<Rigidbody>().AddForce(Vector3.back * woodSlowRate, ForceMode.Impulse);
                }
                gameObject.SetActive(false);
            }
        }
    }
}
