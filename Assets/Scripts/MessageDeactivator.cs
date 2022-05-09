using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDeactivator : MonoBehaviour {

    public GameObject pickupMessage1;
    public GameObject pickupMessage2;
    public GameObject pickupMessage3;

    IEnumerator Deactivator()
    {
        yield return new WaitForSeconds(0.5f);
        pickupMessage1.SetActive(false);
        pickupMessage2.SetActive(false);
        pickupMessage3.SetActive(false);
        Object.Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(Deactivator());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
