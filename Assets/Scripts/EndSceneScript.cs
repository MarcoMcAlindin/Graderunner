using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneScript : MonoBehaviour {

    public Text gameOver;
    public Text score;
    public Image black;
    public GameObject fadeOut;
    public string mainMenuScene = "MainMenu";

    private ScoreScript scoreScript;

    public void MainMenu()
    {
        StartCoroutine (SceneFadeOut(0.7f,black,mainMenuScene));
    }

    public IEnumerator SceneFadeOut(float t, Image black,string scene)
    {
        fadeOut.SetActive(true);
        black.color = new Color(black.color.r, black.color.g, black.color.b, 0);
        while (black.color.a < 1.0f)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, black.color.a + (Time.deltaTime / t));
            yield return null;
        }
        SceneManager.LoadScene(scene, LoadSceneMode.Single);

    }

    // Use this for initialization
    void Start ()
    {
        GameObject scoreObject = GameObject.FindWithTag("ScoreObject");
        scoreScript = scoreObject.GetComponent<ScoreScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (scoreScript.enemyCount == 0)
        {
            score.gameObject.SetActive(true);
            score.text = "player score : " + scoreScript.playerScore.ToString();
        }
        else
        {
            gameOver.gameObject.SetActive(true);
            gameOver.text = "game over";
        }
    }
}
