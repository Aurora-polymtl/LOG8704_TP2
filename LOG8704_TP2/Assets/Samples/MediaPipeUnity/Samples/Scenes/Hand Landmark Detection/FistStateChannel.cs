using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FistStateChannel", menuName = "Scriptable Objects/FistStateChannel")]
public class FistStateChannel : ScriptableObject
{
    public UnityAction<HandState> OnEventRaised;

    public void RaiseEvent(HandState state)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(state);
    }
}

public enum HandState
{
    Open,
    Closed,
    Partial,
    None
}
