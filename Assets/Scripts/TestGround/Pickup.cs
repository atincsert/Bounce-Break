using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public static Action OnPickedUp;

    [SerializeField] private float lightningSpeedValue;
    [SerializeField] private float lightningPowerTimeLimit;

    public float LightningSpeedValue { get => lightningSpeedValue; }
    public float LightningPowerTimeLimit { get => lightningPowerTimeLimit; }

    //private bool hasEffect;
    //public bool HasEffect { get => hasEffect; set => hasEffect = value; }

    private void OnTriggerEnter(Collider other)
    {
        //StartCoroutine(HasteEffect(other));
        if (other.gameObject.layer == 11)
            OnPickedUp?.Invoke();

        //gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.SetActive(false);
    }

    //private IEnumerator HasteEffect(Collider other)
    //{
    //    var hammer = other.GetComponent<Rigidbody>();
    //    hammer.velocity = Vector3.forward * lightningSpeedValue;
    //    hasEffect = true;
    //    Debug.Log($"{ hammer.velocity }");
    //    yield return new WaitForSeconds(lightningPowerTimeLimit);
    //    hammer.velocity -= Vector3.forward * lightningSpeedValue; // instead make use of rigidbody gravity property
    //    hasEffect = false;
    //    Debug.Log($"{ hammer.velocity }");
    //}
}
