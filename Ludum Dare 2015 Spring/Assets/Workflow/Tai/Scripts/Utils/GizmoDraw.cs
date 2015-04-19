using UnityEngine;
using System.Collections;

public class GizmoDraw : MonoBehaviour {

    void OnDrawGizmosSelected()
    {
        transform.parent.SendMessage("OnDrawGizmosSelected");
    }
}
