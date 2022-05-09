using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickupScript : MonoBehaviour {

    //This Script is for the Bonus Points Pickup Script

    public int bonusPoints = 1;
    public GameObject dataObject;
    public GameScript dataScript;
    public GameObject pickupMessage;
    public GameObject scoreText;


    IEnumerator BonusMessage()
    {
        scoreText.SetActive(false);
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
        scoreText.SetActive(true);
        Object.Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        //Assigns the object and script variables to Score containing object
        dataObject = GameObject.Find("GameData");
        if (dataObject != null)
        {
            dataScript = dataObject.GetComponent<GameScript>();
        }

        scoreText = GameObject.Find("Score text");
        pickupMessage = GameObject.FindWithTag("ScoreMessage");
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //On collision with the player, it sets the score to itself+bonus
        if (other.gameObject.name == "Player")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine(BonusMessage());
            dataScript.playerScore = (dataScript.playerScore + bonusPoints);
        }
    }
}
