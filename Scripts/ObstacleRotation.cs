using UnityEngine;
using System.Collections;

public class ObstacleRotation : MonoBehaviour
{
    public float rotationSpeedz;
    public float rotationSpeedx;
    void Update()
    {
        

        transform.Rotate(rotationSpeedx*Time.deltaTime, 0, rotationSpeedz* Time.deltaTime);

    }
}
