using System;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public static Action OnFirstThresholdPassed;
    public static Action OnSecondThresholdPassed;
    public static Action<float> OnDisplayPoint;

    [SerializeField] private float startZPos;
    [SerializeField] private float firstThreshold;
    [SerializeField] private float secondThreshold;

    [HideInInspector] public static float endZPos;
    private float point;
    public float Point { get => point; private set => point = value; }
    private float distanceTraveled;
    // Point is calculated according to distance traveled
    private void OnEnable()
    {
        UIManager.OnGameOverPanelShown += DisplayPoint;
    }

    private void OnDisable()
    {
        UIManager.OnGameOverPanelShown -= DisplayPoint;
    }

    private void CalculatePoint()
    {
        //distanceTraveled = endZPos - startZPos;
        distanceTraveled = FindObjectOfType<InGamePanel>(true).CalculateDistanceTraveled(startZPos, endZPos);
    }

    private void DisplayPoint()
    {
        CalculatePoint();
        point = distanceTraveled;
        Debug.Log($"{ point }");
        CheckForThreshold();
        OnDisplayPoint?.Invoke(point);
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