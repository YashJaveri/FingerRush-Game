/*using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    #region Variables
    public GameObject particleEffects;
    public GameObject sheildCircle;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Common actions(
            var clone = Instantiate(particleEffects, transform.position, transform.rotation);
            Destroy(clone, 1);
            //)Done.

            //PowerUp specific:
            if (gameObject.tag == "Sheild")
            {
                var instance = Instantiate(sheildCircle,Vector3.zero, Quaternion.identity);
                instance.transform.SetParent(collision.gameObject.transform);
                instance.transform.position += (Vector3.up * 0.2f);
            }
            if (gameObject.tag == "Jackpot")
            {
                GameManager.singleton.Jackpot(10);
            }
            if (gameObject.tag == "Slowdown")
            {
                GameManager.singleton.Slowdown();
            }

            if (gameObject.tag == "Shrink") 
            {
                GameManager.singleton.Shrink();
            }
            gameObject.SetActive(false);
            Invoke("DestroyThisGM", 1);
        }
    }
    void DestroyThisGM() { Destroy(gameObject); }

}*/
