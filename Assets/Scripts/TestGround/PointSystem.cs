using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public static Action OnFirstThresholdPassed;
    public static Action OnSecondThresholdPassed;
    public static Action OnDisplayPoint;

    [SerializeField] private float startZPos;
    [SerializeField] private float firstThreshold;
    [SerializeField] private float secondThreshold;

    [HideInInspector] public float endZPos;
    private float point = 0;
    public float Point { get => point; private set => point = value; }
    private float distanceTraveled;
    // Point is calculated according to distance traveled
    private void OnEnable()
    {
        ArrowMovement.OnArrowDeath += DisplayPoint;
        HammerMover.OnHammerDeath += DisplayPoint;
        BallMovement.OnWaterTouch += DisplayPoint;
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
        OnDisplayPoint?.Invoke();

        point = distanceTraveled * 10f;
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
