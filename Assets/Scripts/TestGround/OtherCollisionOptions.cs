using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCollisionOptions : MonoBehaviour
{
    private const int waterLayer = 4, platformLayer = 6, ballLayer = 9, arrowLayer = 10, hammerLayer = 11;

    private float timer;
    private bool hasTimer;

    private void Update()
    {
        if (hasTimer)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                hasTimer = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!Mathf.Approximately(timer, 0f)) return;

        if (collision.gameObject.layer == ballLayer)
        {
            if (gameObject.layer == platformLayer)
            {
                BallMovement.OnTrambolineCollision?.Invoke();
            }
            else if (gameObject.layer == waterLayer)
            {
                PointSystem.endZPos = collision.gameObject.transform.position.z;
                UIManager.OnPlayerDie?.Invoke();               
            }
        }
        else if (collision.gameObject.layer == arrowLayer)
        {
            if (gameObject.layer == platformLayer)
            {
                ArrowMovement.OnTrambolineCollision?.Invoke();
                timer = 1f;
                hasTimer = true;
            }
            else if (gameObject.layer == waterLayer)
            {
                PointSystem.endZPos = collision.gameObject.transform.position.z;
                UIManager.OnPlayerDie?.Invoke();
            }
        }
        else if (collision.gameObject.layer == hammerLayer)
        {
            if (gameObject.layer == platformLayer)
            {
                HammerMovement.OnTrambolineCollision?.Invoke();
                timer = 1f;
                hasTimer = true;
            }
            else if (gameObject.layer == waterLayer)
            {
                PointSystem.endZPos = collision.gameObject.transform.position.z;
                UIManager.OnPlayerDie?.Invoke();
            }
        }
    }
}
