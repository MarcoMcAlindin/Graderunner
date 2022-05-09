using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour {
    // Speed of character
	public float speed = 15.0f;

    // Force that the paper is fired with
    public float fireForce = 30f;

    // The ammunition that gets fired when clicked
    public GameObject paperPrefab;

    // The animator of the character sprite
    public Animator animator;


    // Sounds
    public AudioClip letsGoSound;
    public AudioClip fireSound;

    // The global GameScript, is assigned at Start()
    private GameScript gameScript;

    //Ammo cooldown value
    public float cooldownValue = 0.0f;

    //Boolean to check if cooldown wait is active
    public bool waitCheck;

    Vector3 movementDirection = Vector3.up;

    // How fast to fire 
    public float fireRate = 0.2f;

    // How long in seconds since the last paper was fired
    private float sinceLastFire = 0f;

    bool mouseIsHeldDown = false;
    Vector2 mouseStartPosition;
    float timeSinceMouseStartedBeingHeldDown = 0;

    // How long to wait between a touch starting and ending before discounting it as a swipe (in s)
    public float timeBeforeExpiringSwipe = 1f;

    // How long to wait before expiring a click
    public float timeBeforeExpiringClick = 0.2f;

    public float clickRadius = 5f; // If a cursor lets go within this value of the starting position, it is a click

    public bool mobileControls = true;

    //Coroutine to wait and drop cooldown
    IEnumerator CooldownWait()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        cooldownValue = 4.0f;
        waitCheck = false;
    }


    // Called every update, calculates the player movement
	public void CharacterMovement()
    {
        if (mobileControls) return;
        // Set the movement according to the key pressed
        if (Input.GetKey(KeyCode.W)) movementDirection = Vector3.up;
        if (Input.GetKey(KeyCode.S)) movementDirection = Vector3.down;
        if (Input.GetKey(KeyCode.D)) movementDirection = Vector3.right;
        if (Input.GetKey(KeyCode.A)) movementDirection = Vector3.left;

        

        if (Input.GetButton("Fire1")) Shoot();
    }

    // Capture mobile input
    public void MobileInput()
    {
        if (!mobileControls) return;

        // If the mouse is held down
        if (mouseIsHeldDown)
        {
            // But it has now been let go
            if (!Input.GetMouseButton(0))
            {
                // We'll assume this is a valid swipe and set it to false in a bit if proven otherwise
                bool validSwipe = true;

                // Update the mouse state
                mouseIsHeldDown = false;

                // Check to see if the swipe has expired (the specified time between START and STOP has passed)
                if (timeSinceMouseStartedBeingHeldDown > timeBeforeExpiringSwipe) validSwipe = false;



                // Get a vector between the start and end positions
                Vector2 mouseEndPosition = Input.mousePosition;
                Vector2 swipeVector = mouseEndPosition - mouseStartPosition;
                
                // Check to make sure a good enough distance has been travelled
                if (swipeVector.magnitude <= clickRadius)
                {
                    // If not enough distance has travelled to be considered a swipe, then it's a click as long as not too much time has passed
                    validSwipe = false;
                    if (timeSinceMouseStartedBeingHeldDown <= timeBeforeExpiringClick)
                    {
                        Shoot();
                    }
                }

                // Now, if we have a valid swipe at this point, let's find the direction
                if (validSwipe)
                {
                    if (swipeVector.x > 0)
                    {
                        if (swipeVector.y > swipeVector.x)
                        {
                            movementDirection = Vector3.up;
                        } else if (swipeVector.y < 0 && swipeVector.y < -swipeVector.x)
                        {
                            movementDirection = Vector3.down;
                        } else
                        {
                            movementDirection = Vector3.right;
                        }
                    } else
                    {
                        if (swipeVector.y < swipeVector.x)
                        {
                            movementDirection = Vector3.down;
                        }
                        else if (swipeVector.y > 0 && swipeVector.y > -swipeVector.x)
                        {
                            movementDirection = Vector3.up;
                        }
                        else
                        {
                            movementDirection = Vector3.left;
                        }
                    }
                }

                // Reset the counter now
                timeSinceMouseStartedBeingHeldDown = 0;
            } else
            {
                timeSinceMouseStartedBeingHeldDown += Time.deltaTime;
            }
        } else
        {
            if (Input.GetMouseButton(0))
            {
                mouseStartPosition = Input.mousePosition;
                mouseIsHeldDown = true;
            }
        }
    }

    public void Shoot()
    {

        this.GetComponentInChildren<Animator>().Play("ThrowingAnimation");
        this.GetComponentInChildren<Animator>().SetTrigger("Walking");



        // If the player is firing in this frame
        if (sinceLastFire > fireRate && cooldownValue < 5.0f)
        {
            cooldownValue = cooldownValue + 0.5f;
            sinceLastFire = 0;
            // Play sound
            AudioSource.PlayClipAtPoint(fireSound, this.transform.position);

            // Instiansiate the paper prefab near the player facing is the direction of the cursor
            GameObject paper = (GameObject)Instantiate(paperPrefab, transform.position + movementDirection * 2, Quaternion.identity);

            // Force the paper on the same z as the player, stops it from being pushed on a different layer
            paper.transform.position = new Vector3(paper.transform.position.x, paper.transform.position.y, transform.position.z);

            // Move the firing direction a little bit
            Vector3 randomlyRotatedFireDirection = Quaternion.AngleAxis(Random.Range(-10, 10), Vector3.forward) * movementDirection;

            // Push the paper a little bit in the direction of the firing
            paper.GetComponent<Rigidbody2D>().AddForce(randomlyRotatedFireDirection * fireForce);
        }
    }
 

    public void RotateView()
    {
        // Set the player to face in the given direction
        transform.right = movementDirection;
    }

	void Start () {
        GameObject gameData = GameObject.FindWithTag("GameData");
        gameScript = gameData.GetComponent<GameScript>();
        //animator.SetBool(walkingAnimationParamId, true);

        AudioSource.PlayClipAtPoint(letsGoSound, this.transform.position);
    }


    void FixedUpdate ()
    {
        // Freeze the character if the game is finished
        if (!gameScript.gameOn) return;
        CharacterMovement();
        // Move the character by that direction
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
        MobileInput();
        RotateView();
        sinceLastFire += Time.deltaTime;
        if (mobileControls) fireRate = 0;
        //Cooldown dropping with fixed frames
        if (cooldownValue >= 0.1f && cooldownValue < 5.0f)
        {
            cooldownValue = cooldownValue - 0.01f;
        }
        else if (cooldownValue > 5.0f && !waitCheck)
        {
            waitCheck = true;
            StartCoroutine(CooldownWait());
        }
    }

}
