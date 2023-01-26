using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkManager : MonoBehaviour
{
    [SerializeField] private GameObject hammer;
    [SerializeField] private Pickup[] lightningPickups;

    private void Awake()
    {
        lightningPickups = FindObjectsOfType<Pickup>();

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
    }
}
