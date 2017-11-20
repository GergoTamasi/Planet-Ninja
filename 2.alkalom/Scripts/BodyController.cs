using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	[RequireComponent(typeof(GravityBody))]
	public class BodyController : MonoBehaviour {

		public float MoveSpeed = 2f;


		private Rigidbody _rbody;


		private Vector3 _smoothMoveAmount;
		private Vector3 _calculatedMoveAmount;
		private Vector3 _moveDirection;
		private Vector3 _smoothMoveVelocity;




		void Awake() {
			_rbody = GetComponent<Rigidbody>();

		}

		void Update() {

			_moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
			_calculatedMoveAmount = _moveDirection * MoveSpeed;
			_smoothMoveAmount =Vector3.SmoothDamp(_smoothMoveAmount, _calculatedMoveAmount, ref _smoothMoveVelocity, .15f);

		}

		private void FixedUpdate() {
			var localMove = transform.TransformDirection(_smoothMoveAmount) * Time.fixedDeltaTime;
			_rbody.MovePosition(_rbody.position + localMove);
		}
	}
