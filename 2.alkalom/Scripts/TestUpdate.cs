using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUpdate : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {
		Debug.Log("Time elapsed:" + Time.deltaTime);
	}

	private void FixedUpdate()
	{
		Debug.Log("Time elapsed:" + Time.fixedDeltaTime);
	}
}
