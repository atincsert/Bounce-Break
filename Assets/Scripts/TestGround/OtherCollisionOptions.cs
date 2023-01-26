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
                BallMovement.OnTrambolineCollision?.Invoke();
            }
            else if (gameObject.layer == waterLayer)
            {
                UIManager.OnPlayerDie?.Invoke();               
            }
        }
        else if (collision.gameObject.layer == 10)
        {
            if (gameObject.layer == platformLayer)
            {
                ArrowMovement.OnTrambolineCollision?.Invoke();
            }
            else if (gameObject.layer == waterLayer)
            {
                UIManager.OnPlayerDie?.Invoke();
            }
        }
        else if (collision.gameObject.layer == 11)
        {
            if (gameObject.layer == platformLayer)
            {
                HammerMover.OnTrambolineCollision?.Invoke();
            }
            else if (gameObject.layer == waterLayer)
            {
                UIManager.OnPlayerDie?.Invoke();
            }
        }
    }
}
