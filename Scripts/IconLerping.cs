using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IconLerping : MonoBehaviour
{
    public static IconLerping singleton;
    public float radius;
    public float speed;
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    public bool Open = false;
    void Start()
    {
        Open = false;
        singleton = this;
        Button1.transform.position = transform.position;
        Button2.transform.position = transform.position;
        Button3.transform.position = transform.position;
        Button1.SetActive(false);
        Button2.SetActive(false);
        Button2.SetActive(false);
    }
    private void Update()
    {
        if (Application.isEditor)
        {
            if (Open)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    StartCoroutine(AnimateClose());
                }
            }
        }
        else
        {
            if (Input.touchCount > 0 && Open == true)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Ended)
                {
                    StartCoroutine(AnimateClose());
                }
            }
        }
    }
    public IEnumerator AnimateOpen()
    {
        Button1.transform.position = transform.position;
        Button2.transform.position = transform.position;
        Button3.transform.position = transform.position;
        Vector3 btn1pt = transform.position + new Vector3(radius, radius);
        Vector3 btn2pt = transform.position + new Vector3(0, radius);
        Vector3 btn3pt = transform.position + new Vector3(-radius, radius);
        Button1.SetActive(true);
        Button2.SetActive(true);
        Button3.SetActive(true);
        while (Button1.transform.position != btn1pt && Button2.transform.position != btn2pt && Button3.transform.position != btn3pt)
        {
            Button1.transform.position = Vector3.MoveTowards(Button1.transform.position, btn1pt, speed);
            Button2.transform.position = Vector3.MoveTowards(Button2.transform.position, btn2pt, speed);
            Button3.transform.position = Vector3.MoveTowards(Button3.transform.position, btn3pt, speed);
            yield return null;
        }
        Open = true;
    }
    public IEnumerator AnimateClose()
    {
        while (Button1.transform.position != transform.position && Button2.transform.position != transform.position && Button3.transform.position != transform.position)
        {
            Button1.transform.position = Vector3.MoveTowards(Button1.transform.position, transform.position, speed);
            Button2.transform.position = Vector3.MoveTowards(Button2.transform.position, transform.position, speed);
            Button3.transform.position = Vector3.MoveTowards(Button3.transform.position, transform.position, speed);
            yield return null;
        }
        Button1.SetActive(false);
        Button2.SetActive(false);
        Button3.SetActive(false);
        Open = false;
    }
}
