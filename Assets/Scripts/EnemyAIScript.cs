
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAIScript : MonoBehaviour {
    // The waypoint gameobjects that this enemy will move to, starting at 0
    public GameObject[] waypoints;

    // The current target waypoint
    private int targetWaypoint = 0;

    // The speed to move at (higher -> faster)
    public float speed = 8f;

    // Student cheer sounds
    public AudioClip studentCheer1;
    public AudioClip studentCheer2;
    public AudioClip paperCatchSound;

    // Footstep sounds
    public AudioClip footstepSound1;
    public AudioClip footstepSound2;
    
    // false = play stepsound 1, true  = play stepsound 2
    public bool footStep = false;
    // Move the enemy between waypoints
    private IEnumerator Move()
    {
        // While the enemy is active
        while (isActiveAndEnabled && speed > 0)
        {
            // Figure out the distance between the enemy and the current waypoint
            Vector3 distance = transform.position - waypoints[targetWaypoint].transform.position;

            // If the enemy is still far away from the waypoint
            if(distance.magnitude > 0.5 )
            {
                // Move towards and face the waypoint
                transform.position = Vector3.MoveTowards(transform.position, waypoints[targetWaypoint].transform.position, speed *Time.deltaTime);
                transform.right = waypoints[targetWaypoint].transform.position - transform.position;
            }
            else
            {
                // Otherwise, if the enemy is close to the waypoint, go to the next one
                targetWaypoint++;
                
            }

            // If the last waypoint has been reached, reverse the list
            if (targetWaypoint >= waypoints.Length)
            {
                targetWaypoint = 0;
                System.Array.Reverse(waypoints);
            }
            yield return null;
        }
    }

    private void Update()
    {

        foreach (GameObject waypoint in waypoints)
        {
            waypoint.transform.position = new Vector3(waypoint.transform.position.x, waypoint.transform.position.y, 0f);
        }
    }


    // Play alternative footstep sounds while moving
    public IEnumerator Footstep()
    {
        while (isActiveAndEnabled && speed > 0)
        {
            AudioSource.PlayClipAtPoint(footStep ? footstepSound1 : footstepSound2, this.transform.position);
            footStep = !footStep;
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }

    // Called when the enemy is touched by a paper projectile
    private IEnumerator EnemyDeath(Collision2D other)
    {
        // Make camera effect
        Camera.main.GetComponent<CameraEffectAnimator>().BloomEffect();

        // Stop the character walking
        this.speed = 0;

        // Start the death animation
        this.GetComponentInChildren<Animator>().SetTrigger("Dead");

        // Play one of the two student cheer noises
        AudioClip chosenClip = new AudioClip[]{ studentCheer1, studentCheer2 }[Random.Range(0, 2)];
        AudioSource.PlayClipAtPoint(chosenClip, this.transform.position);
        AudioSource.PlayClipAtPoint(paperCatchSound, this.transform.position);

        // Wait for half a second
        yield return new WaitForSeconds(0.5f);

        // Destroy the enemy, and increase the player score and decrease the enemy count in GameData
        Destroy(gameObject);
        GameObject gameData = GameObject.FindWithTag("GameData");
        GameScript gameScript = gameData.GetComponent<GameScript>();
        gameScript.playerScore += 50;
        gameScript.enemyCount--;
    }
    

    // Use this for initialization
    void Start ()
    {

        // Start the enemy at the first waypoint
        transform.position = waypoints[0].transform.position;
        StartCoroutine (Move());
        //StartCoroutine(Footstep());

	}
	
    // Draw the lines between this enemy's waypoints
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int x = 0; x < waypoints.Length - 1; x++)
        {
            Gizmos.DrawLine(waypoints[x].transform.position, waypoints[x + 1].transform.position);
        }
         
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Paper" && speed > 0)
        {
            StartCoroutine(EnemyDeath(other));
        }
    }
}
