using System;
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

    private bool arrowButtonUnlocked = false;

    private void Awake()
    {
        ballPrefab = FindObjectOfType<BallMovement>(true).gameObject;
        arrowPrefab = FindObjectOfType<ArrowMovement>(true).gameObject;
        hammerPrefab = FindObjectOfType<HammerMovement>(true).gameObject;
    }

    private void OnEnable()
    {
        PointSystem.OnFirstThresholdPassed += UnlockArrow;
        PointSystem.OnSecondThresholdPassed += UnlockHammer;
    }

    private void OnDisable()
    {
        PointSystem.OnFirstThresholdPassed -= UnlockArrow;
        PointSystem.OnSecondThresholdPassed -= UnlockHammer;
    }

    public void SetWeaponButtonsInteractable()
    {
        arrowButton.interactable = SaveManager.IsArrowUnlocked;
        hammerButton.interactable = SaveManager.IsHammerUnlocked;
    }

    private void UnlockArrow()
    {
        if (arrowButtonUnlocked) return;
        arrowButtonUnlocked = true;
        ballButton.interactable = true;
        arrowButton.interactable = true;
        OnArrowUnlock?.Invoke();
    }

    private void UnlockHammer()
    {
        ballButton.interactable = true;
        arrowButton.interactable = true;
        hammerButton.interactable = true;
        OnHammerUnlock?.Invoke();
    }

    private void DisplayBall()
    {
        ballPrefab.SetActive(true);
        arrowPrefab.SetActive(false);
        hammerPrefab.SetActive(false);
        OnBallPrefabSelected?.Invoke();
    }

    private void DisplayArrow()
    {
        ballPrefab.SetActive(false);
        arrowPrefab.SetActive(true);
        hammerPrefab.SetActive(false);
        OnArrowPrefabSelected?.Invoke();
    }

    private void DisplayHammer()
    {
        ballPrefab.SetActive(false);
        arrowPrefab.SetActive(false);
        hammerPrefab.SetActive(true);
        OnHammerPrefabSelected?.Invoke();
    }

    public void DisplayItemByName(string gameObjectname)
    {
        if (gameObjectname == hammerPrefab.name)
        {
            DisplayHammer();
        }
        else if (gameObjectname == arrowPrefab.name)
        {
            DisplayArrow();
        }
        else if (gameObjectname == ballPrefab.name)
        {
            DisplayBall();
        }
    }
}