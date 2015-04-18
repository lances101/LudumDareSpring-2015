using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
    public SpriteRenderer spriteRenderer;
    private int indexPositionChildrenList;

    public void SetSprite(Sprite ballSprite)
    {
        spriteRenderer.sprite = ballSprite;
    }

    public void SetIndexPosition(int indexPositionChildrenList)
    {
        this.indexPositionChildrenList = indexPositionChildrenList;
    }

    public int GetIndexPosition()
    {
        return this.indexPositionChildrenList;
    }
}
