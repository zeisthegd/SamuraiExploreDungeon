using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Channel/Basic/String", fileName = "String Channel")]
public class StringEventChannelSO : ScriptableObject
{
    public UnityAction<string> OnEventRaised;
    public void RaiseEvent(string text)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(text);
        }
        else
        {
            Debug.Log("This event has no subscriber.");
        }
    }
}