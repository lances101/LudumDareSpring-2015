using UnityEngine;
using System.Collections;

public class TeacherController : MonoBehaviour {

	// Use this for initialization
    [Header("AlternativeSprites")] 
    public Sprite AlertSprite;
    public Sprite AngrySprite;
    private Sprite _defaultSprite;
    private SpriteRenderer _spriteRenderer;
    private int childCount;
    void Awake()
    {
        _spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        _defaultSprite = _spriteRenderer.sprite;
    }
    void SetTeacherRange(float range)
    {
        GetComponent<CircleCollider2D>().radius = range;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name.ToLower().Contains("boy") && _spriteRenderer.sprite != AngrySprite)
        {
            var boy = coll.GetComponent<BoyController>();
            if (boy != null)
            {
                boy.IsWatchedByTeacher = true;
                childCount++;
                _spriteRenderer.sprite = AlertSprite;
            }
        }
        else if (coll.name.ToLower().Contains("girl"))
        {
            _spriteRenderer.sprite = AngrySprite;
        }


    }

    void OnTriggerExit2D(Collider2D coll)
    {

        if (coll.name.ToLower().Contains("boy") && _spriteRenderer.sprite != AngrySprite)
        {
            var boy = coll.GetComponent<BoyController>();
            if (boy != null)
            {
                boy.IsWatchedByTeacher = false;
                childCount--;
            }
            
        }
        else if (coll.name.ToLower().Contains("girl"))
        {
            _spriteRenderer.sprite = AlertSprite;
        }

        if (childCount == 0)
        {
            _spriteRenderer.sprite = _defaultSprite;
        }
        
        

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
