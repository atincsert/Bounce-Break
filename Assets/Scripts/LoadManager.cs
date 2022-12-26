using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    // The key used to save the selected game object in PlayerPrefs
    private const string SELECTED_GAMEOBJECT_KEY = "SelectedGameObject";
    private const string UNLOCKED_GAMEOBJECT_KEY = "UnlockedGameObject";

    [SerializeField] private UnlockingOtherWeapons unlockManager;

    private void Awake() => unlockManager = FindObjectOfType<UnlockingOtherWeapons>();

    // This function is called when the game starts
    void Start()
    {
        if (PlayerPrefs.HasKey(UNLOCKED_GAMEOBJECT_KEY))
        {
            var go = GameObject.Find(PlayerPrefs.GetString(UNLOCKED_GAMEOBJECT_KEY));
        }
        if (PlayerPrefs.HasKey(SELECTED_GAMEOBJECT_KEY))
        {
            string selectedWeaponName = PlayerPrefs.GetString(SELECTED_GAMEOBJECT_KEY);
            unlockManager.DisplayItemByName(selectedWeaponName);
        }
    }
}