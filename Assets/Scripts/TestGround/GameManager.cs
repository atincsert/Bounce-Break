using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    private int gameStartCount;
    private bool isGameRunning;

    //public bool IsGameRunning { get => isGameRunning; set => isGameRunning = value; }
    public static bool IsGameRunning { get; set; }
    //public int GameStartCount { get => gameStartCount; set => gameStartCount = value; }
    public static int GameStartCount { get; set; }

    //private UIManager UIManager;

    private void OnEnable()
    {
        UIManager.OnStartButtonPressed += GameStart;
        UIManager.OnPlayerDie += GameStart;
        //UIManager.OnPlayerDie += GameOver;
    }

    private void OnDisable()
    {
        UIManager.OnStartButtonPressed -= GameStart;
        UIManager.OnPlayerDie -= GameStart;
        //UIManager.OnPlayerDie -= GameOver;
    }

    private void Start()
    {
        if (player != null)
            player.SetPlayer(SaveManager.ChoosenWeapon);
    }

    private void GameStart()
    {
        IsGameRunning = true;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int sceneCountInBuildSettings = SceneManager.sceneCountInBuildSettings;

        int availableSceneIndex = FindAvailableSceneIndex(currentSceneIndex, sceneCountInBuildSettings);

        LoadScene(availableSceneIndex);
    }

    //private void GameOver()
    //{
    //    IsGameRunning = false;
    //    //UIManager.GetComponent<UIManager>().OpenGameOverPanel();
    //    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    //    int sceneCountInBuildSettings = SceneManager.sceneCountInBuildSettings;

    //    int availableSceneIndex = FindAvailableSceneIndex(currentSceneIndex, sceneCountInBuildSettings);

    //    LoadScene(availableSceneIndex);
    //}

    private int FindAvailableSceneIndex(int activeSceneIndex, int sceneCountInBuildSettings) => activeSceneIndex < sceneCountInBuildSettings - 1 ? activeSceneIndex + 1 : 0;

    private void LoadScene(int sceneIndex) => SceneManager.LoadScene(sceneIndex);
}