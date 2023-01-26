//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NewLoadManager : MonoBehaviour
//{
//    private NewSaveManager saveManager;

//    private void Awake()
//    {
//        saveManager = GetComponent<NewSaveManager>();
//    }

//    private void Start()
//    {
//        if (PlayerPrefs.HasKey(saveManager.UnlockedGameObjectKey))
//        {
//            var unlockedGameObject = GameObject.Find(PlayerPrefs.GetString(saveManager.UnlockedGameObjectKey));
//        }
//        if (PlayerPrefs.HasKey(saveManager.SelectedGameObjectKey))
//        {
//            string selectedWeaponName = PlayerPrefs.GetString(saveManager.SelectedGameObjectKey);
//            //NewUnlockManager.DisplayItemByName(selectedWeaponName);
//        }
//    }
//}
