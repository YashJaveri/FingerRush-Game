using UnityEngine;

public class EndLevelTrigger : MonoBehaviour {
    public int LevelID;
    public string phaseName;
  
    private void OnTriggerEnter2D(Collider2D collision)
    { if (collision.gameObject.tag == "Player")
        {

            if (!GameManager.singleton.savedCompletedLevelIDs.Contains(LevelID))
            {
                GameManager.singleton.savedCompletedLevelIDs.Add(LevelID);
                Saver.Save(GameManager.singleton.savedCompletedLevelIDs,phaseName);
                }

        }
    }
}