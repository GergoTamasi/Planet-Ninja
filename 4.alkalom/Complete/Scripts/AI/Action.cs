using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {


    public abstract class Action : ScriptableObject {

        public abstract void Act(StateController stateController);

    }
}
