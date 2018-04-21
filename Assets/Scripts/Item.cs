using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public enum ItemType {
    NONE,     // Dumymy
    PICK_UP,  // Item goes to inventory
    ACTION,   // Some action is called
    TRANSFORM // New item to inventory

}

/// <summary>
/// Item descriptor script, should be found on iteractible objects.
/// </summary>
public class Item : MonoBehaviour {
    [Tooltip("Can we activate it, or pick it up?")]
    public ItemType itemType;
    public bool isInInventory = false;
    public bool hasTransformed = false;

    public GameObject transformPrefab;

    public Sprite sprite;

    public void SetAsTransformed() {
        hasTransformed = true;
    }

    public void SetAsInInventory(bool isInInventory) {
        this.isInInventory = isInInventory;
    }
}
