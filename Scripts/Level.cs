using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static Level singleton;

    public string phaseName = "";
    private string internalPhaseName;
    public LevelType leveltype;
    
    public int LevelID;
    private bool complete;
    public string LevelName;
    
    public Image LockImage;
    public Text text;
    public Sprite levelIconComplete;

    void OnEnable()
    {

        if (leveltype == LevelType.Phase)
        {
            internalPhaseName = phaseName;
            GameManager.singleton.LoadApproptFile(internalPhaseName);
         

        }
    }
    void Start()
    {
        if (leveltype == LevelType.Level)
        {
         
            GetComponent<Button>().onClick.AddListener(() => LoadLevel());
        }


        if (leveltype == LevelType.InnerLevel)
        {
            if (LevelID == PlayerPrefs.GetInt("innerLevelID", 0))
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
   
    }
        

    void Update()
    {
   
          if (GameManager.singleton.savedCompletedLevelIDs != null)
        {
            if (GameManager.singleton.savedCompletedLevelIDs.Contains(LevelID))
            { complete = true; }
            else { complete = false; }
        }
        if (leveltype == LevelType.Level)
        {
            if (LevelID == GetLastCompletedInList() + 1)
            {
                LockImage.enabled = false;
                text.enabled = true;
            }
            else if (complete)
            {
                GetComponent<Image>().sprite = levelIconComplete;
                GetComponentInChildren<Text>().color = Color.white;
                LockImage.enabled = false;
                text.enabled = true;
            }
            else
            {
                LockImage.enabled = true;
                text.enabled = false;
            }
        }
            
    }


    private void LoadLevel()
    {
        if (complete || LevelID == GetLastCompletedInList() + 1) {
            PlayerPrefs.SetInt("innerLevelID", LevelID);
            SceneManager.LoadSceneAsync(LevelName);
        }
    }

 

    public int GetLastCompletedInList() {

        int output = -1;
        for (int i = 0; i < GameManager.singleton.savedCompletedLevelIDs.Count; i++)
        {
                output = i;
        }
        return output;

    }

    public enum LevelType {
        Phase, Level, InnerLevel
    }
}