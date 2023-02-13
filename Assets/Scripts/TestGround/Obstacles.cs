using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

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
                PointSystem.endZPos = transform.position.z;
                UIManager.OnPlayerDie?.Invoke();
            }
            if (gameObject.layer == woodObstacleLayer)
            {
                PointSystem.endZPos = transform.position.z;
                UIManager.OnPlayerDie?.Invoke();
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
                UIManager.OnPlayerDie?.Invoke();
            }
        }
        else if (other.gameObject.layer == hammerLayer)
        {
            if (FindObjectOfType<HammerMovement>().HasEffect == true)
            {
                gameObject.SetActive(false);
                return;
            }
            // if hammer has its unique power on, don't apply any of the slow effects
            if (gameObject.layer == glassObstacleLayer)
            {
                gameObject.SetActive(false);
            }
            if (gameObject.layer == woodObstacleLayer)
            {
                if (other.GetComponent<HammerMovement>().Breakable() == false)
                {
                    PointSystem.endZPos = transform.position.z;
                    UIManager.OnPlayerDie?.Invoke();
                }
                other.GetComponent<Rigidbody>().AddForce(Vector3.back * woodSlowRate, ForceMode.Impulse);
                gameObject.SetActive(false);
            }
        }
    }
}
