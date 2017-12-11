
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class Waypoint : MonoBehaviour {
    
    

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position,0.5f);
    }
}
