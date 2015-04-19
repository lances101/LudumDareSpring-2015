using UnityEngine;
using System.Collections;

public class GameInit : MonoBehaviour {

    private GameObject gameController;
	
    void Awake () {
        gameController = new GameObject();
        gameController.AddComponent<GameController>();
	}
}
