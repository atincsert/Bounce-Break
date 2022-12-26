using UnityEngine;
public class SaveManager : MonoBehaviour
{
    // The key used to save the selected game object in PlayerPrefs
    private const string SELECTED_GAMEOBJECT_KEY = "SelectedGameObject";
    private const string UNLOCKED_GAMEOBJECT_KEY = "UnlockedGameObject";

    // The selected game object
    [HideInInspector] public GameObject SelectedGameObject;
    [HideInInspector] public GameObject UnlockedGameObject;

    private void OnEnable()
    {
        UnlockingOtherWeapons.OnArrowUnlock += UnlockArrow;
        UnlockingOtherWeapons.OnHammerUnlock += UnlockHammer;
        UnlockingOtherWeapons.OnBallPrefabSelected += BallSelected;
        UnlockingOtherWeapons.OnArrowPrefabSelected += ArrowSelected;
        UnlockingOtherWeapons.OnHammerPrefabSelected += HammerSelected;
    }

    private void OnDisable()
    {
        UnlockingOtherWeapons.OnArrowUnlock -= UnlockArrow;
        UnlockingOtherWeapons.OnHammerUnlock -= UnlockHammer;
        UnlockingOtherWeapons.OnBallPrefabSelected -= BallSelected;
        UnlockingOtherWeapons.OnArrowPrefabSelected -= ArrowSelected;
        UnlockingOtherWeapons.OnHammerPrefabSelected -= HammerSelected;
    }

    private void HammerSelected()
    {
        PlayerPrefs.SetString(SELECTED_GAMEOBJECT_KEY, SelectedGameObject.name);
    }

    private void ArrowSelected()
    {
        PlayerPrefs.SetString(SELECTED_GAMEOBJECT_KEY, SelectedGameObject.name);
    }

    private void BallSelected()
    {
        PlayerPrefs.SetString(SELECTED_GAMEOBJECT_KEY, SelectedGameObject.name);
    }

    private void UnlockHammer()
    {
        if (UnlockedGameObject == null) return;
        PlayerPrefs.SetString(UNLOCKED_GAMEOBJECT_KEY, UnlockedGameObject.name);
    }

    private void UnlockArrow()
    {
        if (UnlockedGameObject == null) return;
        PlayerPrefs.SetString(UNLOCKED_GAMEOBJECT_KEY, UnlockedGameObject.name);
    }

    // This function is called when the game is closed


#if UNITY_STANDALONE || UNITY_ANDROID || UNITY_IOS
    private void OnApplicationQuit()
    {
        // Save the selected game object in PlayerPrefs
        if (UnlockedGameObject != null)
        {
            PlayerPrefs.SetString(UNLOCKED_GAMEOBJECT_KEY, UnlockedGameObject.name);
        }
        if (SelectedGameObject != null)
        {
            PlayerPrefs.SetString(SELECTED_GAMEOBJECT_KEY, SelectedGameObject.name);
        }
    }
#endif
}