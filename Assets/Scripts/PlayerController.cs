using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class PlayerController : MonoBehaviour {

    GameObject player;
    Animator anim;
    Camera mainCamera;

    [Tooltip("Lower is faster")]
    public float movementSpeed;

    LTDescr movementTween = null;
    bool facingRight = true;

    private void Awake() {
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag(Definitions.PLAYER);
        anim = player.GetComponent<Animator>();
    }

    private void OnEnable() {
        EventManager.StartListening(Events.PLAYER_MOVE, OnPlayerMove);
    }

    private void OnDisable() {
        EventManager.StopListening(Events.PLAYER_MOVE, OnPlayerMove);
    }

    private void OnPlayerMove(BaseMessage msg) {
        anim.SetBool("isPickup", false);
        if (movementTween != null) {
            LeanTween.cancel(movementTween.id);
        }

        float bufferDist = 0;
        float targetLocation = 0;

        if (msg != null) {
            bufferDist = (msg as MovementMessage).distance;
            targetLocation = (msg as MovementMessage).targetLocation;
        } else {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPoint = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.nearClipPlane));
            targetLocation = worldPoint.x;
        }

        float time = Mathf.Abs(player.transform.position.x - targetLocation) * movementSpeed;

        if (targetLocation < -7.82f) {
            targetLocation = -7.82f;
        }

        if (targetLocation > 7.46f) {
            targetLocation = 7.46f;
        }

        if (targetLocation < player.transform.position.x && facingRight) {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            facingRight = false;
        } else if(targetLocation > player.transform.position.x && !facingRight) {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            facingRight = true;
        }

        anim.SetBool("isWalking", true);

        // TODO buffer dist may not work properly
        movementTween = LeanTween.moveX(player, targetLocation - bufferDist, time).setOnComplete(() => {
            movementTween = null;
            anim.SetBool("isWalking", false);
        });
    }
}
