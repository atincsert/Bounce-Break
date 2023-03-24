using GoogleMobileAds.Api;
using UnityEngine;

public class InterstitialAdManager : MonoBehaviour
{
    private InterstitialAd interstitialAd;
    private string adUnitId;

    private void Start()
    {
        // Set the ad unit ID
        this.adUnitId = "ca-app-pub-3940256099942544/1033173712";

        // Initialize the InterstitialAd object
        this.interstitialAd = new InterstitialAd(adUnitId);

        // Load the interstitial ad
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitialAd.LoadAd(request);

        // Register for ad events
        this.interstitialAd.OnAdClosed += HandleOnAdClosed;
    }

    public void ShowInterstitialAd()
    {
        if (this.interstitialAd.IsLoaded())
        {
            this.interstitialAd.Show();
        }
        else
        {
            Debug.Log("Interstitial ad is not ready yet.");
        }
    }

    private void HandleOnAdClosed(object sender, System.EventArgs args)
    {
        // Unregister the event handler
        this.interstitialAd.OnAdClosed -= HandleOnAdClosed;

        // Create a new InterstitialAd object
        this.interstitialAd = new InterstitialAd(adUnitId);

        // Load the interstitial ad
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitialAd.LoadAd(request);

        // Register for ad events
        this.interstitialAd.OnAdClosed += HandleOnAdClosed;
    }
}
