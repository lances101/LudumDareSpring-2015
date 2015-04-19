using UnityEngine;
using System.Collections;

public class LayerSorter : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	// Update is called once per frame
	void Update () {
	    CheckLayer();   
	}

    void CheckLayer()
    {
        var raycast = Physics2D.RaycastAll(transform.position, Vector2.up*-1, 4);
        Debug.DrawRay(transform.position, Vector2.up*-1, Color.red);
        foreach (var hit in raycast)
        {
            Debug.DrawLine(transform.position, hit.collider.transform.position, Color.magenta);
            
            var rend = hit.collider.GetComponent<SpriteRenderer>();
            if(rend == null) continue;
            if (spriteRenderer.sortingOrder >= rend.sortingOrder)
            {
                spriteRenderer.sortingOrder = rend.sortingOrder-1;
                
            }

        }

    }
}
