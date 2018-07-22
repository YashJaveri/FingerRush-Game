using UnityEngine;
using System.Collections.Generic;

public class RandomUtilitySpawner : MonoBehaviour
{
    private GameObject Object;
    Vector3 randomPosition;
    [HideInInspector]
    public float Speed;

    public List<GameObject> gbList = new List<GameObject>();
    public int initialDelay;

    int randomInt;
    public float minPos, maxPos;
    public float minSpeed, maxSpeed;
    public float minTime, maxTime;

    public static RandomUtilitySpawner singleton;

    void Start()
    {
        Invoke("Spawn", initialDelay);

    }

    void Spawn()
    {
        CancelInvoke();
        SetVariables();
        float delay = Random.Range(minTime, maxTime);
        var clone = Instantiate(Object, randomPosition, transform.rotation);
        if (clone.GetComponent<UtilitiesMotion>() != null) { clone.SendMessage("SetSpeed", Speed); }
        clone.transform.parent = gameObject.transform;
        Invoke("Spawn", delay);
    }
    void SetVariables()
    {
            randomInt = Random.Range(0, gbList.Count);
        
        randomPosition = new Vector3(transform.position.x + Random.Range(minPos, maxPos), transform.position.y, -1);
        Object = gbList[randomInt];
        Speed = Random.Range(minSpeed, maxSpeed)
;
    }
}
