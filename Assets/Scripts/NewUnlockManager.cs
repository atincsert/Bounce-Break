//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NewUnlockManager : MonoBehaviour
//{
//    public static Action OnArrowUnlocked;
//    public static Action OnHammerUnlocked;

//    private void OnEnable()
//    {
//        PointSystem.OnFirstThresholdPassed += UnlockArrow;
//        PointSystem.OnSecondThresholdPassed += UnlockHammer;
//    }

//    private void UnlockArrow()
//    {
//        OnArrowUnlocked?.Invoke();
//    }

//    private void UnlockHammer()
//    {
//        OnHammerUnlocked?.Invoke();
//    }
//}
