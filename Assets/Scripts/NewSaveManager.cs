//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NewSaveManager : MonoBehaviour
//{
//    private const string UNLOCKED_GAMEOBJECT_KEY = "UnlockedGameObject", SELECTED_GAMEOBJECT_KEY = "SelectedGameObject";
//    public string UnlockedGameObjectKey { get => UNLOCKED_GAMEOBJECT_KEY; }
//    public string SelectedGameObjectKey { get => SELECTED_GAMEOBJECT_KEY; }
//    [HideInInspector] public GameObject UnlockedGameObject, SelectedGameObject;

//    private void SaveUnlockedArrow()
//    {
//        PlayerPrefs.SetString(UnlockedGameObjectKey, UnlockedGameObject.name);
//    }

//    private void SaveUnlockedHammer()
//    {
//        PlayerPrefs.SetString(UnlockedGameObjectKey, UnlockedGameObject.name);
//    }

//    private void SaveSelectedBall()
//    {
//        PlayerPrefs.SetString(SELECTED_GAMEOBJECT_KEY, SelectedGameObject.name);
//    }

//    private void SaveSelectedArrow()
//    {
//        PlayerPrefs.SetString(SELECTED_GAMEOBJECT_KEY, SelectedGameObject.name);
//    }

//    private void SaveSelectedHammer()
//    {
//        PlayerPrefs.SetString(SELECTED_GAMEOBJECT_KEY, SelectedGameObject.name);
//    }

//#if UNITY_STANDALONE || UNITY_ANDROID || UNITY_IOS
//    private void OnApplicationQuit()
//    {
//        // Save the selected game object in PlayerPrefs
//        if (UnlockedGameObject != null)
//        {
//            PlayerPrefs.SetString(UnlockedGameObjectKey, UnlockedGameObject.name);
//        }
//        if (SelectedGameObject != null)
//        {
//            PlayerPrefs.SetString(SELECTED_GAMEOBJECT_KEY, SelectedGameObject.name);
//        }
//    }
//#endif
//}
