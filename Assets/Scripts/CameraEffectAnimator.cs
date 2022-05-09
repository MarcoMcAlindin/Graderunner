using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraEffectAnimator : MonoBehaviour {
    public PostProcessingProfile profile;

    public float chromaticAbbrToAnimate;

    Animator animator;
	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
	}
	
    // Do the little bloom effect thing
    public void BloomEffect()
    {
        animator.SetTrigger("ChromaticBlur");
    }

	// Update is called once per frame
	void Update () {
        ChromaticAberrationModel.Settings settings = profile.chromaticAberration.settings;
        settings.intensity = chromaticAbbrToAnimate;
        profile.chromaticAberration.settings = settings;
	}
}
