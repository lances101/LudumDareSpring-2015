using UnityEngine;
using System.Collections;

public abstract class ChildController : MonoBehaviour {

    public float speedWalking = 4f;
    public float stepRange = 0.5f;
    public float lookRange = 1f;

    public Vector2 position; // needs to be filled in Start() 

    private int _faceDirection = 1;
    public delegate void FacingUpdateAction();
    public event FacingUpdateAction OnFacingChanged;

    public int FaceDirection
    {
        get { return _faceDirection; }
        set
        {
            _faceDirection = value;
            if (OnFacingChanged != null) OnFacingChanged();
        }
    }


    protected bool continousWalking;

    protected Animator animator;

    protected UnitState state;

    virtual protected void Start () {
        this.animator = GetComponent<Animator>();


        OnFacingChanged += Turn;
        this.position = new Vector2(transform.position.x, transform.position.y);
        state = UnitState.Idle;
    }

    virtual protected void Update(){
        if(this.state == UnitState.Walking){
            // create direction vector basing of FaceDirection
            Vector2 dir = Direction.ToVector2(FaceDirection);
            Vector2 wayleft = (position + dir * stepRange) 
                                - new Vector2(transform.position.x, transform.position.y);

            Vector2 Odir = new Vector2(dir.y, -dir.x).normalized;

            Vector2 rayStart = new Vector2(position.x, 
                                           position.y - stepRange*0.5f);
            // raycasting in that direction and checking if enough freespace
            Debug.DrawLine(rayStart, rayStart + (2*dir+Odir*0.49f).normalized);
            Debug.DrawLine(rayStart, rayStart + (2*dir-Odir*0.49f).normalized);
            if(hasCollision(Physics2D.RaycastAll(rayStart, (2*dir+Odir*0.49f).normalized))
               || hasCollision(Physics2D.RaycastAll(rayStart, (2*dir-Odir*0.49f).normalized))){
                   this.state = UnitState.Idle;
                animator.SetBool("walking", false);
                transform.position = new Vector3(position.x, position.y);
                return;
            }

            if(Vector2.Dot(wayleft, dir) > 0){
                transform.Translate(dir * speedWalking * Time.deltaTime);
            }else{
                position += dir * stepRange;
                if(! continousWalking){
                    transform.position = new Vector3(position.x, position.y);
                    this.state = UnitState.Idle;
                }
            }
            animator.SetBool("walking", true);
        }else if(this.state == UnitState.Idle){
            animator.SetBool("walking", false);
        }
    }

    protected int SkipCollision(RaycastHit2D hit){
        if(hit.collider.gameObject == this.transform.gameObject) return 0;
        if(hit.collider.transform.IsChildOf(this.transform)) return 0;
        if(hit.fraction > stepRange*1.3f) return 1;
        return -1;
    }

    virtual protected bool hasCollision(RaycastHit2D [] allhits){
        foreach(RaycastHit2D hit in allhits){
            int i = SkipCollision(hit);
            if(i == 0) continue;
            else if(i == 1) break;
            return true;
        }
        return false;
    }

    protected void Turn(){
        if(animator.GetInteger("direction") >= 0)
            animator.SetInteger("direction", FaceDirection);
    }

    private void Walk(Direction direction){
        if(this.state == UnitState.Idle) 
        {
            this.FaceDirection = direction;
            this.state = UnitState.Walking;
        }else{
            if (this.state == UnitState.Walking
               && this.FaceDirection == direction){
                this.continousWalking = true;
            }
            else this.continousWalking = false;
        }
        return;
    }

    public void WalkUp() { Walk(Direction.Up); }
    public void WalkDown() { Walk(Direction.Down); }
    public void WalkLeft() { Walk(Direction.Left); }
    public void WalkRight() { Walk(Direction.Right); }
    public void stopWalking(){ continousWalking = false; }

    protected enum UnitState
    {
        Idle, Walking
    }
}



