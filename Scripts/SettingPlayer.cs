using UnityEngine;

public class SettingPlayer : MonoBehaviour {
    GameObject[] playersArray;
	void Start () {
        int playerIndex = PlayerPrefs.GetInt("PlayerIndex", 0);
        playersArray = new GameObject[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            playersArray[i] = transform.GetChild(i).gameObject;
        }
        playersArray[playerIndex].SetActive(true);
    }
	void Update () {
		
	}
     
}
