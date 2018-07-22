using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement :MonoBehaviour
{
    #region Variables 
    public float gap;
    public GameObject particles;
    SpriteRenderer playerSprite;
    public Vector3 playerSpriteSize = new Vector3(1.5f, 1.5f, 0);
    public int pmScore = 0;
    public int pmHighScore;
    public Text scoreText;
    public static PlayerMovement singleton;
    GameObject go;
    public Text gems;
    #endregion

    private void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {

        pmScore = PlayerPrefs.GetInt("RevivedScore", 0);
        scoreText.text = pmScore.ToString();
        singleton = this;
        go = GameObject.Find("ObstacleSpawner");
        scoreText.enabled = true;

        AdMobAds.singleton.ShowBanner();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.contacts.Length > 0)
            {
                ContactPoint2D contact = collision.contacts[0];

                //Instantiate particles and disable player.
                var clone = Instantiate(particles, contact.point, Quaternion.LookRotation(contact.normal));
                gameObject.SetActive(false);
                //Disabling ObstacleSpawnerComponent.
                go.GetComponent<RandomObstacleSpawner>().enabled = false;
                //Shake Camera.
                CameraShake.instance.shakeDuration = 0.3f;
                CameraShake.instance.doShake = true;
                
                if (PlayerPrefs.GetInt("Revived", 0) == 0 && AdsManager.singleton.IsFullVideoAdAvail())
                {
                    Invoke("DisplayRevivePrompt", 0.6f);
                pmHighScore = PlayerPrefs.GetInt("HighScore", 0);
                if (PlayerPrefs.GetInt("HighScore", 0) < pmScore)
                {
                    PlayerPrefs.SetInt("HighScore", pmScore);
                    pmHighScore = pmScore;
                    #if UNITY_ANDROID
                    LeaderbaordManager.instance.PostScore(pmHighScore);
                    #endif
                }
            }
                else
                {
                //Score Managing and Displaying:
                pmHighScore = PlayerPrefs.GetInt("HighScore", 0);
                    if (PlayerPrefs.GetInt("HighScore", 0) < pmScore)
                    {
                    PlayerPrefs.SetInt("HighScore", pmScore);
                    pmHighScore = pmScore;
                    #if UNITY_ANDROID
                    LeaderbaordManager.instance.PostScore(pmHighScore);
                    #endif
                    }
                Invoke("DisplayGameOverScreen", 0.7f);
                }
             Destroy(clone, 1);
            }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Scorer")
        {
            pmScore += 1;
            scoreText.text = pmScore.ToString();
        }
    }

    void Update()
    {
        PlayerControl();

        if (gems != null) { gems.text = PlayerPrefs.GetInt("NumberOfGems", 10).ToString(); }
    }

    #region Controller
    void PlayerControl()
    {


        if (Application.isEditor)
        {
            if (Input.GetMouseButton(0))
            {

                Vector2 req = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var clampedX = Mathf.Clamp(req.x, -2.35f, 2.35f);
                var clampedY = Mathf.Clamp(req.y, -4.5f, 4.5f);
                Bounds playerBounds = new Bounds(gameObject.transform.position, playerSpriteSize);
                if (playerBounds.Contains(req))
                {

                    transform.position = new Vector2(clampedX, clampedY + gap);
                }

            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                var reqtouch = Input.GetTouch(0);
                Vector2 touchPoint = Camera.main.ScreenToWorldPoint(reqtouch.position);
                var clampedX = Mathf.Clamp(touchPoint.x, -2.5f, 2.5f);
                var clapmedY = Mathf.Clamp(touchPoint.y, -4.5f, 4.5f);
                Bounds playerBounds = new Bounds(gameObject.transform.position, playerSpriteSize);
                if (playerBounds.Contains(touchPoint))
                {
                    transform.position = new Vector2(clampedX, clapmedY + gap);
                }
            }
        }

    }
    #endregion


    #region Displaying
    void DisplayGameOverScreen()
    {
        GameManager.singleton.DisplayGameOverScreen();
        //Score Displayer:
        GameManager.singleton.scoreText.text = pmScore.ToString();  // <-- Refers to GameOverScreen's Text.
        GameManager.singleton.highScore.text = "YOUR BEST:\n" + pmHighScore.ToString();
    }
    void DisplayRevivePrompt()
    {
        GameManager.singleton.DisplayRevivePrompt();
        Invoke("DisplayGameOverScreen", 3);
    }
    #endregion
    void SwitchingBetweenScreens()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
