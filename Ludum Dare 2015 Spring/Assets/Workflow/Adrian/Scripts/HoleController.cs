using System;
using UnityEngine;
using System.Collections;

public class HoleController : MonoBehaviour {
    
    private LevelController levelController;
    public bool flagReturnPortal;

    void Start()
    {
        if (GameObject.Find("LevelController") != null)
            levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BallController>())
        {
            BallController ball = other.GetComponent<BallController>();

            if(levelController != null)
                levelController.NotifyPachinko(ball.GetIndexPosition(), flagReturnPortal);

            Destroy(other.gameObject);
        }
    }
}

