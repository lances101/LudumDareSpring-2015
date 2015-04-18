using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GirlController : ChildController {


    override protected void Start () {
        base.Start();
	}
	
	
    protected override void Update()
    {
        base.Update();
        stopWalking();

        var HorAxis = Input.GetAxis("Horizontal");
        var VerAxis = Input.GetAxis("Vertical");
        if (VerAxis > 0) WalkUp();
        if (HorAxis < 0) WalkLeft();
        if (HorAxis > 0)WalkRight();
        if (VerAxis < 0) WalkDown();

    }

}
