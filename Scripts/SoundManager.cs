using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public Sprite muteSprite;
    public Sprite unmuteSprite;
    public Button btn;
    GameObject soundSrcGM;
    private bool muted = false;
    SoundManager instance;
    AudioSource mainAudSrc,effectsAudSrc;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        effectsAudSrc = GameObject.Find("EffectsAudioSource").GetComponent<AudioSource>();
        mainAudSrc = GameObject.Find("MainAudioSource").GetComponent<AudioSource>();
        btn = FindObjectOfType<SoundManager>().GetComponent<Button>();
        if (PlayerPrefs.GetInt("Volume", 1) == 1)
        {
            btn.GetComponent<Image>().sprite = unmuteSprite;
            mainAudSrc.mute = false;
            effectsAudSrc.mute = false;
        }
        else if (PlayerPrefs.GetInt("Volume", 1) == 0)
        {
            btn.GetComponent<Image>().sprite = muteSprite;
            mainAudSrc.mute = true;
            effectsAudSrc.mute = true;
        }

    }

    public void SoundToggle()
    {
        if (PlayerPrefs.GetInt("Volume", 1) == 1)
        {
            btn.GetComponent<Image>().sprite = muteSprite;
            mainAudSrc.mute = true;
            effectsAudSrc.mute = true;
            PlayerPrefs.SetInt("Volume", 0);
        }
        else if (PlayerPrefs.GetInt("Volume", 1) == 0)
        {
            btn.GetComponent<Image>().sprite = unmuteSprite;
            mainAudSrc.mute = false;
            effectsAudSrc.mute = false;
            PlayerPrefs.SetInt("Volume", 1);
        }
    }
}
