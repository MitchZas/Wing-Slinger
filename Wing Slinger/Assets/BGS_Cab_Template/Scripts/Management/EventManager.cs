using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityObjectEvent : UnityEvent<GameObject> { }

public enum EventTypes
{
    ENEMY_DIED = 0,
    PLAYER_DIED = 1,
    SCORED_POINTS = 2,
    PLAYER_WON = 3,
    PLAYER_SPAWNED = 4
}

public class EventManager : Singleton<EventManager>
{
    private Dictionary<string, UnityEvent> eventDictionary;
    private Dictionary<EventTypes, UnityObjectEvent> objectEventDictionary;
    private static EventManager eventManager;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
        if (objectEventDictionary == null)
        {
            objectEventDictionary = new Dictionary<EventTypes, UnityObjectEvent>();
        }
    }
    
    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StartListening(EventTypes eventType, UnityAction<GameObject> listener)
    {
        UnityObjectEvent thisEvent = null;
        if (Instance.objectEventDictionary.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityObjectEvent();
            thisEvent.AddListener(listener);
            Instance.objectEventDictionary.Add(eventType, thisEvent);
        }
    }
    public static void StopListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }
    public static void StopListening(EventTypes e, UnityAction<GameObject> listener)
    {
        UnityObjectEvent thisEvent = null;
        if (Instance.objectEventDictionary.TryGetValue(e, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }
    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
    public static void TriggerEvent(EventTypes e, GameObject invoker)
    {
        UnityObjectEvent thisEvent = null;
        if (Instance.objectEventDictionary.TryGetValue(e, out thisEvent))
        {
            thisEvent.Invoke(invoker);
        }
    }
}
