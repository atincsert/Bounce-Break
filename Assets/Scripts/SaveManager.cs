using UnityEngine;
public class SaveManager : MonoBehaviour
{
    // The key used to save the selected game object in PlayerPrefs
    private const string ARROW_UNLOCKED = "ArrowUnlockedPrefKey";
    private const string HAMMER_UNLOCKED = "HammerUnlockedPrefKey";
    private const string CHOOSEN_WEAPON = "ChoosenWeaponPrefKey";

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

    private void BallSelected() => PlayerPrefs.SetInt(CHOOSEN_WEAPON, 1);
    
    private void ArrowSelected() => PlayerPrefs.SetInt(CHOOSEN_WEAPON, 2);
    
    private void HammerSelected() => PlayerPrefs.SetInt(CHOOSEN_WEAPON, 3);


    private void UnlockArrow()
    {
        PlayerPrefs.SetInt(ARROW_UNLOCKED, 1);
    }

    private void UnlockHammer()
    {
        PlayerPrefs.SetInt(HAMMER_UNLOCKED, 1);
    }

    public static bool IsArrowUnlocked => PlayerPrefs.GetInt(ARROW_UNLOCKED, 0) == 1 ? true : false;

    public static bool IsHammerUnlocked => PlayerPrefs.GetInt(HAMMER_UNLOCKED, 0) == 1 ? true : false;

    public static int ChoosenWeapon => PlayerPrefs.GetInt(CHOOSEN_WEAPON, 0);

    public static Weapon ChoosenWeaponAsEnum => (Weapon)PlayerPrefs.GetInt(CHOOSEN_WEAPON, 0);

    public static void SetChoosenWeapon(Weapon weapon) => PlayerPrefs.SetInt(CHOOSEN_WEAPON, (int)weapon);

    public enum Weapon
    {
        None = 0,
        Ball = 1,
        Arrow = 2,
        Hammer = 3
    }

#if UNITY_STANDALONE || UNITY_ANDROID || UNITY_IOS
    // Crash ile kapanirsa burasi calismaz
    private void OnApplicationQuit()
    {
        // Save the selected game object in PlayerPrefs
        //if (UnlockedGameObject != null)
        //{
        //    PlayerPrefs.SetString(UNLOCKED_GAMEOBJECT_KEY, UnlockedGameObject.name);
        //}
        //if (SelectedGameObject != null)
        //{
        //    PlayerPrefs.SetString(SELECTED_GAMEOBJECT_KEY, SelectedGameObject.name);
        //}
    }
#endif
}