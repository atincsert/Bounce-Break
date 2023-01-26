using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCollisionOptions : MonoBehaviour
{
    private const int waterLayer = 4, platformLayer = 6;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (gameObject.layer == platformLayer)
            {
                BallMovement.OnPlatformCollision?.Invoke();
            }
            else if (gameObject.layer == waterLayer)
            {
                BallMovement.OnWaterTouch?.Invoke();               
            }
        }
    }
}
