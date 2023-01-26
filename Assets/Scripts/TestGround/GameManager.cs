using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;

    public static bool IsGameRunning { get; set; }
    public static int GameStartCount { get; set; }

    private void OnEnable()
    {
        UIManager.OnStartButtonPressed += GameStart;
        UIManager.OnPlayerDie += GameStart;
    }

    private void Start()
    {
        if (player != null)
            player.SetPlayer(SaveManager.ChoosenWeapon);
    }

    private void GameStart()
    {
        GameStartCount += 1;
        IsGameRunning = true;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int sceneCountInBuildSettings = SceneManager.sceneCountInBuildSettings;

        int availableSceneIndex = FindAvailableSceneIndex(currentSceneIndex, sceneCountInBuildSettings);

        LoadScene(availableSceneIndex);
    }

    private int FindAvailableSceneIndex(int activeSceneIndex, int sceneCountInBuildSettings) => activeSceneIndex < sceneCountInBuildSettings - 1 ? activeSceneIndex + 1 : 0;

    private void LoadScene(int sceneIndex) => SceneManager.LoadScene(sceneIndex);
}