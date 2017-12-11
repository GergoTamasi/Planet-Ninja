
using UnityEngine;


namespace Complete {

    public abstract class Decision : ScriptableObject {


        public abstract bool Decide(StateController stateController);

    }
}
