
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlController : ChildController
{
    private BoyController selectedBoy;
    private bool IsCapturing;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        stopWalking();
        if(selectedBoy != null)
            Debug.DrawLine(position, selectedBoy.transform.position);
        ChooseKidToCapture();
        HandleInput();
    }

    private void HandleInput()
    {
        if (IsCapturing) return;
        var HorAxis = Input.GetAxis("Horizontal");
        var VerAxis = Input.GetAxis("Vertical");
        if (VerAxis > 0) WalkUp();
        if (HorAxis < 0) WalkLeft();
        if (HorAxis > 0) WalkRight();
        if (VerAxis < 0) WalkDown();
        if (Input.GetAxis("Jump") > 0)
        {
            TryToCapture();
        }
    }

    private void TryToCapture()
    {
        IsCapturing = true;
        if (selectedBoy != null)
        {
            if (Vector2.Distance(transform.position, selectedBoy.transform.position) < 2f)
            {


                if (selectedBoy.IsWatchedByTeacher)
                {
                    //GAME CONTROLLER LOSE
                    Debug.Log("YOU LOSE.. kind of");

                }
                else
                {

                    selectedBoy.HandleAbduction();
                    selectedBoy.SetSelectedByGirl(false);
                    selectedBoy = null;

                }
            }
        }
        animator.SetBool("capturing", true);
        StartCoroutine("CaptureEnd");
    }
    private IEnumerator CaptureEnd()
    {
        yield return new WaitForSeconds(1);
        IsCapturing = false;
        animator.SetBool("capturing", false);
        
    }
    public void ChooseKidToCapture()
    {
        var dir = Direction.ToVector2(FaceDirection);
        var wayleft = (position + dir*stepRange)
                      - new Vector2(transform.position.x, transform.position.y);

        var normalizedInvertDir = new Vector2(dir.y, -dir.x).normalized;

        var rayStart = new Vector2(position.x,
            position.y - stepRange*0.5f + boxCollider.size.y/2);
        Debug.DrawLine(rayStart, rayStart + (4*dir + normalizedInvertDir*2f).normalized*1f, Color.green);
        Debug.DrawLine(rayStart, rayStart + (4*dir - normalizedInvertDir*2f).normalized*1f, Color.green);
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