//using UnityEngine;
//using UnityEngine.Advertisements;

//public class AddHandler : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener;
//{
//    // This script handles advertisements interstitial, rewarded etc.

//    public string Android_GameID = "ANDROID GAME ID";
//    public string iOS_GameID = "iOS GAME ID";
//    public string GameID { get { return (Application.platform == RuntimePlatform.IPhonePlayer) ? iOS_GameID : Android_GameID; } }

//    public string Android_InterstitialPlacementID = "Interstitial_Android";
//    public string iOS_InterstitialPlacementID = "Interstitial_iOS";
//    public string InterstitialPlacementID { get { return (Application.platform == RuntimePlatform.IPhonePlayer) ? iOS_InterstitialPlacementID : Android_InterstitialPlacementID; } }

//    public bool TestMode = true;

//    private bool isInterstitialAdReady;

//    private void Start()
//    {
//        Advertisement.Initialize(GameID, TestMode, true, this);
//    }

//    public void OnInitializationComplete()
//    {
//        LoadInterstitialAd();
//    }

//    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
//    {
//        Debug.Log($"Could not initialize Unity Ads: { error } - { message }");
//    }

//    private void LoadInterstitialAd()
//    {
//        Advertisement.Load(InterstitialPlacementID, this);
//    }

//    public void OnUnityAdsAdLoaded(string placementID)
//    {
//        if (placementID == InterstitialPlacementID)
//            isInterstitialAdReady = true;
//    }

//    public void OnUnityAdsFailedToLoad(string placementID, UnityAdsLoadError error, string message)
//    {
//        Debug.Log($"Ad could not load: { error } - { message }");

//        if (placementID == InterstitialPlacementID)
//            LoadInterstitialAd();
//    }

//    public void ShowInterstitialAd()
//    {
//        Advertisement.Show(InterstitialPlacementID, this);
//    }

//    public void OnUnityAdsShowStart(string placementID)
//    {
//        if (placementID == InterstitialPlacementID)
//            isInterstitialAdReady = false;

//#if UNITY_EDITOR
//        // Eðer OnUnityAdsShowComplete fonksiyonu editörde çalýþmýyorsa (reklam SDK'inin bir hatasý gibi duruyor), isterseniz bu fonksiyonu burada elle çaðýrabilirsiniz
//        //OnUnityAdsShowComplete( placementId, UnityAdsShowCompletionState.COMPLETED );
//#endif
//    }

//    public void OnUnityAdsShowFailure(string placementID, UnityAdsShowError error, string message)
//    {
//        Debug.Log($"Could not show the add: { placementID } - { error } - { message }");

//        if (placementID == InterstitialPlacementID)
//            LoadInterstitialAd();
//    }

//    public void OnUnityAdsShowClick()
//    {
//    }

//    public void OnUnityAdsShowComplete(string placementID, UnityAdsShowCompletionState showCompletionState)
//    {
//        if (placementID == InterstitialPlacementID)
//            LoadInterstitialAd();
//    }
//}
