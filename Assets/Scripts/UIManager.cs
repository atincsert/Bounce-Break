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

    #region COMMENT
    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else if (Instance != null && Instance != this)
    //    {
    //        DestroyImmediate(gameObject);
    //        return;
    //    }
    //}

    //private void Start()
    //{
    //    canvas = FindObjectOfType<Canvas>(true);
    //    menuPanel = canvas.transform.Find("MenuPanel").GetComponent<Image>();
    //    playerPanel = canvas.transform.Find("PlayerPanel").GetComponent<Image>();
    //    gameOverPanel = canvas.transform.Find("GameOverPanel").GetComponent<Image>();
    //    pointCount = canvas.transform.Find("PointsText").GetComponent<TextMeshProUGUI>();
    //    startButton = canvas.transform.Find("StartButton").GetComponent<Button>();
    //    playerSelectionButton = canvas.transform.Find("PlayerButton").GetComponent<Button>();
    //    ballSelectionButton = canvas.transform.Find("BallSelectionButton").GetComponent<Button>();
    //    arrowSelectionButton = canvas.transform.Find("ArrowSelectionButton").GetComponent<Button>();
    //    hammerSelectionButton = canvas.transform.Find("HammerSelectionButton").GetComponent<Button>();
    //    menuButton = canvas.transform.Find("MainMenuButton").GetComponent<Button>();
    //    backButton = canvas.transform.Find("BackButton").GetComponent<Button>();
    //    point = FindObjectOfType<PointSystem>();

    //    menuPanel.gameObject.SetActive(true);
    //    gameOverPanel.gameObject.SetActive(false);
    //    playerPanel.gameObject.SetActive(false);
    //}
    #endregion

    private void OnEnable()
    {
        PointSystem.OnDisplayPoint += UpdatePointsUI;
        BallMovement.OnDrown += OpenGameOverPanel;
        ArrowMovement.OnArrowDeath += OpenGameOverPanel;
        HammerMover.OnHammerDeath += OpenGameOverPanel;
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
        //OnWeaponSelected?.Invoke(Weapon.Ball);
    }

    public void SelectArrow()
    {
        GameManager.IsGameRunning = false;
        SaveManager.SetChoosenWeapon(SaveManager.Weapon.Arrow);
        //OnWeaponSelected?.Invoke(Weapon.Arrow);
    }

    public void SelectHammer()
    {
        GameManager.IsGameRunning = false;
        SaveManager.SetChoosenWeapon(SaveManager.Weapon.Hammer);
        //OnWeaponSelected?.Invoke(Weapon.Hammer);
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
        Debug.Log($"GameOver");
        // When you died
        gameOverPanel.gameObject.SetActive(true);
        menuPanel.gameObject.SetActive(false);
        playerPanel.gameObject.SetActive(false);
        OnGameOverPanelShown?.Invoke();
        OnPlayerDie?.Invoke();
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
        // Load the game scene
        OnStartButtonPressed?.Invoke();
        menuPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        playerPanel.gameObject.SetActive(false);
        //canvas.gameObject.SetActive(false);
    }

    private void UpdatePointsUI()
    {
        GameManager.IsGameRunning = false;
        //if (pointCount != null)
        //{
        pointCount.text = $"Points : {point.Point}";
        //}
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