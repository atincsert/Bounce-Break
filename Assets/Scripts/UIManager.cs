using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public static Action OnBallSelected;
    public static Action OnArrowSelected;
    public static Action OnHammerSelected;
    public static Action OnStartPressed;

    [SerializeField] private Canvas canvas;
    [SerializeField] private Image menuPanel;
    [SerializeField] private Image playerPanel;
    [SerializeField] private Image gameOverPanel;
    [SerializeField] private TextMeshProUGUI pointCount;
    [SerializeField] private Button startButton;
    [SerializeField] private Button playerSelectionButton;
    [SerializeField] private Button ballSelectionButton;
    [SerializeField] private Button arrowSelectionButton;
    [SerializeField] private Button hammerSelectionButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button backButton;

    private PointSystem point;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(canvas.gameObject);
        }
        else if (Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
            Destroy(canvas.gameObject);
            return;
        }

        if (menuPanel.gameObject.activeInHierarchy == false)
        {
            menuPanel.gameObject.SetActive(true);
        }
        gameOverPanel.gameObject.SetActive(false);
        playerPanel.gameObject.SetActive(false);

        point = FindObjectOfType<PointSystem>(true);
    }

    private void OnEnable()
    {
        PointSystem.OnDisplayPoint += UpdatePointsUI;
        BallMovement.OnWaterTouch += OpenGameOverPanel;
        ArrowMovement.OnArrowDeath += OpenGameOverPanel;
        HammerMover.OnHammerDeath += OpenGameOverPanel;
    }

    // OnClickEvent
    public void OpenPlayerPanel()
    {
        Debug.Log($"PlayerPanel");
        playerPanel.gameObject.SetActive(true);
        menuPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
    }

    // OnClickEvents
    public void SelectBall()
    {
        OnBallSelected?.Invoke();
        Debug.Log($"Ball is selected");
    }

    public void SelectArrow() => OnArrowSelected?.Invoke();

    public void SelectHammer() => OnHammerSelected?.Invoke();

    // OnClickEvent
    public void GoBack()
    {
        Debug.Log($"Back");
        menuPanel.gameObject.SetActive(true);
        gameOverPanel.gameObject.SetActive(false);
        playerPanel.gameObject.SetActive(false);
    }

    private void OpenGameOverPanel()
    {
        Debug.Log($"GameOver");
        // When you died
        gameOverPanel.gameObject.SetActive(true);
        menuPanel.gameObject.SetActive(false);
        playerPanel.gameObject.SetActive(false);
    }

    // OnClickEvent
    public void OpenMenuPanel()
    {
        Debug.Log($"MenuPanel");
        menuPanel.gameObject.SetActive(true);
        gameOverPanel.gameObject.SetActive(false);
        playerPanel.gameObject.SetActive(false);
    }

    // OnClickEvent
    public void StartGame()
    {
        // Load the game scene
        OnStartPressed?.Invoke();
        Debug.Log($"GameStart");
        menuPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        playerPanel.gameObject.SetActive(false);
        //canvas.gameObject.SetActive(false);
    }

    private void UpdatePointsUI()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        if (pointCount != null)
        {
            pointCount.text = $"Points : {point.Point}";
        }
    }
}
