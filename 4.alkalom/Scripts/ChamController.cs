using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class ChamController : MonoBehaviour {
    private Animator _anim;

    private float _speed=0;
    
	// Use this for initialization
	void Start () {
	    _anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	    _speed = Input.GetAxis("Vertical");
	    _anim.SetFloat("Speed",_speed);

	    if (Input.GetKeyDown(KeyCode.Space) && _speed >0.1f) {
	        _anim.SetTrigger("jump");
	    }
	}
}
