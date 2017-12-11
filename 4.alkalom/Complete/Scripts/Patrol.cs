using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Complete {
    [Serializable]
    [RequireComponent(typeof(GravityBody))]
    public class Patrol : MonoBehaviour {

        [System.Serializable]
        public struct WaypointInfo {
            public Transform CurrentPoint;
            public int CurrentWaypointIndex;
        }

        public List<Transform> Waypoints = new List<Transform>();
      
        [Range(2, 10)] public float MoveSpeed;
        [Range(2, 10)] public float RotateSpeed;
        [Range(0.1f, 20f)] public float WaypointCastSphereRadius;
        [Range(0.1f, 20f)] public float EnemyLookCastSphereRadius;
        [Range(20, 160)] public float EnemyFieldOfView;


        public Transform child;

        private Vector3 _smoothMoveVelocity;
        private Vector3 _smoothMoveAmount;
        private Vector3 _moveDirection;
        private Vector3 _localMove;

       
        private Vector3 innerPoint;
        private Vector3 nextPoint;

        private Rigidbody _rbody;
        private GravityBody _gbody;

        private bool _moving;
        public Transform MovingTarget;

        public WaypointInfo Waypointinfo;
        public Transform ChasingTarget;





        void Awake() {
            _rbody = GetComponent<Rigidbody>();
            _gbody = GetComponent<GravityBody>();

        }


        public void ResumeMoving() {

            if (!_moving) {
                StartCoroutine(Moving());
            }
        }

        public void StopMoving() {
            _moving = false;
        }

        public void SetNextWaypoint() {
            Waypointinfo.CurrentWaypointIndex = (Waypointinfo.CurrentWaypointIndex + 1) % Waypoints.Count;
            Waypointinfo.CurrentPoint = Waypoints[Waypointinfo.CurrentWaypointIndex];
        }


        public void ResumeChasing() {
            MovingTarget = ChasingTarget;
            ResumeMoving();

        }

        public void ResumePartolling() {
            if (!Waypointinfo.CurrentPoint) {
                Waypointinfo.CurrentPoint = Waypoints[0];
            }
            MovingTarget = Waypointinfo.CurrentPoint;
            ResumeMoving();

        }

        public bool CheckIfReachWayPoint() {
            return CheckIfCollideWithObject(WaypointCastSphereRadius, LayerMask.NameToLayer("Waypoint"),
                Waypointinfo.CurrentPoint.gameObject);
        }



        private bool CheckIfCollideWithObject(float checkRadius, LayerMask layerMaskToCheck, GameObject gameObject) {
            var result = Physics.OverlapSphere(transform.position, checkRadius, layerMaskToCheck);
            if (result.Length > 0) {
                for (int i = 0; i < result.Length; i++) {
                    if (result[i].gameObject == gameObject) {
                        return true;
                    }
                }
            }
            return false;
        }






        private IEnumerator Moving() {
            _moving = true;
            while (_moving) {
                
                Debug.DrawLine(transform.position,MovingTarget.transform.position,Color.red);
                //var targetRotation = Quaternion.FromToRotation(child.transform.forward, _localMove) * child.transform.rotation;
                //child.transform.rotation = Quaternion.Slerp(child.transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
                var magnitude = (MovingTarget.transform.position - transform.position).magnitude;
                innerPoint =transform.position + ((MovingTarget.transform.position- transform.position).normalized * (magnitude*0.30f));
                nextPoint =_gbody.Planet.transform.position+ ((innerPoint - _gbody.Planet.transform.position).normalized *
                                (_gbody.Planet.transform.position - transform.position).magnitude);
                var moveDirection = (nextPoint-transform.position).normalized;
                Debug.DrawRay(transform.position,moveDirection*5,Color.black);
                var calculatedMoveAmount = moveDirection * MoveSpeed;
                _smoothMoveAmount = Vector3.SmoothDamp(_smoothMoveAmount, calculatedMoveAmount, ref _smoothMoveVelocity,.15f);
                yield return new WaitForFixedUpdate();
                _localMove = _smoothMoveAmount * Time.fixedDeltaTime;
                _rbody.MovePosition(_rbody.position + _localMove);
               
                
            }
        }
    


    public Vector3 DirectionFromAngle(float angle) {
            return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
        
    private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(transform.position, WaypointCastSphereRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, EnemyLookCastSphereRadius);
            Gizmos.color = Color.cyan;
            /*Vector3 viewAngleA = transform.position +
                                 DirectionFromAngle(-EnemyFieldOfView / 2) * EnemyLookCastSphereRadius;
            Vector3 viewAngleB =
                transform.position + DirectionFromAngle(EnemyFieldOfView / 2) * EnemyLookCastSphereRadius;

            Vector3 rotatedAngleA = child.rotation * (viewAngleA - transform.position) + transform.position;
            Vector3 rotatedAngleB = child.rotation * (viewAngleB - transform.position) + transform.position;
            

            Gizmos.DrawLine(transform.position, rotatedAngleA);
            Gizmos.DrawLine(transform.position, rotatedAngleB);
            */
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(nextPoint,0.5f);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(innerPoint,0.5f);

        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < Waypoints.Count - 1; i++)
            {
                Gizmos.DrawLine(Waypoints[i].position, Waypoints[i + 1].position);
            }
            Gizmos.DrawLine(Waypoints[0].position, Waypoints[Waypoints.Count - 1].position);
        }
    }


}
