using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnpointManager : MonoBehaviour {

    void OnDrawGizmosSelected()
    {
        foreach (var point in GetTeacherPoints())
        {
            Gizmos.DrawCube(point, Vector3.one * 2);
        }
        foreach (var point in GetBoyPoints())
        {
            Gizmos.DrawWireCube(point, Vector3.one * 2);
            
        }
        foreach (var point in GetGirlPoints())
        {
            Gizmos.DrawWireSphere(point, 2);
        }
    }

    private Vector2[] GetPointsByName(string name)
    {
        List<Vector2> vecs = new List<Vector2>();
        foreach (Transform child in transform)
        {
            if (child.name == name) 
                vecs.Add(child.position);
        }
        return vecs.ToArray();
    }

    public Vector2[] GetTeacherPoints()
    {
        return GetPointsByName("Teacher");
    }

    public Vector2[] GetBoyPoints()
    {
        return GetPointsByName("Boy");
    }

    public Vector2[] GetGirlPoints()
    {
        return GetPointsByName("Girl");
    }

	// Update is called once per frame
	void Update () {
	    
	}
}
