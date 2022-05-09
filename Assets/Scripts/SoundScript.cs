using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundScript : MonoBehaviour {


    public bool soundState;

    public Sprite soundOn;
    public Sprite soundOff;

    public void SoundState()
    {
        if (soundState == true)
        {
            soundState = false;
            GetComponent<Image>().sprite = soundOff;
            
        }
        else
        {
            soundState = true;
            GetComponent<Image>().sprite = soundOn;
            
        }
    }



	// Use this for initialization
	void Start ()
    {
        soundState = true;
    } 
        
		
	
}
