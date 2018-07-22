using UnityEngine;
using System.Collections;
using System.IO;

public class Share : MonoBehaviour
{

    private string shareText;

    private string gameLink = "https://play.google.com/store/apps/details?id=com.imbuegen.ExiledFingers";

    private string subject = "Infinite Turns";

    public bool justPlayed = false;

    private bool isProcessing = false;
    void Start()
    {

    }
    void Update()
    {
        if (Input.touches.Length > 0 && GetComponent<GUITexture>().HitTest(Input.touches[0].position))
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                GetComponent<GUITexture>().enabled = false;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {

            }
        }
    }

    public void ShareContent()
    {
        if (!isProcessing)
            StartCoroutine(ShareScreenshot());

    }
    public IEnumerator ShareScreenshot()
    {
        isProcessing = true;

        // wait for graphics to render
        yield return new WaitForEndOfFrame();
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
        // create the texture
        Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);

        // put buffer into texture
        screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);

        // apply
        screenTexture.Apply();
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO

        byte[] dataToSave = screenTexture.EncodeToPNG();

        string destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");

        File.WriteAllBytes(destination, dataToSave);

        if (!Application.isEditor)
        {
            if (justPlayed == true)
                shareText = "Hey, look I just cleared " + PlayerMovement.singleton.pmScore.ToString() + " obstacles. " + "\r\n" + "Lets see how many you can. Get this amazing game 'Exiled Finger' now : ";
            else
                shareText = "My high score is " + PlayerPrefs.GetInt("HighScore",0) + ". \r\n" + "I challenge you to beat me, can you?? Get this amazing game 'Exiled Finger' now : ";
            // block to open the file and share it ------------START
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareText + gameLink);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            // option one WITHOUT chooser:
            //currentActivity.Call("startActivity", intentObject);

            //option two WITH chooser:
            AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share your score with your friend");
            currentActivity.Call("startActivity", jChooser);

            // block to open the file and share it ------------END

        }
        isProcessing = false;
        GetComponent<GUITexture>().enabled = true;
    }
}
