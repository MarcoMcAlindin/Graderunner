using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUIScript : MonoBehaviour {

    //Player Object
    public GameObject player;
    //Current value for ammo over-heat
    public float cooldownValue; 
    //me script of the player object
    public CharacterControllerScript playerScript; 

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<CharacterControllerScript>();
        cooldownValue = playerScript.cooldownValue;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        cooldownValue = playerScript.cooldownValue;
        GetComponent<Image>().color = new Color(1, (5f - cooldownValue)/5f, (5f - cooldownValue) / 5f);
    }
}
