using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {

[CreateAssetMenu(menuName = "AI/Actions/Chase")]
public class Chasing : Action {
	public override void Act(StateController stateController) {
		ChaseTarget(stateController);
	}


	private void ChaseTarget(StateController stateController) {
			stateController.Patrol.ResumeChasing();
	}
}
}