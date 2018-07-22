using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    //public GameObject phasePannelParent;  <-- ForLevelMode.
    public GameObject mainMenu;
    public GameObject mainMenuPannel;
    public GameObject aboutsPannel;

    public Text gemsText;
    public Text giftText;
    public Text ad_Unavail_Mes;
    public static MenuManager singleton;

    public Text highScore;
    private string levelname; //<-- For Ref.

    private AudioSource audSor;
    public AudioClip click;

    private void Start()
    {
        singleton = this;
        //Managing score:
        highScore.text = "YOUR BEST:\n " + PlayerPrefs.GetInt("HighScore", 0).ToString();

        audSor = GameObject.Find("EffectsAudioSource").GetComponent<AudioSource>();
    }
    void Update()
    {
        int numberOfGems = PlayerPrefs.GetInt("NumberOfGems", 10);
        gemsText.text = "X " + numberOfGems.ToString();
        if(Input.GetKey(KeyCode.Escape) && mainMenuPannel.activeSelf && !aboutsPannel.activeSelf)
         Application.Quit();
    }
    #region Loading scenes and fading:
    public void LoadALevel(string levelName)
    {
        Fade.singleton.alpha = 0;
        Fade.singleton.BeginFade(1);
        PlayerPrefs.SetInt("Revived", 0);
        Invoke("LoadIt", 0.35f);
        levelname = levelName;
    }
    void LoadIt()
    {
        SceneManager.LoadSceneAsync(levelname);
    }
    #endregion
    #region ForFuture
    /*
     For LevelMode:
     public void LevelsScreenPanelEnable()
     {
         phasePannelParent.SetActive(true);
     }
     public void LevelsScreenPanelDisable()
     {
         phasePannelParent.SetActive(false);
     }

     public void PhasePannelFalse(int phaseNumber)
     {
         phasePannelParent.transform.GetChild(phaseNumber).gameObject.SetActive(false);
     }
     public void PhasePannelTrue(int phaseNumber)
     {
         phasePannelParent.transform.GetChild(phaseNumber).gameObject.SetActive(true);
     }
     */
    #endregion

    public void SetPanelActive(GameObject Pannel)
    {
        Pannel.SetActive(true);
    }
    public void SetPanelInactive(GameObject Pannel)
    {
        Pannel.SetActive(false);
    }
    #region "i" pannel animation
    public void Exit(GameObject gm)
    {
        gm.GetComponent<Animator>().SetBool("Out", true);
        mainMenu.SetActive(true);
        Invoke("Transit", 0.4f);
    }
    void Transit()
    {
        aboutsPannel.SetActive(false);
    }
    #endregion
    //Managing audio:


    public void PlayClickSound()
    {
        audSor.PlayOneShot(click);
    }
    #region Manage UI
    public void Share()
    {
        if (IconLerping.singleton.Open)
        {
            StartCoroutine(IconLerping.singleton.AnimateClose());
        }
        else
        {
            StartCoroutine(IconLerping.singleton.AnimateOpen());
        }
    }
    public void OpenURL(string URltoOpen)
    {
        Application.OpenURL(URltoOpen);
    }
    //Video Ad Credits and NoAds and leaderboard:
    public void GiveCredits()
    {

        if (AdsManager.singleton.IsFullVideoAdAvail())
        {
            AdsManager.singleton.ShowFullVideoAd((UnityEngine.Advertisements.ShowResult Result) =>
            {
                if (Result == UnityEngine.Advertisements.ShowResult.Finished)
                {
                    int prev = PlayerPrefs.GetInt("NumberOfGems", 10);
                    int r = Random.Range(7, 11);
                    int gift = prev + r;
                    giftText.text = "+ " + r.ToString();
                    giftText.gameObject.SetActive(true);
                    PlayerPrefs.SetInt("NumberOfGems", gift);
                    print(prev + " " + r + " " + gift);
                    prev = 0;r = 0;gift = 0;
                    Invoke("DisableGiftandMessageText", 2);
                }
            });
        }
        else { ad_Unavail_Mes.gameObject.SetActive(true); Invoke("DisableGiftandMessageText", 2); }
    }
    public void DisableGiftandMessageText() { giftText.gameObject.SetActive(false); ad_Unavail_Mes.gameObject.SetActive(false); }
#if UNITY_ANDROID
    public void ShowLeaderboard()
    {
        LeaderbaordManager.instance.ShowLeaderbaord();
    }
#endif
    public void NoAds()
    {
        //save no ads var
    }
    #endregion
}
