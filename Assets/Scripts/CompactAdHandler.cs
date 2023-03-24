//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using GoogleMobileAds.Api;

//public class CompactAdHandler : MonoBehaviour
//{
//    private static CompactAdHandler instance = null;

//    [Header("IDs")]
//    public string bannerId = "";
//    public string interstitialId = "";
//    public string rewardedId = "";

//    [Header("Test Mode")]
//    public bool testMode = false;
//    public string testDeviceId = "";

//    [Header("Other Settings")]
//    public bool showAdRelatedToKids = false;
//    public AdPosition bannerPosition = AdPosition.Top;

//    private BannerView bannerAd;
//    private InterstitialAd interstitialAd;
//    private RewardedAd rewardedAd;

//    private float interstitialRequestTimeoutSecond;
//    private float rewardedRequestTimeoutSecond;

//    private float bannerAutomaticallyNewRequestSecond = float.PositiveInfinity;
//    private float interstitialAutomaticallyNewRequestSecond = float.PositiveInfinity;
//    private float rewardedAutomaticallyNewRequestSecond = float.PositiveInfinity;

//    private IEnumerator showInterstitialCoroutine;
//    private IEnumerator showRewardedCoroutine;

//    public delegate void RewardedAdReward(Reward reward);
//    private RewardedAdReward rewardDelegate;

//    private void Awake()
//    {
//        if (instance == null)
//        {
//            instance = this;
//            DontDestroyOnLoad(this);

//            bannerId = bannerId.Trim();
//            interstitialId = interstitialId.Trim();
//            rewardedId = rewardedId.Trim();
//            testDeviceId = testDeviceId.Trim();

//            MobileAds.Initialize(adState => { });
//            RequestConfiguration.Builder adConfiguration = new RequestConfiguration.Builder();

//            if (testMode && !string.IsNullOrEmpty(testDeviceId))
//                adConfiguration.SetTestDeviceIds(new List<string>() { testDeviceId });

//            if (showAdRelatedToKids)
//                adConfiguration.SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.True);

//            MobileAds.SetRequestConfiguration(adConfiguration.build());

//            LoadBannerAd();
//            LoadInterstitialAd();
//            LoadRewardedAd();
//        }
//        else if (this != instance)
//            Destroy(this);
//    }

//    private void Update()
//    {
//        float time = Time.realtimeSinceStartup;

//        if (time >= bannerAutomaticallyNewRequestSecond)
//        {
//            bannerAutomaticallyNewRequestSecond = float.PositiveInfinity;
//            LoadBannerAd();
//        }
//        if (time >= interstitialAutomaticallyNewRequestSecond)
//        {
//            interstitialAutomaticallyNewRequestSecond = float.PositiveInfinity;
//            LoadInterstitialAd();
//        }
//        if (time >= rewardedAutomaticallyNewRequestSecond)
//        {
//            rewardedAutomaticallyNewRequestSecond = float.PositiveInfinity;
//            LoadRewardedAd();
//        }
//    }

//    private void LoadBannerAd()
//    {
//        if (!testMode && string.IsNullOrEmpty(bannerId)) return;

//        if (bannerAd != null)
//            bannerAd.Destroy();

//        if (testMode && (string.IsNullOrEmpty(testDeviceId) || string.IsNullOrEmpty(bannerId)))
//        {
//#if UNITY_ANDROID
//            bannerAd = new BannerView("ca-app-pub-3940256099942544/1033173712", AdSize.SmartBanner, bannerPosition);
//#else
//            bannerAd = newBannerView("ca-app-pub-3940256099942544/4411468910", AdSize.SmartBanner, bannerPosition);
//#endif
//        }
//        else
//            bannerAd = new BannerView(bannerId, AdSize.SmartBanner, bannerPosition);

//        bannerAd.OnAdFailedToLoad += BannerCouldNotBeLoaded;
//        bannerAd.LoadAd(CreateAdRequest());
//        bannerAd.Hide();
//    }

//    private void LoadInterstitialAd()
//    {
//        if (!testMode && string.IsNullOrEmpty(interstitialId)) return;

//        if (interstitialAd != null && interstitialAd.IsLoaded()) return;

//        if (interstitialAd != null)
//            interstitialAd.Destroy();

//        if (testMode && (string.IsNullOrEmpty(testDeviceId) || string.IsNullOrEmpty(interstitialId)))
//        {
//#if UNITY_ANDROID
//            interstitialAd = new InterstitialAd("ca-app-pub-3940256099942544/1033173712");
//#else
//            interstitialAd = new InterstitialAd("ca-app-pub-3940256099942544/4411468910");
//#endif
//        }
//        else
//            interstitialAd = new InterstitialAd(interstitialId);

//        interstitialAd.OnAdClosed += InterstitialDelegate;
//        interstitialAd.OnAdFailedToLoad += InterstitialCouldNotBeLoaded;
//        interstitialAd.LoadAd(CreateAdRequest());

//        interstitialRequestTimeoutSecond = Time.realtimeSinceStartup + 10f;
//    }

//    private void LoadRewardedAd()
//    {
//        if (!testMode && string.IsNullOrEmpty(rewardedId)) return;

//        if (rewardedAd != null && rewardedAd.IsLoaded()) return;

//        if (rewardedAd != null)
//            rewardedAd.Destroy();

//        if (testMode && (string.IsNullOrEmpty(testDeviceId) || string.IsNullOrEmpty(rewardedId)))
//        {
//#if UNITY_ANDROID
//            rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/1033173712");
//#else
//            rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/4411468910");
//#endif
//        }
//        else
//            rewardedAd = new RewardedAd(rewardedId);

//        rewardedAd.OnAdClosed += RewardedAdDelegate;
//        rewardedAd.OnAdFailedToLoad += RewardedCouldNotBeLoaded;
//        rewardedAd.OnUserEarnedReward += GiveRewardedAdReward;
//        rewardedAd.LoadAd(CreateAdRequest());

//        rewardedRequestTimeoutSecond = Time.realtimeSinceStartup + 30f;
//    }

//    private AdRequest CreateAdRequest() => new AdRequest.Builder().Build();

//    private void InterstitialDelegate(object sender, EventArgs args) => LoadInterstitialAd();

//    private void RewardedAdDelegate(object sender, EventArgs args) => LoadRewardedAd();

//    private void BannerCouldNotBeLoaded(object sender, AdFailedToLoadEventArgs args)
//    {
//        Debug.Log($"{ args.LoadAdError }");
//        bannerAutomaticallyNewRequestSecond = Time.realtimeSinceStartup + 30f;

//        if (bannerAd != null)
//        {
//            bannerAd.Destroy();
//            bannerAd = null;
//        }
//    }

//    private void InterstitialCouldNotBeLoaded(object sender, AdFailedToLoadEventArgs args)
//    {
//        Debug.Log($"{ args.LoadAdError }");
//        interstitialAutomaticallyNewRequestSecond = Time.realtimeSinceStartup + 30f;

//        if (interstitialAd != null)
//        {
//            interstitialAd.Destroy();
//            interstitialAd = null;
//        }
//    }

//    private void RewardedCouldNotBeLoaded(object sender, AdFailedToLoadEventArgs args)
//    {
//        Debug.Log($"{ args.LoadAdError }");
//        rewardedAutomaticallyNewRequestSecond = Time.realtimeSinceStartup + 30f;

//        if (rewardedAd != null)
//        {
//            rewardedAd.Destroy();
//            rewardedAd = null;
//        }
//    }

//    private void OnGUI()
//    {
//        Color c = GUI.color;

//        if (GUI.Button(new Rect(Screen.width / 2 - 150, 0, 300, 120), "Show Banner"))
//            CompactAdHandler.ShowBanner();

//        if (GUI.Button(new Rect(Screen.width / 2 - 150, 120, 300, 120), "Hide Banner"))
//            CompactAdHandler.HideBanner();

//        GUI.color = IsInterstitialReady() ? Color.green : Color.red;
//        if (GUI.Button(new Rect(Screen.width / 2 - 150, 240, 300, 120), "Show Interstitial"))
//            CompactAdHandler.ShowInterstitial();

//        GUI.color = IsRewardedReady() ? Color.green : Color.red;
//        if (GUI.Button(new Rect(Screen.width / 2 - 150, 360, 300, 120), "Show Rewarded"))
//            CompactAdHandler.ShowRewarded(null);

//        GUI.color = c;
//    }

//    public static void GetBannerAd()
//    {
//        if (instance == null) return;
//        instance.LoadBannerAd();
//    }

//    public static void ShowBanner()
//    {
//        if (instance == null) return;

//        if (instance.bannerAd == null)
//        {
//            instance.LoadBannerAd();
//            if (instance.bannerAd == null) return;
//        }

//        instance.bannerAd.Show();
//    }

//    public static void HideBanner()
//    {
//        if (instance == null) return;

//        if (instance.bannerAd == null) return;

//        instance.bannerAd.Hide();
//    }

//    public static bool IsInterstitialReady()
//    {
//        if (instance == null) return false;

//        if (instance.interstitialAd == null) return false;

//        return instance.interstitialAd.IsLoaded();
//    }

//    public static void GetInterstitialAd()
//    {
//        if (instance == null) return;
//        instance.LoadInterstitialAd();
//    }

//    public static void ShowInterstitial()
//    {
//        if (instance == null) return;

//        if (instance.interstitialAd == null)
//        {
//            instance.LoadInterstitialAd();
//            if (instance.interstitialAd == null) return;
//        }
//        if (instance.ShowInterstitialCoroutine != null)
//        {
//            instance.StopCoroutine(instance.ShowInterstitialCoroutine);
//            instance.ShowInterstitialCoroutine = null;
//        }
//        if (instance.interstitialAd.IsLoaded())
//            instance.interstitialAd.Show();
//        else
//        {
//            if (Time.realtimeSinceStartup >= instance.interstitialRequestTimeoutSecond)
//                instance.LoadInterstitialAd();

//            instance.ShowInterstitialCoroutine = instance.ShowInterstitialCoroutine();
//            instance.StartCoroutine(instance.ShowInterstitialCoroutine);
//        }
//    }

//    public static bool IsRewardedReady()
//    {
//        if (instance == null) return false;

//        if (instance.rewardedAd == null) return false;

//        return instance.rewardedAd.IsLoaded();
//    }

//    public static void GetRewardedAd()
//    {
//        if (instance == null) return;
//        instance.LoadRewardedAd();
//    }

//    public static void ShowRewardedAd(RewardedAdReward rewardMethod)
//    {
//        if (instance == null) return;

//        if (instance.rewardedAd == null)
//        {
//            instance.LoadRewardedAd();
//            if (instance.rewardedAd == null) return;
//        }

//        if (instance.ShowRewardedCoroutine != null)
//        {
//            instance.StopCoroutine(instance.ShowRewardedCoroutine);
//            instance.ShowRewardedCoroutine = null;
//        }

//        instance.rewardDelegate = rewardMethod;

//        if (instance.rewardedAd.IsLoaded())
//            instance.rewardedAd.Show();
//        else
//        {
//            if (Time.realtimeSinceStartup >= instance.rewardedRequestTimeoutSecond)
//                instance.LoadRewardedAd();

//            instance.ShowRewardedCoroutine = instance.ShowRewardedCoroutine();
//            instance.StartCoroutine(instance.ShowRewardedCoroutine);
//        }
//    }

//    private IEnumerator ShowInterstitialCoroutine()
//    {
//        float requestTimeoutMoment = Time.realtimeSinceStartup + 2.5f;
//        while (!interstitialAd.IsLoaded())
//        {
//            if (Time.realtimeSinceStartup > requestTimeoutMoment)
//                yield break;

//            yield return null;

//            if (interstitialAd == null)
//                yield break;
//        }

//        interstitialAd.Show();
//    }

//    private IEnumerator ShowRewardedCoroutine()
//    {
//        float requestTimeoutMoment = Time.realtimeSinceStartup + 10f;
//        while (!rewardedAd.CanShowAd())
//        {
//            if (Time.realtimeSinceStartup >= requestTimeoutMoment)
//                yield break;

//            yield return null;

//            if (rewardedAd == null)
//                yield break;
//        }

//        rewardedAd.Show();
//    }

//    private void GiveRewardedAdReward(object sender, Reward reward)
//    {
//        if (rewardDelegate != null)
//        {
//            rewardDelegate(reward);
//            rewardDelegate = null;
//        }
//    }
//}
