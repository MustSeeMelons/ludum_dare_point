using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

    ProximityCheck proximityCheck;

    Camera mainCamera;

    private void Awake() {
        mainCamera = Camera.main;
    }

    private void Start() {
        proximityCheck = GameObject.FindObjectOfType<ProximityCheck>();
    }

    private void Update() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            Vector3 worldPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast(worldPoint, Vector2.up, .001f);

            if (hit2d) {
                GameObject obj = hit2d.transform.gameObject;

                if (obj.tag == Tags.ITEM) {
                    Utils.Log("InputManager: Raycast hit item.");
                    if (proximityCheck.IsInProximity(obj.transform.position, 1.5f)) {

                        Item item = obj.GetComponent<Item>();

                        if (!item.isInInventory && item.isClickable) {
                            EventManager.TriggerEvent(Events.ITEM_ACTION, new ItemActionMessage(item));
                        }
                    } else {
                        EventManager.TriggerEvent(
                            Events.PLAYER_MOVE,
                            new MovementMessage(
                                obj.transform.position.x,
                                proximityCheck.proximityDistance
                            )
                        );
                    }
                } else {
                    Utils.Log("InputManager: Raycast hit something else.");
                }
                // If we would need multiple levels, we would add a tag for the stairs
                // Check if we clicked on stairs, save that to state and check once movement
                // has finished

            } else {
                Utils.Log("InputManager: Raycast hit nothing.");
                // Hit nothing, try to move there
                EventManager.TriggerEvent(Events.PLAYER_MOVE);
            }
        }
    }
}
