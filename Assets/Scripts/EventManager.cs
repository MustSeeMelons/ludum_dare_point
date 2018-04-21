using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utility;

public class Events {
    public const string COMBINE_ITEM = "COMBINE_ITEM";
    public const string PLAYER_MOVE = "PLAYER_MOVE";
    public const string ITEM_ACTION = "ITEM_PICK_UP";
    public const string ON_CURSOR_ENTER = "ON_CURSOR_ENTER";
    public const string ITEM_UI_ADD = "ITEM_UI_ADD";
}

public class BaseEvent : UnityEvent<BaseMessage> {
}

public class BaseMessage {
}

public class MovementMessage: BaseMessage {

    public float targetLocation;
    public float distance;

    public MovementMessage(float targetLocation, float distance) {
        this.targetLocation = targetLocation;
        this.distance = distance;
    }

}

public class ItemActionMessage: BaseMessage {

    public Item item;
    public bool ignoreItem = false;

    public ItemActionMessage(Item item) {
        this.item = item;
    }

}

public class EventManager : MonoBehaviour {

    private Dictionary<string, BaseEvent> eventDictionary;
    private static EventManager eventManager;

    public void Awake() {
        if (eventManager == null) {
            eventManager = this;
        } else if (eventManager != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public static EventManager Instance {
        get {
            if (eventManager == null) {
                eventManager = FindObjectOfType<EventManager>();
                if (eventManager == null) {
                    Utils.Log("EventManager: Object not found in the scene!", LogType.ERROR);
                }
                eventManager.Init();
            }
            return eventManager;
        }
    }

    void Init() {
        Utils.Log("EventManager: Init");
        if (eventDictionary == null) {
            eventDictionary = new Dictionary<string, BaseEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction<BaseMessage> listener) {
        BaseEvent thisEvent = null;
        if (!Instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent = new BaseEvent();
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
        thisEvent.AddListener(listener);
    }

    public static void StopListening(string eventName, UnityAction<BaseMessage> listener) {
        if (eventManager != null) {
            BaseEvent thisEvent = null;
            if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
                thisEvent.RemoveListener(listener);
            }
        }
    }

    public static void TriggerEvent(string eventName, BaseMessage msg = null) {
        BaseEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            Utils.Log("EventManager: Triggering `" + eventName + "`");
            thisEvent.Invoke(msg);
        } else {
            Utils.Log("EventManager: No listeners for:  `" + eventName + "`", LogType.WARNING);
        }
    }

}
