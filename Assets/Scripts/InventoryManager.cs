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

    Animator animator = null;

    private void Awake() {
        animator = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<Animator>();
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
                Animator anim = item.GetComponent<Animator>();

                if (anim) {
                    anim.SetBool(Definitions.IS_ANIMATING, true);
                    item.GetComponent<Collider2D>().enabled = false;
                }

                break;
            case ItemType.TRANSFORM:
                if (item.hasTransformed) {
                    return;
                }
                item.SetAsTransformed();
                GameObject obj = Instantiate(item.transformPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                Item newItem = obj.GetComponent<Item>();

                // HACK Disable rendered of the newly created obj..
                obj.GetComponent<SpriteRenderer>().enabled = false;

                newItem.SetAsInInventory(true);

                itemList.Add(newItem);

                ItemActionMessage newMsg = new ItemActionMessage(newItem) {
                    ignoreItem = false
                };

                EventManager.TriggerEvent(Events.ITEM_UI_ADD, newMsg);
                break;
            case ItemType.TRIGGER:
                EventManager.TriggerEvent(Events.GAME_OVER);
                break;
        }
        animator.SetBool("isPickup", true);

        item.TickType();
        item.TIckActivation();
    }
}
