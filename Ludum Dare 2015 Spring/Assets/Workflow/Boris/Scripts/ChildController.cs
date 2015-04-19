using System.Collections.Generic;
using UnityEngine;

public abstract class ChildController : MonoBehaviour
{
    public delegate void FacingUpdateAction();

    private int _faceDirection = 1;
    protected Animator animator;
    protected BoxCollider2D boxCollider;
    protected bool continousWalking;
    public float lookRange = 1f;
    public Vector2 position;
    public float speedWalking = 4f;
    protected UnitState state;
    public float stepRange = 0.5f;

    public int FaceDirection
    {
        get { return _faceDirection; }
        set
        {
            _faceDirection = value;
            if (OnFacingChanged != null) OnFacingChanged();
        }
    }

    public event FacingUpdateAction OnFacingChanged;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();


        OnFacingChanged += Turn;
        boxCollider = GetComponent<BoxCollider2D>();

        position = new Vector2(transform.position.x, transform.position.y);
        state = UnitState.Idle;
    }

    protected virtual void Update()
    {
        if (state == UnitState.Walking)
        {
            
            var dir = Direction.ToVector2(FaceDirection);
            var wayleft = (position + dir*stepRange)
                          - new Vector2(transform.position.x, transform.position.y);

            var normalizedInvertDir = new Vector2(dir.y, -dir.x).normalized;


            var rayStart = new Vector2(position.x,
                position.y - stepRange*0.5f + boxCollider.size.y/2);
            Debug.DrawLine(rayStart, rayStart + (4*dir + normalizedInvertDir*2f).normalized*3f, Color.red);
            Debug.DrawLine(rayStart, rayStart + (4*dir - normalizedInvertDir*2f).normalized*3f, Color.red);
            if (HasCollision(Physics2D.RaycastAll(rayStart, (4*dir + normalizedInvertDir*1f).normalized))
                || HasCollision(Physics2D.RaycastAll(rayStart, (4*dir - normalizedInvertDir*1f).normalized)))
            {
                state = UnitState.Idle;
                animator.SetBool("walking", false);
                transform.position = new Vector3(position.x, position.y);
                return;
            }

            if (Vector2.Dot(wayleft, dir) > 0)
            {
                transform.Translate(dir*speedWalking*Time.deltaTime);
            }
            else
            {
                position += dir*stepRange;
                if (!continousWalking)
                {
                    transform.position = new Vector3(position.x, position.y);
                    state = UnitState.Idle;
                }
            }
            animator.SetBool("walking", true);
        }
        else if (state == UnitState.Idle)
        {
            animator.SetBool("walking", false);
        }
    }

    protected int SkipCollision(RaycastHit2D hit)
    {
        if (hit.collider.gameObject == transform.gameObject) return 0;
        if (hit.collider.transform.IsChildOf(transform)) return 0;
        if (hit.collider.gameObject.tag == "MovingChild") return 0;
        if (hit.fraction > stepRange*2f) return 1;
        return -1;
    }

    protected Collider2D FindComponentInColliders<T>(RaycastHit2D[] allhits)
    {
        
        foreach (var ray in allhits)
        {
            if (ray.collider.gameObject.GetComponent<T>() != null)
            {
                return ray.collider;
            }
        }
        return null;
    }
    protected virtual bool HasCollision(RaycastHit2D[] allhits)
    {
        foreach (var hit in allhits)
        {
            var i = SkipCollision(hit);
            if (i == 0) continue;
            if (i == 1) break;
            return true;
        }
        return false;
    }

    protected void Turn()
    {
        if (animator.GetInteger("direction") >= 0)
            animator.SetInteger("direction", FaceDirection);
    }

    private void Walk(Direction direction)
    {
        if (state == UnitState.Idle)
        {
            FaceDirection = direction;
            state = UnitState.Walking;
        }
        else
        {
            if (state == UnitState.Walking
                && FaceDirection == direction)
            {
                continousWalking = true;
            }
            else continousWalking = false;
        }
    }

    public void WalkUp()
    {
        Walk(Direction.Up);
    }

    public void WalkDown()
    {
        Walk(Direction.Down);
    }

    public void WalkLeft()
    {
        Walk(Direction.Left);
    }

    public void WalkRight()
    {
        Walk(Direction.Right);
    }

    public void stopWalking()
    {
        continousWalking = false;
    }

    protected enum UnitState
    {
        Idle,
        Walking
    }
}