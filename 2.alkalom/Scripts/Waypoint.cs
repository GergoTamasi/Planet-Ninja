using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
	private void OnDrawGizmos()
	{
		
		
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(gameObject.transform.position,0.5f);
		
	}
}
