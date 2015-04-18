using UnityEngine;
using System.Collections;

public class PachinkoController : MonoBehaviour {
    public GameObject ring;
    public GameObject ball;

    void Start()
    {
        ring.GetComponent<Rigidbody2D>().velocity = Vector2.right * 2;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(ball, ring.transform.position, Quaternion.identity);
        }
    }
    
    public void CreateBall(Sprite ballSprite, int indexPositionChildrenList)
    {
        if (ballSprite != null)
        {
            GameObject geBall = (GameObject)Instantiate(ball, ring.transform.position, Quaternion.identity);
            geBall.GetComponent<BallController>().SetSprite(ballSprite);
            geBall.GetComponent<BallController>().SetIndexPosition(indexPositionChildrenList);
        }
    }

}
