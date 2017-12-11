using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Complete {

    public class PlanetAttractor : MonoBehaviour {

        public float Gravitiy = -9.8f;

        public void Attract(Rigidbody body) {

            var gravityUp = (body.position - transform.position).normalized;
            var localUp = body.transform.up;

            body.AddForce(gravityUp * Gravitiy);

            body.rotation = Quaternion.FromToRotation(localUp, gravityUp) * body.rotation;
        }

    }
}
