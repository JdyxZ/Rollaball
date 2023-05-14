using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Private attributes
    private Rigidbody rb;
    private float motionX;
    private float motionY;
    private int counter = 0;
    private int numCollectibles;

    // Public attributes
    public GameObject CollectibleParent;
    public TextMeshProUGUI counterText;
    public GameObject winTextObject;
    public float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize rigid body reference
        rb = GetComponent<Rigidbody>();

        // Initialize counter info
        SetCounterText();

        // Deactivate win message at the beginning
        winTextObject.SetActive(false);

        // Get the number of collectibles in the screen
        numCollectibles = CollectibleParent.transform.childCount;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(motionX, 0.0f, motionY);

        rb.AddForce(movement * speed);
    }

    private void OnMove(InputValue movement)
    {
        // Get motion vector
        Vector2 motionVector = movement.Get<Vector2>();

        motionX = motionVector.x;
        motionY = motionVector.y;
    }

    void SetCounterText()
    {
        counterText.text = "Harvested cubes: " + counter.ToString();
    }

    void ValidateWinCondition()
    {
        if (counter == numCollectibles)
            winTextObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Collectible"))
        {
            // Hide collectible
            other.gameObject.SetActive(false);

            // Increase the counter
            counter++;

            // Update counter info 
            SetCounterText();

            // Validate win condition
            ValidateWinCondition();
        }
    }

}