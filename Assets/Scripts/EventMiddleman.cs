using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventMiddleman : MonoBehaviour
{
    [Serializable]
    public struct EventPair
    {
        public string name;
        public UnityEvent action;
    }
    [SerializeField]
    private List<EventPair> _ActionList;

    private Dictionary<string, UnityEvent> _Actions;

    void Awake()
    {
        _Actions = new Dictionary<string, UnityEvent>();
        foreach (EventPair ep in _ActionList)
        {
            _Actions[ep.name] = ep.action;
        }
    }

    public void CallEvent(string name)
    {
        _Actions[name].Invoke();
    }
}
