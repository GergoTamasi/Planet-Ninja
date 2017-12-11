using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Complete;
using NUnit.Framework;
using UnityEngine;

namespace Complete {

    [CreateAssetMenu(menuName = "AI/Decisions/CheckIfCaught")]
    public class CheckIfCaught : Decision {

        public override bool Decide(StateController stateController) {
            return CheckIfPlayerIsCaught(stateController);
        }

        private bool CheckIfPlayerIsCaught(StateController stateController) {
            RaycastHit[] hits = Physics.SphereCastAll(stateController.Patrol.transform.position,
                stateController.Patrol.WaypointCastSphereRadius, stateController.Patrol.child.forward, 1f);
            if (hits.Length > 0) {
                for (int i = 0; i < hits.Length; i++) {
                    if (hits[i].collider.CompareTag("Player")) {
                        Debug.Log("Catch");
                        return true;
                }
            }
            }
            return false;

        }

    }
}
