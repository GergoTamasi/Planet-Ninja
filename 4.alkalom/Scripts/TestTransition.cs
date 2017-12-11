using System.Collections;
using System.Collections.Generic;
using Complete;
using UnityEngine;


[System.Serializable]
public struct TestTransition {
    public Decision Decision;
    public State TrueState;
    public State FalseState;
}
