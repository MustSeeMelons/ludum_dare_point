using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void OnEnable() {
        EventManager.StartListening(Events.GAME_OVER, OnGameOver);
    }

    private void OnDisable() {
        EventManager.StopListening(Events.GAME_OVER, OnGameOver);
    }

    public void OnGameOver(BaseMessage msg) {
        anim.SetBool(Definitions.IS_ANIMATING, true);
    }
}
