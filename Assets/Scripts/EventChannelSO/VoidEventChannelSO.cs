using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName="Channel/Basic/Void",fileName="Void Channel")]
public class VoidEventChannelSO : ScriptableObject
{
    public UnityAction OnEventRaised;
    public void RaiseEvent()
    {
        if(OnEventRaised != null)
        {
            OnEventRaised.Invoke();
        }
        else
        {
            Debug.Log("Cannot connect to any listener.");
        }
    } 
}
