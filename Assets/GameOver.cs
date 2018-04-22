using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    public GameObject overImage;
    Animator anim;
    Animator pAnim;
    GameObject player;

    private void Awake() {
        overImage.SetActive(false);
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        pAnim = player.GetComponent<Animator>();
    }

    private void OnEnable() {
        EventManager.StartListening(Events.GAME_OVER, OnGameOver);
    }

    private void OnDisable() {
        EventManager.StopListening(Events.GAME_OVER, OnGameOver);
    }

    public void OnGameOver(BaseMessage msg) {
        anim.SetBool(Definitions.IS_ANIMATING, true);
        StartCoroutine("fly");
    }

    public IEnumerator fly() {
        yield return new WaitForSeconds(2);
        pAnim.SetBool("fly", true);
        LeanTween.moveLocalY(player, 10, 2f);
        StartCoroutine("image");
    }

    public IEnumerator image() {
        yield return new WaitForSeconds(2);
        overImage.SetActive(true);
    }
}
