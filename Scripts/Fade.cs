using UnityEngine;

public class Fade : MonoBehaviour
{
    public float fadeSpeed = 0.8f;
    static public int direction = 0;
    public float alpha = 1;

    public GameObject fader;
    CanvasGroup cg;

    static public Fade singleton;
    static public bool thisIsCreated = false;

    private void Awake()
    {
        singleton = this;
        if (thisIsCreated == false)
        {
            DontDestroyOnLoad(this.gameObject);
            thisIsCreated = true;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    private void Start()
    {
        cg = fader.GetComponent<CanvasGroup>();
        alpha = 1;
        BeginFade(-1);
    }
    private void Update()
    {
        alpha += fadeSpeed * direction * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);
        cg.alpha = alpha;
        if (cg.alpha == 0) { cg.gameObject.SetActive(false); }
        else { cg.gameObject.SetActive(true); }
    }
    public void BeginFade(int fadeDirection)
    {
        direction = fadeDirection;
    }
    private void OnLevelWasLoaded()
    {
        alpha = 1;
        BeginFade(-1);

    }


    /* void OnGui()
        {
            alpha += fadeSpeed * direction * Time.deltaTime;
            Mathf.Clamp01(alpha);

            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
            GUI.depth = drawdepth;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), textureToDraw);
        }*/
}
