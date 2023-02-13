using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI distanceTraveledText;
    [SerializeField] private Image inGamePanel;
    [SerializeField] private Image pauseButton;
    [SerializeField] private GameObject[] players;

    private float startPoint;
    private float distanceTraveled;
    private GameObject currentPlayer;
    private PointSystem pointSystem;

    private void Awake()
    {
        pointSystem = FindObjectOfType<PointSystem>(true);
    }

    private void Start()
    {
        startPoint = 0;
        foreach (GameObject possiblePlayer in players)
        {
            if (possiblePlayer.activeInHierarchy)
            {
                currentPlayer = possiblePlayer;
            }
        }
    }

    private void Update()
    {
        CalculateDistanceTraveled(startPoint, currentPlayer.transform.position.z);
        ShowDistanceUI();
    }

    public float CalculateDistanceTraveled(float startPoint, float endPoint)
    {
        return distanceTraveled = Vector3.Distance(new Vector3(0, 0, startPoint), new Vector3(0, 0, endPoint));
    }

    private void ShowDistanceUI()
    {
        distanceTraveledText.text = $"Distance: {(int)distanceTraveled}m";
    }
}
