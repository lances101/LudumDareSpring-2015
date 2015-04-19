using UnityEngine;
using System.Collections;

public class TeacherController : MonoBehaviour {

	// Use this for initialization
    void SetTeacherRange(float range)
    {
        GetComponent<CircleCollider2D>().radius = range;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        
        var boy = coll.GetComponent < BoyController>();
        if (boy != null)
        {
            boy.IsWatchedByTeacher = true;
        }

    }

    void OnTriggerExit2D(Collider2D coll)
    {
        
        var boy = coll.GetComponent<BoyController>();
        if (boy != null)
        {
            boy.IsWatchedByTeacher = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
