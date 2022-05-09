using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public GameObject[] topLeftCorners;
    public GameObject[] topRightCorners;
    public GameObject[] bottomLeftCorners;
    public GameObject[] bottomRightCorners;

    // Use this for initialization
    void Start () {
        Generate();
	}
	
    public void Generate()
    {
        // Pick one corner from the set of corners and make it active
        topLeftCorners[Random.Range(0, topLeftCorners.Length)].SetActive(true);
        topRightCorners[Random.Range(0, topRightCorners.Length)].SetActive(true);
        bottomLeftCorners[Random.Range(0, bottomLeftCorners.Length)].SetActive(true);
        bottomRightCorners[Random.Range(0, bottomRightCorners.Length)].SetActive(true);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
