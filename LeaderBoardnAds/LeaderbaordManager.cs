#if UNITY_ANDROID
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
public class LeaderbaordManager : MonoBehaviour {
    public static LeaderbaordManager instance;
    private void Awake()
    {
       if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        SignIn();
	}
	public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate((result) =>
        {
            if(result == true)
            {
                SplashScreenSignIn.instance.Load();
            }
        });
    }
    public void PostScore(int Score)
    {
        PlayGamesPlatform.Instance.ReportScore(Score, GPGSIds.leaderboard_leaderboard, (sucess) =>
        {
            if(sucess == true)
            {
                print("Score Posted Sucessfully");
            }
        });
    }
    public void ShowLeaderbaord()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_leaderboard);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
#endif