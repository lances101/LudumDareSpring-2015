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
            speedWalking = Random.Range(2, 5);
            _changingTime = DateTime.Now;
           
        }

        private int _movingDirection;
        private DateTime _changingTime;
        public GameObject SelectionIndicator;
        public void SetSelectedByGirl(bool val)
        {
            if (val)
            {
                SelectionIndicator.SetActive(true);
            }
            else
            {
                SelectionIndicator.SetActive(false);
            }
            
        }

        protected override void Update()
        {
            base.Update();
            stopWalking();
            if ((state == UnitState.Idle && _movingDirection != -1) || DateTime.Now > _changingTime)
            {
                var r = Random.value;
                if (r > 0.9f)
                    _movingDirection = -1;
                else
                    _movingDirection = Random.Range(0, 4);
                Debug.Log("Random was " + r);
                _changingTime = DateTime.Now.AddMilliseconds(Random.Range(500, 2500));
            }
            
            switch(_movingDirection)
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
                case -1:
                    break;
            }

        }


    }

}
