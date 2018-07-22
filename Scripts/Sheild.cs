using UnityEngine;

public class Sheild : MonoBehaviour
{
    #region Variables
    public GameObject sheildDestroyParticles;
    GameObject player;
    #endregion
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            if (collision.contacts.Length > 0)
            {
                ContactPoint2D contactPoint = collision.contacts[0];
                collision.gameObject.SetActive(false);
                // Quaternion normal direction is still left.
                Destroy(this.gameObject);
                var clone = Instantiate(sheildDestroyParticles, contactPoint.point, transform.rotation);
                Destroy(clone, 0.7f);
            }
        }
    }
    
}
