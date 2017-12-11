using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {



    public class StateController : MonoBehaviour {

        public State CurrentState;
        public State RemainState;

        private Patrol _patrol;

        public Patrol Patrol => _patrol;

        private bool _aiIsActive;



        void Awake() {
            _patrol = GetComponent<Patrol>();
            _aiIsActive = true;
        }


        private void Update() {
            if (!_aiIsActive) {
                return;
            }
            CurrentState.UpdateState(this);

        }

        public void TransitionToState(State nextState) {
            if (nextState != RemainState) {
                CurrentState = nextState;
            }
        }
    }
}
