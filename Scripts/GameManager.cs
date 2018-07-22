using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Pannels
    public GameObject gameOverPanel;
    public GameObject fadingRefObject;
    public GameObject reviveScreen;
    public GameObject scene;
    #endregion
    #region Singletons/Lists(files)
    static bool thisIsCreated = false;
    public static GameManager singleton;
    public List<int> savedPlayerIndices;
    public List<int> savedCompletedLevelIDs;
    #endregion
    #region Audio
    private AudioSource audioSource;
    public AudioClip buySoundClip;
    #endregion
    #region Timer/Texts(Score,timer etc)
    private float countdown = 3.5f;
    public Text countdownText;
    public Text scoreText;
    public Text highScore;
    #endregion
    int numberOfGems;
    // public int multiplyer = 1;
    public bool gmMute = false;

 
    private void Awake()
    {
        if (singleton != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            singleton = this;
        }

    }
    void Start()
    {
        audioSource = GameObject.Find("EffectsAudioSource").GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        savedPlayerIndices = Saver.load("GameData");
        //For Setting 1st playerobj as default.
        if (savedPlayerIndices == null)
        {
            savedPlayerIndices = new List<int>();
            savedPlayerIndices.Add(0);
            Saver.Save(savedPlayerIndices, "GameData");
        }
    }

    private void Update()
    {
        //For reseting data before built:
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerPrefs.SetInt("NumberOfGems", 2900);
            PlayerPrefs.SetInt("HighScore", 120);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Saver.Delete("GameData");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Saver.Delete("Phase 1");
            Saver.Delete("Phase 2");
        }

        //For countdown:
        if (countdown > 0 && reviveScreen.activeInHierarchy == true)
        {
            countdown -= Time.deltaTime;
            countdownText.text = countdown.ToString("f0");
        }
    }

    #region Manage UI/Buttons :
    public void DisplayGameOverScreen()
    {
        PlayerPrefs.SetInt("RevivedScore", 0);
        gameOverPanel.SetActive(true);
        if(PlayerMovement.singleton.pmScore >= 30 && PlayerPrefs.GetInt("GamesPlayed_with_30+",0) <= 10){
            int gp = PlayerPrefs.GetInt("GamesPlayed_with_30+",0);
            PlayerPrefs.SetInt("GamesPlayed_with_30+",gp + 1);
            gp=0;
        }
         if(PlayerMovement.singleton.pmScore >= 0 && PlayerPrefs.GetInt("GamesPlayed",0) <= 35){
            int gp = PlayerPrefs.GetInt("GamesPlayed",0);
            PlayerPrefs.SetInt("GamesPlayed",gp + 1);
            gp=0;
        }
        Invoke("DisableRevScrn", 0.3f);
        //Show Interstial Ad:
        if (Random.Range(0, 100) < 75) 
            Invoke("DisplayAdInter", 0.3f);
    }
    private void DisplayAdInter()
    {
        AdMobAds.singleton.ShowInterstitial();
    }

    public void DisplayRevivePrompt()
    {
        reviveScreen.gameObject.SetActive(true);
        countdown = 3.1f;
    }
    public void Revive()
    {
        CancelInvoke("DisplayGameOverScreen");
        Time.timeScale = 0;
        AdsManager.singleton.ShowFullVideoAd((result) =>
              {
                  if (result == UnityEngine.Advertisements.ShowResult.Finished)
                {
                    Time.timeScale = 1;
                    PlayerPrefs.SetInt("Revived", 1);
                    GOPFalse();
                    scene.gameObject.SetActive(false);
                    Invoke("DisableRevScrn", 0.4f);
                    PlayerPrefs.SetInt("RevivedScore", PlayerMovement.singleton.pmScore);
                    RestartCurrentLevel(1);
                }
              });
     }
   
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("RevivedScore", 0);
        PlayerPrefs.SetInt("Revive", 0);
    }
    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetInt("RevivedScore", 0);
        PlayerPrefs.SetInt("Revive", 0);
    }

    public void LoadMainMenuScene()
    {
        PlayerPrefs.SetInt("Revived", 0);
        AdMobAds.singleton.CloseBanner();
        //For Fading:
        Fade.singleton.alpha = 0;
        Fade.singleton.BeginFade(1);
        Invoke("LoadIt", 0.36f);
    }
    public void RestartCurrentLevel(int x)
    {
        PlayerPrefs.SetInt("Revived", x);
        Fade.singleton.alpha = 0;
        Fade.singleton.BeginFade(1);
        Invoke("Restart", 0.3f);
    }
    public void PlayVideo(int gift)
    {
        //logic.
        numberOfGems = PlayerPrefs.GetInt("NumberOfGems", 10) + Random.Range(gift, gift + 5);
        PlayerPrefs.SetInt("NumberOfGems", numberOfGems);
    }

    #region ForFadePurpose(ExtraSub-Methods)
    void LoadIt()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        Invoke("GOPFalse", 0.36f);
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Invoke("GOPFalse", 0.02f);
    }

    void GOPFalse()
    {
        gameOverPanel.SetActive(false);
    }
    void DisableRevScrn()
    {
        reviveScreen.SetActive(false);
    }
    #endregion
    #endregion

    #region ManageSound:
    public void PlayBuySound()
    {
        audioSource.PlayOneShot(buySoundClip);
    }
    #endregion

    #region Manage Files-Data:
    public void LoadApproptFile(string fileName)
    {
        savedCompletedLevelIDs = Saver.load(fileName);
        if (savedCompletedLevelIDs == null)
        {
            savedCompletedLevelIDs = new List<int>();
            Saver.Save(savedCompletedLevelIDs, fileName);
        }

    }
    #endregion
}

/* #region  PowerUps Management:
public void Jackpot(int value)
{
    multiplyer = 2;
    Invoke("ResetMultiplyer", 4);
}
public void Slowdown()
{
    Time.timeScale = 0.45f;
}

public void Shrink()
{
    int playerIndex = PlayerPrefs.GetInt("PlayerIndex", 0);
    ogSize = FindObjectOfType<SettingPlayer>().gameObject.transform.GetChild(playerIndex).GetComponent<Transform>().localScale;
    FindObjectOfType<SettingPlayer>().gameObject.transform.GetChild(playerIndex).GetComponent<Transform>().localScale = ogSize / 1.43f;
    Invoke("Resize", 6);
}
void Resize()
{
    int playerIndex = PlayerPrefs.GetInt("PlayerIndex", 0);
    FindObjectOfType<SettingPlayer>().gameObject.transform.GetChild(playerIndex).GetComponent<Transform>().localScale = ogSize;
}
void ResetMultiplyer()
{
    multiplyer = 1;
}
#endregion
}


GameObject[] objs = GameObject.FindGameObjectsWithTag("SpeedCmpAtc");
// tempSaver1 = RandomSpawner.singleton.Speed;

foreach (GameObject gb in objs)
{
 if (gb.GetComponent<RandomSpawner>() != null)
 {
     gb.GetComponent<RandomSpawner>().Speed = slowDownTo;
 }
 if (gb.GetComponent<EnemyMovements>() != null)
 {
     tempSaver1 = gb.GetComponent<EnemyMovements>().enemySpeed;
     gb.GetComponent<EnemyMovements>().enemySpeed = slowDownTo;
 }
 if (gb.GetComponent<BackgroundMovement>() != null)
 {
     tempSpeedSaver = gb.GetComponent<BackgroundMovement>().speed;
     gb.GetComponent<BackgroundMovement>().speed = slowDownTo;
 }

 Invoke("ResetSpeed", 7);
}
}
private void ResetSpeed() {
GameObject[] objs = GameObject.FindGameObjectsWithTag("SpeedCmpAtc");
foreach (GameObject gb in objs)
{
 if (gb.GetComponent<RandomSpawner>() != null)
 {
     gb.GetComponent<RandomSpawner>().Speed = tempSpeedSaver;
 }
 if (gb.GetComponent<EnemyMovements>() != null)
 {
     gb.GetComponent<EnemyMovements>().enemySpeed = tempSaver1;
 }
 if (gb.GetComponent<BackgroundMovement>() != null)
 {
     gb.GetComponent<BackgroundMovement>().speed = tempSpeedSaver;
 }
}
}*/
