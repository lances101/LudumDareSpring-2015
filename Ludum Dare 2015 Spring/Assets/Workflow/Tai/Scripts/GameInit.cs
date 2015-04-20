using UnityEngine;
using System.Collections;

public class GameInit : MonoBehaviour {

    public GameObject gameController;
	
    void Awake () {
        //gameController = new GameObject();
        //gameController.AddComponent<GameController>();
        var gc = Instantiate(gameController); 
        Debug.Log("GC " + gc.GetComponent<GameController>().GetHashCode());
	}
}
