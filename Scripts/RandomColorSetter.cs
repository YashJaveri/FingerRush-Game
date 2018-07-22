using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorSetter : MonoBehaviour {
    #region Variables
    public List<Color> colorList = new List<Color>();
   
#endregion

    void OnEnable () {
        int randomInt = Random.Range(0, colorList.Count);
    
        Component[] colorArray = gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in colorArray)
        {
            sr.color = colorList[randomInt];
        }
    }
	
}
