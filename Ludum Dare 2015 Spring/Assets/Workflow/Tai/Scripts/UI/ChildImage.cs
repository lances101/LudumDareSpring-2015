using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ChildImage : MonoBehaviour {
    public Sprite sprite;
    public int indexPosition;

    public Image childImage;

    public void SetSpriteChild(Sprite sprite, int index)
    {
        childImage.sprite = sprite;
        indexPosition = index;
    }
}
