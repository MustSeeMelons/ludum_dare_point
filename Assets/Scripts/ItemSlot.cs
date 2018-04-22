using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

public class ItemSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

    Item item;
    Vector3 startLocation;

    public void SetItem(Item item) {
        this.item = item;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        startLocation = transform.position;
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Utils.Log("ItemSlot: OnEndDrag");
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(eventData.position);
        RaycastHit2D hit2d = Physics2D.Raycast(worldPoint, Vector2.left, .001f);

        if (hit2d) {
            GameObject obj = hit2d.transform.gameObject;

            Utils.Log("ItemSlot: Hit " + obj.tag);

            if (obj.tag == Tags.ITEM) {
                Item hitItem = obj.GetComponent<Item>();
                
                if (hitItem.actionItemId == item.itemId) {
                    EventManager.TriggerEvent(Events.ITEM_ACTION, new ItemActionMessage(hitItem));
                    Destroy(this.gameObject);
                } else {
                    Reset();
                }
            }
        } else {
            Reset();
        }
    }

    public void Reset() {
        transform.position = startLocation;
    }
}
