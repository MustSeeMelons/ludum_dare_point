using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioManager instance;

    AudioSource source;



    void Awake() {
        if (instance == null) {
            instance = this;
            source = GetComponent<AudioSource>();
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

}
