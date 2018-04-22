using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public enum ItemType {
    NONE,     // Dumymy
    PICK_UP,  // Item goes to inventory
    ACTION,   // Some action is called
    TRANSFORM, // New item to inventory
    TRIGGER
}

/// <summary>
/// Item descriptor script, should be found on iteractible objects.
/// </summary>
public class Item : MonoBehaviour {
    public string itemId;
    [Tooltip("Can we activate it, or pick it up?")]
    public ItemType itemType;
    public ItemType nextItemType = ItemType.NONE;
    public bool isInInventory = false;
    public bool hasTransformed = false;
    public bool isClickable = true;
    public string actionItemId;
    public string nextActionItemId;
    public string triggerEvent;

    public GameObject transformPrefab;

    public Sprite sprite;

    public void TickType() {
        itemType = nextItemType;
    }

    public void TIckActivation() {
        actionItemId = nextActionItemId;
    }

    public void SetAsTransformed() {
        hasTransformed = true;
    }

    public void SetAsInInventory(bool isInInventory) {
        this.isInInventory = isInInventory;
    }
}
