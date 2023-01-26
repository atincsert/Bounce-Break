using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkManager : MonoBehaviour
{
    [SerializeField] private GameObject hammer;
    [SerializeField] private Pickup[] lightningPickups;
    [SerializeField] private GameObject ball;
    [SerializeField] private Obstacles[] ballunrelatedplatforms;

    private void Awake()
    {
        lightningPickups = FindObjectsOfType<Pickup>();
        ballunrelatedplatforms = FindObjectsOfType<Obstacles>();

        if (hammer.activeInHierarchy)
        {
            foreach (Pickup lightningPickup in lightningPickups)
            {
                lightningPickup.gameObject.SetActive(true);
            }
        }
        if(!hammer.activeInHierarchy)
        {
            foreach (Pickup lightningPickup in lightningPickups)
            {
                lightningPickup.gameObject.SetActive(false);
            }
        }
        if (!ball.activeInHierarchy)
        {
            foreach (Obstacles platform in ballunrelatedplatforms)
            {
                platform.gameObject.SetActive(true);
            }
        }
        if(ball.activeInHierarchy)
        {
            foreach (Obstacles platform in ballunrelatedplatforms)
            {
                platform.gameObject.SetActive(false);
            }
        }
    }
}
