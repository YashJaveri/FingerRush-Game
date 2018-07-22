using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class AdMobAds : MonoBehaviour {
    public const string GooglePlayAppId = "ca-app-pub-3076192958641421~4213955641";
    public const string BannerID = "ca-app-pub-3076192958641421/9589183558";
    public const string _InterstialID = "ca-app-pub-3076192958641421/6877182314";
    bool isOpen = false;
    BannerView bannerView;
    AdRequest request;
    InterstitialAd interstitialAd;
    public static AdMobAds singleton;
    // Use this for initialization
    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(this);
        }
        else
        {
            singleton = this;
        }
    }

    void Start() {
        DontDestroyOnLoad(gameObject);
        MobileAds.Initialize(GooglePlayAppId);
        RequestInterstial();
    }
    public void RequestBanner()
    {
        bannerView = new BannerView(BannerID, AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }
    public void RequestInterstial()
    {
        interstitialAd = new InterstitialAd(_InterstialID);
        AdRequest request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(request);

    }
    public void ShowBanner()
    {
              RequestBanner();
        if (!isOpen)
        {
            bannerView.Show();
            isOpen = true;
        }
    }
    public void CloseBanner()
    {
        bannerView.Hide();
        isOpen = false;
    }
    public void ShowInterstitial()
    {
        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
    }
}
