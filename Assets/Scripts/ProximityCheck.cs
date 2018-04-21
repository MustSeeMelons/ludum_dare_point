using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

// On what the pointer is now?
public enum OnPointerType {
    FLOOR,
    ITEM
}

/// <summary>
/// Utility class for calculating the distance between two objects. Dont like this class though.
/// </summary>
public class ProximityCheck : MonoBehaviour {

    public float proximityDistance = .4f;
    GameObject player;
    Camera mainCamera;

    public Texture2D walkIcon;
    public Texture2D grabIcon;
    public Texture2D errorIcon;

    private void Start() {
        player = GameObject.FindGameObjectWithTag(Definitions.PLAYER);
        mainCamera = Camera.main;
    }

    public bool IsInProximity(Vector3 position, float proximityMod = 1f) {
        return Mathf.Abs(position.x - player.transform.position.x) <= proximityDistance * proximityMod;
    }

    // TODO should not be done every frame, but oh well
    private void Update() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit2d = Physics2D.Raycast(worldPoint, Vector2.up, .001f);

        if (hit2d) {
            string tag = hit2d.transform.gameObject.tag;
            if (tag == Tags.ITEM) {
                // Enlarging the cursor radius a bit
                if (IsInProximity(worldPoint, 1.1f)) {
                    Cursor.SetCursor(grabIcon, new Vector2(25, 0), CursorMode.Auto);
                }
            } else if (tag == Tags.PLAYER) {
                Cursor.SetCursor(errorIcon, new Vector2(25, 0), CursorMode.Auto);
            }
        } else {
            Cursor.SetCursor(walkIcon, new Vector2(32, 0), CursorMode.Auto);
        }
    }
}
