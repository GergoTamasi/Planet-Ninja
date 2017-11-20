using System.Collections;
using System.Collections.Generic;
using UnityEngine;


	[RequireComponent(typeof(Rigidbody))]
	public class GravityBody : MonoBehaviour {

		private PlanetAttractor _planet;
		public PlanetAttractor Planet => _planet;

		private Rigidbody _rbody;




		void Awake() {
			_planet = GameObject.FindObjectOfType<PlanetAttractor>();
			_rbody = GetComponent<Rigidbody>();
			_rbody.useGravity = false;
			_rbody.constraints = RigidbodyConstraints.FreezeRotation;
		}

		void FixedUpdate() {
			_planet.Attract(_rbody);
		}
	}

