using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decision",fileName = "Decision")]
public abstract class Decision : ScriptableObject
{
    public abstract bool Decide(StateMachine stateMachine);
}
