﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class UIItemManager : MonoBehaviour {

    [Tooltip("Content child of the scroll view")]
    public GameObject scrollContent;

    public GameObject uiItemPrefab;

    private void OnEnable() {
        EventManager.StartListening(Events.ITEM_UI_ADD, OnItemUIAdd);
    }

    private void OnDisable() {
        EventManager.StopListening(Events.ITEM_UI_ADD, OnItemUIAdd);
    }

    public void OnItemUIAdd(BaseMessage msg) {
        // Adding the new UI item
        GameObject newObj = Instantiate(uiItemPrefab, new Vector3(), Quaternion.identity);
        RectTransform newRect = newObj.GetComponent<RectTransform>();
        newRect.SetParent(scrollContent.GetComponent<RectTransform>());

        ItemActionMessage castMsg = msg as ItemActionMessage;

        Item item = (msg as ItemActionMessage).item;
        if (castMsg.ignoreItem) {
            newObj.GetComponent<Image>().sprite = item.sprite;
        } else {
            SpriteRenderer rend = item.GetComponent<SpriteRenderer>();
            newObj.GetComponent<Image>().sprite = rend.sprite;
            newObj.GetComponent<ItemSlot>().SetItem(item);
        }

        // Setting item
        ItemSlot slot = newObj.GetComponent<ItemSlot>();

    }
}
