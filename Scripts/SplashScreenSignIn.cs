using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreenSignIn : MonoBehaviour {
    public static SplashScreenSignIn instance;
    public Slider slider;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
       
    }
    #if UNITY_ANDROID
    public void SignIn()
    {
        LeaderbaordManager.instance.SignIn();
    }
    #endif
    public void Skip()
    {
        Load();
    }
    public void Load()
    {
        StartCoroutine(Show_n_Load());
    }

    IEnumerator Show_n_Load()
    {
        slider.gameObject.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainMenu");
        while (!operation.isDone)
        {
            slider.value = operation.progress / 0.91f;
            yield return null;
        }
    }
}
