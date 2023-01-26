//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GeneralManager : MonoBehaviour
//{
//    public static GeneralManager Instance { get; private set; }

//    public static Action OnBallSelected;
//    public static Action OnArrowSelected;
//    public static Action OnHammerSelected;
//    public static Action OnStartPressed;
//    public static Action OnGameOverPanel;

//    [SerializeField] private NewUIManagerScriptableObject newUIManagerScriptableObject;

//    private NewSaveManager newSaveManager;
//    private NewUnlockManager newUnlockManager;
//    //private NewUIManagerScriptableObject newUIManager;

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//        }
//        else if (Instance != null && Instance != this)
//        {
//            DestroyImmediate(gameObject);
//            return;
//        }

//        newSaveManager = FindObjectOfType<NewSaveManager>(true);
//        DontDestroyOnLoad(newSaveManager);
//        newUnlockManager = FindObjectOfType<NewUnlockManager>(true);
//        DontDestroyOnLoad(newUnlockManager);
//    }

//    public void StartGame()
//    {
//        OnStartPressed?.Invoke();
//    }
//}
