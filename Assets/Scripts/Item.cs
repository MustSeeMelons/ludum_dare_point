using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public enum ItemType {
    PICK_UP,
    ACTION
}

/// <summary>
/// Item descriptor script, should be found on iteractible objects.
/// </summary>
public class Item : MonoBehaviour {
    [Tooltip("Can we activate it, or pick it up?")]
    public ItemType itemType;
    public bool isInInventory = false;

    public void SetAsInInventory(bool isInInventory) {
        this.isInInventory = isInInventory;
    }
}
