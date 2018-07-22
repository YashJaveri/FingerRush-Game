using UnityEngine;

public class RandomObstacleSpawner : MonoBehaviour
{
    #region Variables
    public int randomIndex = 0;
    private float randomX;
    public static RandomObstacleSpawner singleton;
    int min = 0;
    int max;

    int rosScore;
    public int difclty0min = 0, difclty0max;
    public int difclty1min, difclty1max;
    public int difclty2min, difclty2max;
    public int difclty3min, difclty3max;
    #endregion

    void Start()
    {
        randomIndex = 0;
        Invoke("SpawnObstacle", 3);
    }
    public void SpawnObstacle()
    {
        rosScore = PlayerMovement.singleton.pmScore;
        SettingRange();

        randomIndex = Random.Range(min, max + 1);
        while (gameObject.transform.GetChild(randomIndex).gameObject.activeSelf == true)
        {
            randomIndex = Random.Range(min, max);
        }

        EnableIt();
    }

    void SettingRange()
    {
        if (rosScore < 15) { min = difclty0min; max = difclty0max; }
        if (rosScore >= 15 && rosScore < 40) { min = difclty1min; max = difclty1max; }
        if (rosScore >= 40 && rosScore < 60) { min = difclty2min; max = difclty2max; }
        if (rosScore >= 60) { min = difclty3min; max = difclty3max; }
    }

    void EnableIt()
    {

        if (gameObject.transform.GetChild(randomIndex).gameObject.activeSelf == false)
        {
            GameObject go = gameObject.transform.GetChild(randomIndex).gameObject;
            Vector3 vector = go.transform.position;
            vector.y = transform.position.y;
            if (go.GetComponent<BackgroundMovement>().vAxisNonZero == true)
            {
                vector.x = Random.Range(-2, 2);
            }
            else
            {
                vector.x = 0;
            }
            gameObject.transform.GetChild(randomIndex).gameObject.transform.position = vector;
            go.SetActive(true);
        }
    }
}


/*int rosScore = PlayerMovement.singleton.pmScore;
        if (rosScore < 25) { min = 0; max = difclty1; invokeTime = time1; }
        if (rosScore >= 25 && rosScore < 50) { min = difclty1 - 7; max = difclty2; invokeTime = time1; }
        if (rosScore >= 50 && rosScore < 60) { min = difclty1 - 3; max = difclty3; invokeTime = time1; }//Speed = -7.
        //For Speed:
        if (rosScore >= 50 && rosScore < 51) { foreach (Component component in compsArray) { component.GetComponent<BackgroundMovement>().speed = -7; } }

        if (rosScore >= 60 && rosScore < 75) { min = difclty2 - 8; max = difclty3; invokeTime = time2; }
        //For Speed:
        if (rosScore >= 75 && rosScore < 76) { foreach (Component component in compsArray) { component.GetComponent<BackgroundMovement>().speed = -10; } }
        if (rosScore >= 75 && rosScore < 90) { min = difclty2 - 6; max = difclty3; invokeTime = time2; }//Speed = -10.
        if (rosScore >= 90) { min = difclty2 - 3; max = difclty4; invokeTime = time2; }*/
