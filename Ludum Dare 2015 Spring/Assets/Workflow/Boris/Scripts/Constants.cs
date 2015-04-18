using UnityEngine;
using System.Collections;

public class Direction{
    
    public static readonly Direction Right   = new Direction(0); 
    public static readonly Direction Up      = new Direction(1);
    public static readonly Direction Left    = new Direction(2);
    public static readonly Direction Down    = new Direction(3);
    
    private int val;
    
    private Direction(int v){ val = v; }
    
    public static implicit operator int(Direction d){
        return d.val;
    }
    
    public static implicit operator Direction(int i){
        switch(i){
        case(0): return Right;
        case(1): return Up;
        case(2): return Left;
        case(3): return Down;
        default: throw new System.ArgumentOutOfRangeException("Invalid parameter");
        }    
    }

    public static Vector2 ToVector2(Direction d){
        return new Vector2(Mathf.Cos (0.5f*Mathf.PI*d), Mathf.Sin (0.5f*Mathf.PI*d)).normalized;
    }
}

