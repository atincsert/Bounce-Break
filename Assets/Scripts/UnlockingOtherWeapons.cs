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

    private SaveManager saveManager;

    private void Awake()
    {
        saveManager = FindObjectOfType<SaveManager>(true);
        ballPrefab = FindObjectOfType<BallMovement>(true).gameObject;
        arrowPrefab = FindObjectOfType<ArrowMovement>(true).gameObject;
        hammerPrefab = FindObjectOfType<HammerMover>(true).gameObject;
    }

    private void OnEnable()
    {
        PointSystem.OnFirstThresholdPassed += UnlockArrow;
        PointSystem.OnSecondThresholdPassed += UnlockHammer;
    }

    private void UnlockArrow()
    {
        ballButton.interactable = true;
        arrowButton.interactable = true;
        if (saveManager.UnlockedGameObject != null)
        {
            saveManager.UnlockedGameObject = arrowPrefab;
        }
        saveManager.SelectedGameObject = arrowPrefab;
        OnArrowUnlock?.Invoke();
    }

    private void UnlockHammer()
    {
        ballButton.interactable = true;
        arrowButton.interactable = true;
        hammerButton.interactable = true;
        if (saveManager.UnlockedGameObject != null)
        {
            saveManager.UnlockedGameObject = hammerPrefab;
        }
        saveManager.SelectedGameObject = hammerPrefab;
        OnHammerUnlock?.Invoke();
    }

    public void DisplayBall()
    {
        ballPrefab.SetActive(true);
        arrowPrefab.SetActive(false);
        hammerPrefab.SetActive(false);
        saveManager.SelectedGameObject = ballPrefab;
        OnBallPrefabSelected?.Invoke();
    }

    public void DisplayArrow()
    {
        ballPrefab.SetActive(false);
        arrowPrefab.SetActive(true);
        hammerPrefab.SetActive(false);
        saveManager.SelectedGameObject = arrowPrefab;
        OnArrowPrefabSelected?.Invoke();
        UnlockArrow();
    }

    public void DisplayHammer()
    {
        ballPrefab.SetActive(false);
        arrowPrefab.SetActive(false);
        hammerPrefab.SetActive(true);
        saveManager.SelectedGameObject = hammerPrefab;
        OnHammerPrefabSelected?.Invoke();
        UnlockHammer();
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
