using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMovement : MonoBehaviour
{
    public float nonObsSpeed;
    public float incriment;
    public static BackgroundMovement singleton;
    public bool obstacle;
    public bool nonObstacle;
    public bool vAxisNonZero = false;

    bool isCleared;
    private void OnEnable()
    {
        isCleared = true;
        foreach (Transform g in GetComponentsInChildren<Transform>(true))
        {
            g.gameObject.SetActive(true);
        }
    }
    private void Awake()
    {
        singleton = this;
    }

    void Update()
    { 
        if (nonObstacle)
        {
            transform.position += new Vector3(0, Time.deltaTime * nonObsSpeed);
        }
    
        //Only for obstacles.
        if (obstacle)
        {
            if (PlayerMovement.singleton.pmScore == (SpeedManager.singleton.initialScore + 5))
            {
                SpeedManager.singleton.initialScore = SpeedManager.singleton.initialScore + 5;
                SpeedManager.singleton.obsSpeed += incriment;
            }
            transform.position += new Vector3(0, Time.deltaTime*SpeedManager.singleton.obsSpeed);
            if (transform.position.y <= -1f && isCleared)
            {
                gameObject.transform.GetComponentInParent<RandomObstacleSpawner>().SpawnObstacle();
                isCleared = false;
            }
            if (transform.position.y < -8)
            {
                gameObject.SetActive(false);
            }
            
           
        }
    }
}
