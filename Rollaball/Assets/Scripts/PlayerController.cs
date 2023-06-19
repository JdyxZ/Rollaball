using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Public attributes
    public float speed = 0;
    public int playerIndex;

    // Private attributes
    private Rigidbody rb;
    private GameObject CollectibleParent;
    private Transform playerRespawn;
    private GameController gameController;
    private ScoreHandler scoreHandler;
    private AudioSource playerAudioSource;
    private float motionX;
    private float motionY;
    private int individualCounter;
    private int numCollectibles;

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize rigid body reference
        rb = GetComponent<Rigidbody>();

        // Initialize private objects
        CollectibleParent = GameObject.Find("Collectibles");
        playerRespawn = GameObject.Find("Player Respawn").transform;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        scoreHandler = GameObject.Find("Canvas/Counter Panel").GetComponent<ScoreHandler>();
        playerAudioSource = transform.parent.Find("Audio Player").GetComponent<AudioSource>();

        // Initialize individual counter
        individualCounter = 0;

        // Initialize counter info
        setCounterInfo();

        // Get the number of collectibles in the screen
        numCollectibles = CollectibleParent.transform.childCount;
    }

    // Update is called once per frame
    private void Update()
    {
        if(transform.position.y < -10)
        {
            Respawn();
        }
    }

    // Physics update
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(motionX, 0.0f, motionY);

        rb.AddForce(movement * speed);
    }

    // Get motion vector
    private void OnMove(InputValue movement)
    {
        Vector2 motionVector = movement.Get<Vector2>();
        motionX = motionVector.x;
        motionY = motionVector.y;
    }

    // Set counter info
    private void setCounterInfo()
    {
        gameController.setCounterText(playerIndex, individualCounter);
    }

    // Validate win condition
    private void ValidateWinCondition()
    {
        if (scoreHandler.Score == numCollectibles)
            EndGame("win");
    }

    // End game
    private void EndGame(string type)
    {
        // Check
        if (gameController.getGameState())
            return;

        // Check victory
        if (string.Equals(type, "win"))
        {
            gameController.winGame();
        }
        else if (string.Equals(type, "lose"))
        {
            gameController.loseGame();
            this.gameObject.SetActive(false);
        }
    }

    // Player respawn
    private void Respawn()
    {
        // Disable rigidbody in order to move the player without forces
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();

        // Respawn the player
        transform.position = playerRespawn.position;
    }

    // Launch collectible animation
    private void throwHarvestAnimation(GameObject collectible)
    {
        // Add components
        move move = collectible.AddComponent<move>();
        rotate rotate = collectible.GetComponent<rotate>();
        TweenScale tweenScale = collectible.AddComponent<TweenScale>();
        DestroyTimer destroyTimer = collectible.AddComponent<DestroyTimer>();

        // Set motion animation
        move.movementSpeed = new Vector3(0, 4, 0);
        move.space = Space.World;

        // Set rotation animation
        rotate.rotationSpeed = new Vector3(0, 90, 180);

        // Set scale animation
        tweenScale.targetScale = 0.3f;
        tweenScale.timeToReachTarget = 1f;

        // Set destroy timer
        destroyTimer.timeToDestroy = 1f;
    }

    // Triggers
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Collectible"))
        {
            // Play collectible sound
            playerAudioSource.Play();

            // Throw collectible harvest animation
            throwHarvestAnimation(other.gameObject);

            // Increase counters
            individualCounter++;
            scoreHandler.Score++;

            // Update counter info 
            setCounterInfo();

            // Validate win condition
            ValidateWinCondition();
        }
    }

    // Collisions
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Sphere enemy"))
            EndGame("lose");
    }

}