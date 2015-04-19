using UnityEngine;
using System.Collections;

public class GameInit : MonoBehaviour {

    public GameObject gameController;
	
    void Awake () {
        //gameController = new GameObject();
        //gameController.AddComponent<GameController>();
        Instantiate(gameController);
	}
}
