//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//[CreateAssetMenu]
//public class NewUIManagerScriptableObject : ScriptableObject
//{
//    public Canvas canvas;
//    public Image menuPanel;
//    public Image playerPanel;
//    public Image gameOverPanel;
//    public TextMeshProUGUI pointCount;
//    public Button startButton;
//    public Button playerSelectionButton;
//    public Button ballSelectionButton;
//    public Button arrowSelectionButton;
//    public Button hammerSelectionButton;
//    public Button menuButton;
//    public Button backButton;

//    private void Awake()
//    {
//        if (menuPanel.gameObject.activeInHierarchy == false)
//        {
//            menuPanel.gameObject.SetActive(true);
//        }
//        gameOverPanel.gameObject.SetActive(false);
//        playerPanel.gameObject.SetActive(false);
//    }

//    public void SelectBall()
//    {
//        GameManager.IsGameRunning = false;
//    }
//    public void SelectArrow()
//    {
//        GameManager.IsGameRunning = false;
//    }

//    public void SelectHammer()
//    {
//        GameManager.IsGameRunning = false;
//    }

//    public void GoBack()
//    {
//        GameManager.IsGameRunning = false;
//        menuPanel.gameObject.SetActive(true);
//        gameOverPanel.gameObject.SetActive(false);
//        playerPanel.gameObject.SetActive(false);
//    }

//    public void OpenGameOverPanel()
//    {
//        GameManager.IsGameRunning = false;
//        gameOverPanel.gameObject.SetActive(true);
//        playerPanel.gameObject.SetActive(false);
//        menuPanel.gameObject.SetActive(false);
//    }

//    public void OpenPlayerPanel()
//    {
//        GameManager.IsGameRunning = false;
//        menuPanel.gameObject.SetActive(false);
//        playerPanel.gameObject.SetActive(true);
//        gameOverPanel.gameObject.SetActive(false);
//    }

//    public void OpenMenuPanel()
//    {
//        GameManager.IsGameRunning = false;
//        menuPanel.gameObject.SetActive(true);
//        gameOverPanel.gameObject.SetActive(false);
//        playerPanel.gameObject.SetActive(false);
//    }

//    public void StartGame()
//    {
//        menuPanel.gameObject.SetActive(false);
//        playerPanel.gameObject.SetActive(false);
//        gameOverPanel.gameObject.SetActive(false);
//    }
//}
