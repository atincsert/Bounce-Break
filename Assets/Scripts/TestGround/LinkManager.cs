using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkManager : MonoBehaviour
{
    [SerializeField] private GameObject hammer;
    [SerializeField] private GameObject[] lightningPickups;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject[] ballunrelatedplatforms;

    private void Awake()
    {
        if (hammer.activeInHierarchy)
        {
            foreach (GameObject lightningPickup in lightningPickups)
            {
                lightningPickup.SetActive(true);
            }
        }
        if(!hammer.activeInHierarchy)
        {
            foreach (GameObject lightningPickup in lightningPickups)
            {
                lightningPickup.SetActive(false);
            }
        }
        if (ball.activeInHierarchy)
        {
            foreach (GameObject platform in ballunrelatedplatforms)
            {
                platform.SetActive(true);
            }
        }
        if(ball.activeInHierarchy)
        {
            foreach (GameObject platform in ballunrelatedplatforms)
            {
                platform.SetActive(false);
            }
        }
    }
}
