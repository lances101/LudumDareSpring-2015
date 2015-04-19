using System;
using UnityEngine;
using System.Collections;

public class AnimationSpriteOverrider : MonoBehaviour
{

    public Sprite[] overridingSprites;
	// Use this for initialization
	void Start ()
	{
	    overridingSprites = Resources.LoadAll<Sprite>(path);
	}

    public string path;
	// Update is called once per frame
	void LateUpdate ()
	{

	    foreach (var renderer in GetComponents<SpriteRenderer>())
	    {
	        string spriteName = renderer.sprite.name;
	        var newSprite = Array.Find(overridingSprites, item => item.name == spriteName);
	        if (newSprite)
	        {
	            renderer.sprite = newSprite;
	        }
	    }
	}
}
