using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {


    [CreateAssetMenu(menuName = "AI/State")]
    public class State : ScriptableObject {

        public Action[] Actions;
        public Transition[] Transitions;
        public TestTransition[] TestTr;

        public void UpdateState(StateController stateController) {
            DoActions(stateController);
            CheckTransitions(stateController);

        }

        private void DoActions(StateController stateController) {


            for (int i = 0; i < Actions.Length; i++) {
                Actions[i].Act(stateController);
            }
        }

        private void CheckTransitions(StateController stateController) {
            for (int i = 0; i < Transitions.Length; i++) {
                var succeeded = Transitions[i].Decision.Decide(stateController);
                if (succeeded) {
                    stateController.TransitionToState((Transitions[i].trueState));
                }
                else {
                    stateController.TransitionToState((Transitions[i].falseState));
                }
            }
        }
    }
}