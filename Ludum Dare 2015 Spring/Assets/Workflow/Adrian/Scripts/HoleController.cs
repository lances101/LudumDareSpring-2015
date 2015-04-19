using System;
using UnityEngine;
using System.Collections;

public class HoleController : MonoBehaviour {
    private LevelController levelController;
    public bool flagReturnPortal;
    void Start()
    {
        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BallController>())
        {
            BallController ball = other.GetComponent<BallController>();
            levelController.NotifyPachinko(ball.GetIndexPosition, flagReturnPortal);
            Destroy(other.gameObject);
        }
    }
}

internal class LevelController
{
    public void NotifyPachinko(Func<int> getIndexPosition, bool flagReturnPortal)
    {
        throw new NotImplementedException();
    }

    public static void SendToPachinko(int boyId)
    {
        throw new NotImplementedException();
    }
}
