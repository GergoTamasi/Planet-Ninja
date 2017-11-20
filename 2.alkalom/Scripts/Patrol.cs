using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{

	//public Waypoint[] Waypoints;
	public Transform[] Waypoints;

	[Range(0.1f,0.8f)]
	public float PathRes;

	[Range(1f,10f)]
	public float MoveSpeed;

	[Range(1f,10f)]
	public float WaypointCheckRadius;

	
	private int _currentIndex=0;
	private Transform _target;

	private Vector3 _smoothMoveAmount;
	private Vector3 _smoothVelocity;
	private bool _isMoving;

	private GravityBody _gb;
	private Rigidbody _rb;


	private void Awake()
	{
		_gb = gameObject.GetComponent<GravityBody>();
		_rb = gameObject.GetComponent<Rigidbody>();
		
		StartMove();
	}


	public void NextWP()
	{
		_currentIndex = (_currentIndex + 1) % Waypoints.Length;
		_target = Waypoints[_currentIndex];

	}

	public bool CheckIfReachWaypoint()
	{

		RaycastHit[] hits;
		hits = Physics.SphereCastAll(transform.position, WaypointCheckRadius, Vector3.forward);
		if (hits.Length > 0)
		{
			for (int i = 0; i < hits.Length; i++)
			{
				if (hits[i].collider.gameObject.name == _target.name)
				{
					return true;
				}
			}
		}
		return false;

	}

	public void StartMove()
	{
		if(_target==null)
		{
			_currentIndex = 0;
			_target = Waypoints[_currentIndex];
		}
		StartCoroutine(Moving());

	}



	private IEnumerator Moving()
	{
		_isMoving = true;
		while (_isMoving)
		{
			//1. A és B pont közötti vektor
			
			
			Vector3 distance = _target.position - gameObject.transform.position;
			Vector3 innerPoint = transform.position + distance.normalized * (distance.magnitude * PathRes);
			Vector3 nextPoint = _gb.Planet.transform.position + (innerPoint - _gb.Planet.transform.position).normalized *
			                    (transform.position - _gb.Planet.transform.position).magnitude;
			
			Vector3 moveDirection = (nextPoint - transform.position).normalized;
			
			var calculatedMoveAmount = moveDirection * MoveSpeed;
			yield return new WaitForFixedUpdate();
			
			
			
			_smoothMoveAmount = Vector3.SmoothDamp(_smoothMoveAmount, calculatedMoveAmount, ref _smoothVelocity, .15f);
			var localMove = _smoothMoveAmount * Time.fixedDeltaTime;
			_rb.MovePosition(transform.position + localMove);

			if (CheckIfReachWaypoint())
			{
				NextWP();
			}
			
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position,WaypointCheckRadius);
	}
}
