using UnityEngine;
using System.Collections;

public class PachinkoController : MonoBehaviour {
    public GameObject ring;
    public GameObject ball;

    void Start()
    {
        ring.GetComponent<Rigidbody2D>().velocity = Vector2.right * 2;
    }

    
    public void CreateBall(Sprite ballSprite, int boyID)
    {
        if (ballSprite != null)
        {
            GameObject geBall = (GameObject)Instantiate(ball, ring.transform.position, Quaternion.identity);
            geBall.GetComponent<BallController>().SetSprite(ballSprite);
            geBall.GetComponent<BallController>().SetBoyID(boyID);
        }
    }

}
