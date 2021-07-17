using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action", fileName = "Action")]
public abstract class Action : ScriptableObject
{
    public abstract void Act(StateMachine stateMachine);
}
