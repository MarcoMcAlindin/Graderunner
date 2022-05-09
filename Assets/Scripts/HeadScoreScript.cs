using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadScoreScript : MonoBehaviour {
    public Canvas canvas;
    public Image activeHead;
    public Image unactiveHead;
    public int enemyCount;

    private Image[] activeStudentHeads;
    private GameScript gameScript;

    //Coroutine waits time for game script to assign enemy count to its variable.
    IEnumerator LateStart(float Time)
    {
        yield return new WaitForSeconds(Time);
        GameObject gameData = GameObject.FindWithTag("GameData");
        gameScript = gameData.GetComponent<GameScript>();
        enemyCount = gameScript.enemyCount;
        activeStudentHeads = new Image[enemyCount];
        HeadScore();
    }

    public void HeadScore()
    {
        // Generates head images based on enemy count.
        for (int x = 0; x < enemyCount; x++)
        {
            float headSpace = 45f * canvas.scaleFactor;
            Vector3 position = new Vector3(transform.position.x + x * headSpace,0,0);
            Image unHead = (Image)Instantiate(unactiveHead, gameObject.transform.position, Quaternion.identity);
            unHead.transform.SetParent(gameObject.transform, false);
            unHead.transform.position = gameObject.transform.position + position;
            Image head = (Image)Instantiate(activeHead, gameObject.transform.position, Quaternion.identity);
            activeStudentHeads[x] = head;
            head.transform.SetParent(gameObject.transform, false);
            head.transform.position = gameObject.transform.position + position;
        }

    }

   


    // Use this for initialization
    void Start () {
        StartCoroutine(LateStart(0.1f));
        



    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!gameScript) return;

        //Eliminates active student heads when enemy count decreases.
        enemyCount = gameScript.enemyCount;
        if (enemyCount < activeStudentHeads.Length)
        {
            if (gameScript.enemyCount == enemyCount)
            {
                Destroy(activeStudentHeads[gameScript.enemyCount]);
            }
        }
       
       


    }
}
