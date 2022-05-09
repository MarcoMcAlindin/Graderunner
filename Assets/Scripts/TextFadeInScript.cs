using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeInScript : MonoBehaviour {


    

    public IEnumerator TextFade(float t, Text gameOver)
    {
        gameOver.color = new Color(gameOver.color.r, gameOver.color.g, gameOver.color.b, 0);
        while (gameOver.color.a < 1.0f)
        {
            gameOver.color = new Color(gameOver.color.r, gameOver.color.g, gameOver.color.b, gameOver.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(TextFade(6f, GetComponent<Text>()));
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
