using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float lightningSpeedValue;
    [SerializeField] private float lightningPowerTimeLimit;

    private bool hasEffect;
    public bool HasEffect { get => hasEffect; set => hasEffect = value; }

    private void OnTriggerEnter(Collider other)
    {
        HasteEffect(other);
    }

    private IEnumerator HasteEffect(Collider other)
    {
        var hammer = other.GetComponent<Rigidbody>();
        hammer.velocity += Vector3.forward * lightningSpeedValue;
        hasEffect = true;
        yield return new WaitForSeconds(lightningPowerTimeLimit);
        hammer.velocity -= Vector3.forward * lightningSpeedValue;
        hasEffect = false;
    }
}
