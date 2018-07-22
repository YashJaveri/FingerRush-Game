using UnityEngine;
using System.Collections;

public class ObstacleMotion : MonoBehaviour
{

    public float maxValue ; 
    public float minValue ; 
    float currentValue = 0; 
    int direction = 1;
    public float speed;
    public bool oscillate;
    public float rotationSpeed;
    public float rotationSpeedy = 0;

    void Update()
    {   if (oscillate == true)
        {
            currentValue += Time.deltaTime * direction * speed;
            if (currentValue >= maxValue)
            {
                direction *= -1;
                currentValue = maxValue;
            }
            else if (currentValue <= minValue)
            {
                direction *= -1;
                currentValue = minValue;
            }
            transform.position = new Vector3(currentValue, transform.position.y, 0);
        }


        transform.Rotate(0, rotationSpeedy * Time.deltaTime, rotationSpeed * Time.deltaTime,Space.Self);

    }
}
