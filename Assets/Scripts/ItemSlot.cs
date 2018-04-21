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
        transform.position = startLocation;
    }
}
