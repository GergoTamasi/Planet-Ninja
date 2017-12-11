using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotator : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {
	    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
	    Debug.DrawRay(transform.position,mousePosition+ Vector3.up *transform.position.y, Color.red);
	    Debug.Log(Vector3.up*transform.position.y);
	    transform.LookAt(mousePosition+ Vector3.up *transform.position.y);
	}
}
