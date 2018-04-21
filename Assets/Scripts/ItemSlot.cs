using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDragHandler
{
    public Transform TopParent;
    Transform OrginalParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        OrginalParent = transform.parent;
        transform.parent = TopParent;

        transform.position = eventData.position;
        GetComponent<Canvas>().sortingOrder += 7;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {


        transform.SetParent(OrginalParent);//to add back and refresh grid layout
        GetComponent<Canvas>().sortingOrder -= 7;
    }
}
