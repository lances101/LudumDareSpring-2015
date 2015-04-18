using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Workflow.Shitfix.Scripts
{
    class BoyController : ChildController
    {


        override protected void Start()
        {
            base.Start();
        }


        protected override void Update()
        {
            base.Update();
            stopWalking();

            switch(Random.Range(0, 5))
            {
                case 0:
                    WalkDown();
                    break;
                case 1:
                    WalkLeft();
                    break;

                case 2:
                    WalkRight();
                    break;

                case 3:
                    WalkUp();
                    break;
            }

        }

    }

}
