using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class AdsManager : MonoBehaviour {
    public static AdsManager singleton;
    private static string GameID = "1708937";
    private const string placementIDFul = "rewardedVideo";
    private const string placementIDSkip = "video";

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    void Start () {
        Advertisement.Initialize(GameID);
    }
    
   public void ShowFullVideoAd(System.Action<ShowResult> Callback)
    {
        if (Advertisement.IsReady(placementIDFul))
        {
            ShowOptions options = new ShowOptions();
            options.resultCallback = Callback;
            Advertisement.Show(placementIDFul,options);
        }
    }
    public bool IsFullVideoAdAvail()
    {
        return Advertisement.IsReady(placementIDFul);
    }

    public void ShowSkippable(System.Action<ShowResult> Callback)
    {
        if (Advertisement.IsReady(placementIDSkip))
        {
            ShowOptions options1 = new ShowOptions();
            options1.resultCallback = Callback;
            Advertisement.Show(placementIDSkip, options1);
        }
    }

    public bool IsSkippableVideoAdAvail()
    {
        return Advertisement.IsReady(placementIDSkip);
    }
}
