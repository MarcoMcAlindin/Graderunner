using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePickupScript : MonoBehaviour {

    //This Script is for the Bonus Time Pickup Script

    public float bonusTime = 5.0f;
    public GameObject dataObject;
    public GameScript dataScript;
    public GameObject pickupMessage;
    public GameObject timeText;

    IEnumerator BonusMessage()
    {
        timeText.SetActive(false);
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
        timeText.SetActive(true);
        Object.Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        //Assigns the object and script variables to Time containing object
        dataObject = GameObject.Find("GameData");
        if (dataObject != null)
        {
            dataScript = dataObject.GetComponent<GameScript>();
        }

        pickupMessage = GameObject.FindWithTag("TimeMessage");
        timeText = GameObject.Find("Timer Text");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //On collision with the player, it sets the time to itself+bonus
        if (other.gameObject.name == "Player")
        {
            dataScript.timer = (dataScript.timer + bonusTime);
            StartCoroutine(BonusMessage());
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
