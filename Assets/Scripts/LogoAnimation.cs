using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoAnimation : MonoBehaviour {
    public Animator animator;
    public AudioClip sound1;
    public AudioClip sound2;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fadeout") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void PlaySoundOne()
    {
        AudioSource.PlayClipAtPoint(sound1, new Vector3(0, 0, 0));
    }

    public void PlaySoundTwo()
    {
        AudioSource.PlayClipAtPoint(sound2, new Vector3(0, 0, 0));
    }
}
