using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {


    [CreateAssetMenu(menuName = "AI/Actions/Patrol")]
    public class PatrolAction : Action {


        public override void Act(StateController stateController) {
            Patrol(stateController);
        }

        private void Patrol(StateController stateController) {

            stateController.Patrol.ResumePartolling();

            if (stateController.Patrol.CheckIfReachWayPoint()) {
                stateController.Patrol.SetNextWaypoint();
            }
        }
    }
}