using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clock : MonoBehaviour, IDragHandler
{
    public float CurrentTime;

    public void OnDrag(PointerEventData eventData)
    {
        //// Vector3.up makes it move in the world x/z plane.
        //Plane plane = new Plane(Vector3.up, transform.position);
        //Ray ray = eventData.pressEventCamera.ScreenPointToRay(eventData.position);
        //float distamce;
        //if (plane.Raycast(ray, out distamce))
        //{
        //    transform.position = ray.origin + ray.direction * distamce;
        //}

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
