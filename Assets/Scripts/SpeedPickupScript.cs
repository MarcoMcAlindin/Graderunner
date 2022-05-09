using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickupScript : MonoBehaviour {


    //This Script is for the Bonus Time Pickup Script

    public float bonusSpeed = 2.0f;
    public GameObject player;
    public GameObject pickupMessage;
    public CharacterControllerScript controllerScript;


    IEnumerator SpeedUp()
    {
        controllerScript.speed = (controllerScript.speed + bonusSpeed);
        pickupMessage.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        pickupMessage.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        pickupMessage.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        pickupMessage.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        pickupMessage.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        pickupMessage.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        controllerScript.speed = (controllerScript.speed - bonusSpeed);
        Object.Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        //Assigns the object and script variables to Score containing object
        player = GameObject.Find("Player");
        if (player != null)
        {
            controllerScript = player.GetComponent<CharacterControllerScript>();
        }
        pickupMessage = GameObject.FindWithTag("SpeedMessage");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //On collision with the player, it sets the score to itself+bonus
        if (other.gameObject.name == "Player")
        {
            StartCoroutine(SpeedUp());
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
