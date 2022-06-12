using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Game Event")]
public class GameEvent : BaseScriptObject
{
    [SerializeField]
    private List<GameEventListener> _listeners;

    private void OnEnable()
    {
        _listeners = new List<GameEventListener>();
    }

    public void Raise()
    {
        for(int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listenerToBeAdded)
    {
        _listeners.Insert(0, listenerToBeAdded);
    }

    public void UnregisterListener(GameEventListener listenerToBeRemoved)
    {
        _listeners.Remove(listenerToBeRemoved);
    }
}
