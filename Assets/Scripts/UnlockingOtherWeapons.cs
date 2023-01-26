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
    private bool arrowButtonUnlocked = false;

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

    public void SetWeaponButtonsInteractable()
    {
        arrowButton.interactable = SaveManager.IsArrowUnlocked;
        hammerButton.interactable = SaveManager.IsHammerUnlocked;
    }

    private void UnlockArrow()
    {
        if (arrowButtonUnlocked) return;
        arrowButtonUnlocked = true;
        Debug.Log($"Arrow interactible : {arrowButton.interactable}");

        ballButton.interactable = true;
        arrowButton.interactable = true;
        //if (saveManager.UnlockedGameObject == null)
        //{
        //    saveManager.UnlockedGameObject = arrowPrefab;
        //    Debug.Log($"Unlocked Arrow save manager");
        //}
        OnArrowUnlock?.Invoke();
        //if(saveManager.SelectedGameObject != null && saveManager.SelectedGameObject != ballPrefab && saveManager.SelectedGameObject != hammerPrefab)
        //    saveManager.SelectedGameObject = arrowPrefab;
    }

    private void UnlockHammer()
    {
        Debug.Log($"Unlock Hammer");
        ballButton.interactable = true;
        arrowButton.interactable = true;
        hammerButton.interactable = true;
        //if (saveManager.UnlockedGameObject != null)
        //{
        //    saveManager.UnlockedGameObject = hammerPrefab;
        //    Debug.Log($"Unlocked Hammer save manager");
        //}
        //if (saveManager.SelectedGameObject != null && saveManager.SelectedGameObject != ballPrefab && saveManager.SelectedGameObject != arrowPrefab)
        //    saveManager.SelectedGameObject = hammerPrefab;

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