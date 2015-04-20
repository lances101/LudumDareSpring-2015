using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
    public SpriteRenderer spriteRenderer;
    private int boyID = -1;

    public void SetSprite(Sprite ballSprite)
    {
        spriteRenderer.sprite = ballSprite;
    }

    public void SetBoyID(int indexPositionChildrenList)
    {
        this.boyID = indexPositionChildrenList;
    }

    public int GetBoyID()
    {
        return this.boyID;
    }
}
