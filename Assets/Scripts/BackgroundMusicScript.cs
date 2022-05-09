using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicScript : MonoBehaviour {
	void Start () {
        DontDestroyOnLoad(this);
        this.GetComponent<AudioSource>().PlayDelayed(5f);
    }
}
