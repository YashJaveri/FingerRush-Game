using UnityEngine;
using UnityEngine.Audio;

public class GemReceive : MonoBehaviour
{
    public GameObject coinParticles;
    static int numberOfGems = 10;
    public GameObject audGM;
    public AudioClip ringReceive;
    private AudioSource mainAudSrc;

    private void Start()
    {
        mainAudSrc = GameObject.Find("EffectsAudioSource").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mainAudSrc.PlayOneShot(ringReceive);
            Destroy(gameObject);
            var clone = Instantiate(coinParticles, transform.position, transform.rotation);
            Destroy(clone, 1);
            numberOfGems = PlayerPrefs.GetInt("NumberOfGems", 10)+1;
            PlayerPrefs.SetInt("NumberOfGems", numberOfGems);
        }
    }
}
