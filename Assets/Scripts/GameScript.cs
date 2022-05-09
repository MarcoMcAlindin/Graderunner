using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour {

    public float timer = 30f; // How much time is left on the counter
    public float timeBetweenScenes = 0.5f; // How long to wait before transitioning scenes
    public float startTimerSoundAtSeconds = 10f; // At which time to start the audible clicking down
    public int enemyCount; // Count of enemies on the current map
    public int playerScore; // Player's current score
   

    public bool gameOn = true; // Whether the game is playing or not
    
    public Text timeText; // The time text to update in the UI
    public GameObject timeTextImage;
    public GameObject fadeOut;
    Animator timeTextAnimator; // The animator for the timer text
    public Text playerScoreText; // The player score text to update in the UI
    public Image black;
    public string gameOverScene = "GameOverScene";
    public string winScene = "WinScene";


    // Sounds
    public AudioClip clockTick1;
    public AudioClip clockTick2;
    private bool clockTick = false;

    GameObject[] enemies;

    


    public IEnumerator EndScene(float t, Image black, string scene)
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

   

    public void SoundState()
    {
        GameObject soundObject = GameObject.FindGameObjectWithTag("Sound");
        SoundScript soundScript = soundObject.GetComponent<SoundScript>();
        if (soundScript.soundState == true)
        {
            
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera.GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera.GetComponent<AudioListener>().enabled = false;
        }
    }




    // Use this for initialization
    void Start ()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
        timeTextAnimator = timeTextImage.GetComponent<Animator>();
        StartCoroutine(TimerSound());
	}
	

	// Update is called once per frame
	void Update ()
    {
        if (enemyCount !=0 && timer <= 0.0f )
        {
            timeText.text = "00:00";
            StartCoroutine(EndScene(0.7f,black,gameOverScene));
            gameOn = false;
        }
        else if (enemyCount == 0 && timer > 0.0f)
        {
            StartCoroutine(EndScene(0.7f, black,winScene));
            gameOn = false;
        }
        else
        {
            timer -= Time.deltaTime;
            string minutes = Mathf.Floor((timer / 60)).ToString().PadLeft(2, '0');
            string seconds = Mathf.Floor((timer % 60)).ToString().PadLeft(2, '0');
            timeText.text = minutes + ":" + seconds;
        }

        playerScoreText.text = "Player Score : " + playerScore.ToString();
        SoundState();
	}

    // Handles the timer sound and animation once the clock has ticked below a certain level
    private IEnumerator TimerSound()
    {
        while (timer > 0)
        {
            if (timer < startTimerSoundAtSeconds)
            {
                AudioSource.PlayClipAtPoint(clockTick ? clockTick1 : clockTick2, Camera.main.transform.position);
                clockTick = !clockTick;
                timeTextAnimator.SetTrigger("ClockAnimate");
            }

            yield return new WaitForSeconds(1f);
        }

        yield return null;
    }
}
