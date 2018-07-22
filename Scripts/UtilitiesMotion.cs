using UnityEngine;

public class UtilitiesMotion : MonoBehaviour {
    [HideInInspector]
   public float Speed;
    public static UtilitiesMotion singleton;
  
	void Start () {
       
    }
	
	void Update () {
        transform.position += transform.up * Speed * Time.deltaTime;
        Destroy(gameObject, 2.5f);
    }
   public void SetSpeed(float speed)
    {
        Speed = speed;

    }
}
