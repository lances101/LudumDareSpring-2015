using Assets.Workflow.Shitfix.Scripts;
using UnityEngine;

public class GirlController : ChildController
{
    private BoyController selectedBoy;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        stopWalking();

        ChooseKidToCapture();
        HandleInput();
    }

    private void HandleInput()
    {
        var HorAxis = Input.GetAxis("Horizontal");
        var VerAxis = Input.GetAxis("Vertical");
        if (VerAxis > 0) WalkUp();
        if (HorAxis < 0) WalkLeft();
        if (HorAxis > 0) WalkRight();
        if (VerAxis < 0) WalkDown();
        if (Input.GetAxis("Jump") > 0)
        {
            if (selectedBoy != null)
            {
                selectedBoy.gameObject.SetActive(false);   
                //SENT TO HELL
            }
        }
    }

    public void ChooseKidToCapture()
    {
        var dir = Direction.ToVector2(FaceDirection);
        var wayleft = (position + dir*stepRange)
                      - new Vector2(transform.position.x, transform.position.y);

        var normalizedInvertDir = new Vector2(dir.y, -dir.x).normalized;

        var rayStart = new Vector2(position.x,
            position.y - stepRange*0.5f + boxCollider.size.y/2);
        Debug.DrawLine(rayStart, rayStart + (4*dir + normalizedInvertDir*2f).normalized*3f, Color.red);
        Debug.DrawLine(rayStart, rayStart + (4*dir - normalizedInvertDir*2f).normalized*3f, Color.red);
        var collider = FindComponentInColliders<BoyController>(Physics2D.RaycastAll(rayStart, (4*dir).normalized));
        if (selectedBoy == collider) return;
        
        
        if (collider == null)
        {
            if (selectedBoy != null)
                selectedBoy.SetSelectedByGirl(false);
            selectedBoy = null;
            return;
        }

        
        if(selectedBoy != null)
            selectedBoy.SetSelectedByGirl(false);
        selectedBoy = collider.gameObject.GetComponent<BoyController>();
        selectedBoy.SetSelectedByGirl(true);


    }
}