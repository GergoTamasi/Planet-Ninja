using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Complete {


    [CreateAssetMenu(menuName = "AI/Decisions/Look")]
    public class LookDecision : Decision {

        public override bool Decide(StateController stateController) {

            return Look(stateController);
        }

        private bool Look(StateController stateController) {
            RaycastHit[] hits = Physics.SphereCastAll(stateController.Patrol.transform.position,
                stateController.Patrol.EnemyLookCastSphereRadius, stateController.Patrol.child.forward, 1f);
            if (hits.Length > 0) {
                for (int i = 0; i < hits.Length; i++) {
                    if (hits[i].collider.CompareTag("Player")) {
                        Transform player = hits[i].transform;
                        Vector3 directionToPlayer = (player.position - stateController.Patrol.transform.position).normalized;
                       
                        if (Vector3.Angle(stateController.Patrol.child.forward, directionToPlayer) <
                            stateController.Patrol.EnemyFieldOfView / 2) {
                            
                            Debug.DrawRay(stateController.transform.position,
                                directionToPlayer * Vector3.Distance(player.position,
                                    stateController.Patrol.transform.position),
                                Color.green);
                            stateController.Patrol.ChasingTarget = player;
                            return true;
                        }
                        Debug.DrawRay(stateController.transform.position,
                            directionToPlayer * Vector3.Distance(player.position,
                                stateController.Patrol.transform.position),
                            Color.red);
                        return false;
                    }
                }

            }
            return false;
        }

    }
}
