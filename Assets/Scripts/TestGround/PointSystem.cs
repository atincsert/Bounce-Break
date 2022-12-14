using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public static Action OnFirstThresholdPassed;
    public static Action OnSecondThresholdPassed;

    [SerializeField] private float startZPos;
    [SerializeField] private float firstThreshold;
    [SerializeField] private float secondThreshold;

    [HideInInspector] public float endZPos;
    private float point = 0;
    private float distanceTraveled;
    // Point is calculated according to distance traveled
    private void OnEnable()
    {
        ArrowMovement.OnArrowDeath += DisplayPoint;
        HammerMover.OnHammerDeath += DisplayPoint;
    }

    private void Update()
    {
        CheckForThreshold();
    }

    private void CalculatePoint()
    {
        distanceTraveled = endZPos - startZPos;
    }

    private void DisplayPoint()
    {
        CalculatePoint();

        point = distanceTraveled * 10f;
        // display point in UI
        Debug.Log($"You got { point } points");
    }

    private void CheckForThreshold()
    {
        if (point >= firstThreshold)
        {
            OnFirstThresholdPassed?.Invoke();
        }
        if (point >= secondThreshold)
        {
            OnSecondThresholdPassed?.Invoke();
        }
    }

}
