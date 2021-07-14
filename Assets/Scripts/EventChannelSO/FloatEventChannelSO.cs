using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Channel/Basic/Float", fileName = "Float Channel")]
public class FloatEventChannelSO : ScriptableObject
{
    public UnityAction<float> OnEventRaised;
    public void RaiseEvent(float value)
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
