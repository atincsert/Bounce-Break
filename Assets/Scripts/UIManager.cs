using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public static Action<Weapon> OnWeaponSelected;
    public static Action OnStartButtonPressed;
    public static Action OnPlayerDie;
    public static Action OnGameOverPanelShown;

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
            DestroyImmediate(canvas.gameObject);
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
        UIManager.OnPlayerDie += OpenGameOverPanel;
    }

    // OnClickEvent
    public void OpenPlayerPanel()
    {
        GameManager.IsGameRunning = false;
        playerPanel.gameObject.SetActive(true);
        menuPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        SetSelectionButtonsInteractability();
    }

    // OnClickEvents
    public void SelectBall()
    {
        GameManager.IsGameRunning = false;
        SaveManager.SetChoosenWeapon(SaveManager.Weapon.Ball);
    }

    public void SelectArrow()
    {
        GameManager.IsGameRunning = false;
        SaveManager.SetChoosenWeapon(SaveManager.Weapon.Arrow);
    }

    public void SelectHammer()
    {
        GameManager.IsGameRunning = false;
        SaveManager.SetChoosenWeapon(SaveManager.Weapon.Hammer);
    }

    // OnClickEvent
    public void GoBack()
    {
        GameManager.IsGameRunning = false;
        menuPanel.gameObject.SetActive(true);
        gameOverPanel.gameObject.SetActive(false);
        playerPanel.gameObject.SetActive(false);
    }

    private void OpenGameOverPanel()
    {
        GameManager.IsGameRunning = false;
        gameOverPanel.gameObject.SetActive(true);
        menuPanel.gameObject.SetActive(false);
        playerPanel.gameObject.SetActive(false);
        OnGameOverPanelShown?.Invoke();
        //OnPlayerDie?.Invoke(); Hakan's code
    }

    // OnClickEvent
    public void OpenMenuPanel()
    {
        GameManager.IsGameRunning = false;
        menuPanel.gameObject.SetActive(true);
        gameOverPanel.gameObject.SetActive(false);
        playerPanel.gameObject.SetActive(false);
        SetSelectionButtonsInteractability();
    }

    // OnClickEvent
    public void StartGame()
    {
        OnStartButtonPressed?.Invoke();
        menuPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        playerPanel.gameObject.SetActive(false);
    }

    private void UpdatePointsUI()
    {
        GameManager.IsGameRunning = false;
        pointCount.text = $"Points : {point.Point}";
    }

    private void SetSelectionButtonsInteractability()
    {
        arrowSelectionButton.interactable = SaveManager.IsArrowUnlocked;
        hammerSelectionButton.interactable = SaveManager.IsHammerUnlocked;
    }

    public enum Weapon
    {
        Ball,
        Arrow,
        Hammer
    }
}