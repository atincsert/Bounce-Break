using System;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public static Action OnFirstThresholdPassed;
    public static Action OnSecondThresholdPassed;
    public static Action OnDisplayPoint;

    [SerializeField] private float startZPos;
    [SerializeField] private float firstThreshold;
    [SerializeField] private float secondThreshold;

    [HideInInspector] public static float endZPos;
    private float point = 0;
    public float Point { get => (int)point; private set => point = value; }
    private float distanceTraveled;
    // Point is calculated according to distance traveled
    private void OnEnable()
    {
        UIManager.OnGameOverPanelShown += DisplayPoint;
    }

    private void CalculatePoint()
    {
        distanceTraveled = endZPos - startZPos;
    }

    private void DisplayPoint()
    {
        CalculatePoint();
        point = distanceTraveled * 10f;
        CheckForThreshold();
        OnDisplayPoint?.Invoke();
    }

    private void CheckForThreshold()
    {
        if (point >= firstThreshold)
        {
            Debug.Log($"calling first threshold passed event");
            OnFirstThresholdPassed?.Invoke();
        }
        if (point >= secondThreshold)
        {
            Debug.Log($"calling second threshold passed event");
            OnSecondThresholdPassed?.Invoke();
        }
    }
}