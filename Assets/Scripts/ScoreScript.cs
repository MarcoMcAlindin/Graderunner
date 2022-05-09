using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    private GameScript gameScript;
    
    public int playerScore;
    public int enemyCount;



	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this);
        GameObject gameData = GameObject.FindWithTag("GameData");
        gameScript = gameData.GetComponent<GameScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerScore = gameScript.playerScore;
        enemyCount = gameScript.enemyCount;

    
    }
}
