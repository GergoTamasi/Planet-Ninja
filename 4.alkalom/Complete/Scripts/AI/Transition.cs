using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {


    [System.Serializable]
    public class Transition {


        public Decision Decision;
        public State trueState;
        public State falseState;

    }
}
