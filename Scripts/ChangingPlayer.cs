using UnityEngine;
using UnityEngine.UI;

public class ChangingPlayer : MonoBehaviour
{
    public int price;
    public int ButtonIndex;
    public int scoreRequired;
    public Text insufficientBalance;
    public GameObject ring;
    void Update()
    {

        if (GameManager.singleton.savedPlayerIndices.Contains(ButtonIndex) && PlayerPrefs.GetInt("PlayerIndex", 0) != ButtonIndex)
        {  //Something --> Use.
            GetComponentInChildren<Text>().text = "USE";
            ring.SetActive(false);
        }
        else if (GameManager.singleton.savedPlayerIndices.Contains(ButtonIndex) && PlayerPrefs.GetInt("PlayerIndex", 0) == ButtonIndex)
        {   //Use --> Equipped.
            GetComponentInChildren<Text>().text = "EQUIPPED";
            ring.SetActive(false);
        }
        else
        {
            if (ButtonIndex == 10) // For Special Ball.
            {
                ring.SetActive(false);
               if(!GameManager.singleton.savedPlayerIndices.Contains(ButtonIndex) && PlayerPrefs.GetInt("GamesPlayed_with_30+",0) >= 10)
                {
                    GameManager.singleton.savedPlayerIndices.Add(ButtonIndex);
                    Saver.Save(GameManager.singleton.savedPlayerIndices, "GameData");
                }
               GetComponentInChildren<Text>().text = "Score 10\n'30s'";
            }
            else if(ButtonIndex == 9){
               ring.SetActive(false);
               if(!GameManager.singleton.savedPlayerIndices.Contains(ButtonIndex) && PlayerPrefs.GetInt("GamesPlayed",0) >= 35)
                {
                    GameManager.singleton.savedPlayerIndices.Add(ButtonIndex);
                    Saver.Save(GameManager.singleton.savedPlayerIndices, "GameData");
                }
               GetComponentInChildren<Text>().text = "Play 35 rounds";
            }
            else{
             GetComponentInChildren<Text>().text = "  " + price + "\n" +"Score: " + scoreRequired; 
            }
        }
    }

    public void SelectPlayerButtonMethod()
    {

        if (GameManager.singleton.savedPlayerIndices.Contains(ButtonIndex))
        {
            PlayerPrefs.SetInt("PlayerIndex", ButtonIndex);
        }
        else
        {

            if (PlayerPrefs.GetInt("NumberOfGems") >= price && PlayerPrefs.GetInt("HighScore", 0) >= scoreRequired)
            {
                //Buy --> Equipped
                PlayerPrefs.SetInt("NumberOfGems", PlayerPrefs.GetInt("NumberOfGems") - price);
                GameManager.singleton.savedPlayerIndices.Add(ButtonIndex);
                PlayerPrefs.SetInt("PlayerIndex", ButtonIndex);
                Saver.Save(GameManager.singleton.savedPlayerIndices, "GameData");
                GameManager.singleton.PlayBuySound();
            }
            else if ((PlayerPrefs.GetInt("HighScore", 0) < scoreRequired && PlayerPrefs.GetInt("NumberOfGems") < price))
            {
                switch(ButtonIndex){
                    case 10:
                    insufficientBalance.gameObject.SetActive(true);
                    insufficientBalance.text = 10 - PlayerPrefs.GetInt("GamesPlayed_with_30+",0) + "Tries to go!";
                    Invoke("DisableText", 1); // To disable again.
                    break;
                    case 9:
                    insufficientBalance.gameObject.SetActive(true);
                    insufficientBalance.text =  35 - PlayerPrefs.GetInt("GamesPlayed",0) + "Tries to go!";
                    Invoke("DisableText", 1); // To disable again.  
                    break;
                    default:
                    //Less Score & Ring(currency).
                    insufficientBalance.gameObject.SetActive(true);
                    insufficientBalance.text = "NOT ENOUGH STATUS :(";
                    Invoke("DisableText", 1); // To disable again.
                    break;
                }
            }
            else if (PlayerPrefs.GetInt("HighScore", 0) < scoreRequired)
            {   //Less score.
                insufficientBalance.gameObject.SetActive(true);
                insufficientBalance.text = (scoreRequired - PlayerPrefs.GetInt("HighScore", 0)).ToString() + " MORE TO GO!";

                Invoke("DisableText", 1); // To disable again.
            }
            else if (PlayerPrefs.GetInt("NumberOfGems") < price)
            {   //Less Ring(Currency).
                insufficientBalance.gameObject.SetActive(true);
                insufficientBalance.text = "Need " + (price - PlayerPrefs.GetInt("NumberOfGems", 10)).ToString() + " More Rings!";
                Invoke("DisableText", 1);
                if ((price - PlayerPrefs.GetInt("NumberOfGems")) <= 11)
                {
                    PlayVideo();
                }
            }
        }
    }
    void DisableText()
    {
        insufficientBalance.gameObject.SetActive(false);
    }

    void PlayVideo()
    {
        AdsManager.singleton.ShowFullVideoAd((result) =>
        {
            if (result == UnityEngine.Advertisements.ShowResult.Finished)
            {
                int a = PlayerPrefs.GetInt("NumberOfGems");
                a += Random.Range(7, 11);
                PlayerPrefs.SetInt("NumberOfGems", a);
                a = 0;
            }
        });
    }
}
