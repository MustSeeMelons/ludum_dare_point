using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public float itemPickupScaleOutTime = .2f;

    // Will a list do?
    List<Item> itemList = new List<Item>();

    private void OnEnable() {
        EventManager.StartListening(Events.ITEM_ACTION, OnItemAction);
    }

    private void OnDisable() {
        EventManager.StopListening(Events.ITEM_ACTION, OnItemAction);
    }

    void OnItemAction(BaseMessage msg) {
        Item item = (msg as ItemActionMessage).item;

        switch (item.itemType) {
            case ItemType.PICK_UP:
                item.SetAsInInventory(true);
                itemList.Add(item);
                LeanTween.scale(item.gameObject, new Vector3(0, 0, 0), itemPickupScaleOutTime);
                EventManager.TriggerEvent(Events.ITEM_UI_ADD, new ItemActionMessage(item));
                break;
            case ItemType.ACTION:
                // TODO trigger predefined action
                break;
        }
    }
}
