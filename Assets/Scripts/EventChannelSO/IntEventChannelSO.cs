using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Channel/Basic/Int", fileName = "Int Channel")]
public class IntEventChannelSO : ScriptableObject
{
    public UnityAction<int> OnEventRaised;
    public void RaiseEvent(int value)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(value);
        }
        else
        {
            Debug.Log("This event has no subscriber.");
        }
    }
}
