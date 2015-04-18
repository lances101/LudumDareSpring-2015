using UnityEngine;
using System.Collections;

public class HoleController : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BallController>())
        {
            Destroy(other.gameObject);
        }
    }
}
