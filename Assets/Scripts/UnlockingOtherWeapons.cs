using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockingOtherWeapons : MonoBehaviour
{
    public static Action OnArrowUnlock;
    public static Action OnHammerUnlock;
    public static Action OnBallPrefabSelected;
    public static Action OnArrowPrefabSelected;
    public static Action OnHammerPrefabSelected;

    [SerializeField] private Button ballButton;
    [SerializeField] private Button arrowButton;
    [SerializeField] private Button hammerButton;

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject hammerPrefab;

    private void OnEnable()
    {
        PointSystem.OnFirstThresholdPassed += UnlockArrow;
        PointSystem.OnSecondThresholdPassed += UnlockHammer;
    }

    private void UnlockArrow()
    {
        OnArrowUnlock?.Invoke();
        ballButton.interactable = true;
        arrowButton.interactable = true;       
    }

    private void UnlockHammer()
    {
        OnHammerUnlock?.Invoke();
        ballButton.interactable = true;
        arrowButton.interactable = true;
        hammerButton.interactable = true;
    }

    public void DisplayBall()
    {
        OnBallPrefabSelected?.Invoke();
        ballPrefab.SetActive(true);
        arrowPrefab.SetActive(false);
        hammerPrefab.SetActive(false);
    }

    public void DisplayArrow()
    {
        OnArrowPrefabSelected?.Invoke();
        ballPrefab.SetActive(false);
        arrowPrefab.SetActive(true);
        hammerPrefab.SetActive(false);
    }

    public void DisplayHammer()
    {
        OnHammerPrefabSelected?.Invoke();
        ballPrefab.SetActive(false);
        arrowPrefab.SetActive(false);
        hammerPrefab.SetActive(true);
    }
}
