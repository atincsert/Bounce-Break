using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using System;

public class AddHandler : MonoBehaviour
{
#if UNITY_ANDROID
    private string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
    private string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        private string adUnitId = "unused";
#endif

    private InterstitialAd interstitialAd;

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
        this.RequestInterstitial();
    }

    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd object.
        this.interstitialAd = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitialAd.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitialAd.OnAdClosed += HandleOnAdClosed;

        // Create an AdRequest.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the interstitial with the request.
        //this.interstitialAd.Load(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Debug.Log($"Interstitial ad loaded");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log($"Interstitial ad failed to load : { args }");
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        Debug.Log($"Interstitial ad opened");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Debug.Log($"Interstitial ad closed");
        this.interstitialAd.Destroy();
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        this.interstitialAd = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitialAd.LoadAd(request);
    }

    //public void LoadInterstitialAd()
    //{
    //    if (interstitialAd != null)
    //    {
    //        return;
    //        //interstitialAd.Destroy();
    //        //interstitialAd = null;
    //    }

    //    Debug.Log($"Loading the interstitial ad");

    //    var adRequest = new AdRequest.Builder().AddKeyword("unity-admob-sample").Build();

    //    InterstitialAd.Load(adUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
    //    {
    //        if (error != null || ad == null)
    //        {
    //            Debug.LogError($"interstitial ad failed to load and ad with error {error}");
    //            return;
    //        }

    //        Debug.Log($"interstitial ad loaded with response {ad.GetResponseInfo()}");
    //        interstitialAd = ad;
    //    });
    //}

    //public void ShowInterstitialAd()
    //{
    //    if (interstitialAd == null)
    //    {
    //        return;
    //    }
    //    if (/*interstitialAd != null && */interstitialAd.CanShowAd())
    //    {
    //        Debug.Log($"Show interstitial ad");
    //        //StartCoroutine(ShowInterstitialCoroutine());
    //        //GameManager.IsGameRunning = false;
    //        interstitialAd.Show();
    //    }
    //    else
    //    {
    //        Debug.LogError($"Interstitial ad is not ready yet");
    //    }
    //    //GameManager.IsGameRunning = true;
    //}

    ////private IEnumerator ShowInterstitialCoroutine()
    ////{
    ////    if (interstitialAd != null && interstitialAd.CanShowAd())
    ////    {
    ////        GameManager.IsGameRunning = false;
    ////        Debug.Log($"Show interstitial ad");
    ////        //interstitialAd.Show();
    ////    }
    ////    else
    ////    {
    ////        Debug.LogError($"Interstitial ad is not ready yet");
    ////    }

    ////    yield return new WaitForSecondsRealtime(5f);
    ////    GameManager.IsGameRunning = true;
    ////}

    //private void RegisterEventHandlers(InterstitialAd ad)
    //{
    //    // Raised when the ad is estimated to have earned money.
    //    ad.OnAdPaid += (AdValue adValue) =>
    //    {
    //        Debug.Log(string.Format("Interstitial ad paid {0} {1}.",
    //            adValue.Value,
    //            adValue.CurrencyCode));
    //    };
    //    // Raised when an impression is recorded for an ad.
    //    ad.OnAdImpressionRecorded += () =>
    //    {
    //        Debug.Log("Interstitial ad recorded an impression.");
    //    };
    //    // Raised when a click is recorded for an ad.
    //    ad.OnAdClicked += () =>
    //    {
    //        Debug.Log("Interstitial ad was clicked.");
    //    };
    //    // Raised when an ad opened full screen content.
    //    ad.OnAdFullScreenContentOpened += () =>
    //    {
    //        Debug.Log("Interstitial ad full screen content opened.");
    //    };
    //    // Raised when the ad closed full screen content.
    //    ad.OnAdFullScreenContentClosed += () =>
    //    {
    //        Debug.Log("Interstitial ad full screen content closed.");
    //        LoadInterstitialAd();
    //    };
    //    // Raised when the ad failed to open full screen content.
    //    ad.OnAdFullScreenContentFailed += (AdError error) =>
    //    {
    //        Debug.LogError("Interstitial ad failed to open full screen content " +
    //                       "with error : " + error);
    //        LoadInterstitialAd();
    //    };
    //}

    //private void RegisterReloadHandler(InterstitialAd ad)
    //{
    //    // Raised when the ad closed full screen content.
    //    ad.OnAdFullScreenContentClosed += () =>
    //{
    //    Debug.Log("Interstitial Ad full screen content closed.");

    //    // Reload the ad so that we can show another as soon as possible.
    //    LoadInterstitialAd();
    //};
    //    // Raised when the ad failed to open full screen content.
    //    ad.OnAdFullScreenContentFailed += (AdError error) =>
    //    {
    //        Debug.LogError("Interstitial ad failed to open full screen content " +
    //                       "with error : " + error);

    //        // Reload the ad so that we can show another as soon as possible.
    //        LoadInterstitialAd();
    //    };
    //}
}
