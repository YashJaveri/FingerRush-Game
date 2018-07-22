using UnityEngine;

public class CameraShake : MonoBehaviour {
    Vector3 startPos;
    public static CameraShake instance;
    public float shakePower;
    public float shakeDuration = 0;
    [HideInInspector]
    public bool doShake = false;
    // Use this for initialization
    private void Awake()
    {
        instance = this;
    }
    void Start () {
        startPos = transform.position;
	}

    // Update is called once per frame
    void Update() {
        if (doShake == true) { 
            if (shakeDuration > 0)
            {
            
                transform.position = startPos + Random.insideUnitSphere * shakePower;
                shakeDuration -= Time.deltaTime;
                shakePower *= 0.8f;
            }
            else {
                transform.position = startPos;
                doShake = false;
            }
        }
    }
}
