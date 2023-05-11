using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Private attributes
    private Rigidbody rb;
    private float motionX;
    private float motionY;

    // Public attributes
    public float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize rigid body reference
        rb = GetComponent<Rigidbody>();
    }

    private void OnMove(InputValue movement)
    {
        // Get motion vector
        Vector2 motionVector = movement.Get<Vector2>();

        motionX = motionVector.x;
        motionY = motionVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(motionX, 0.0f, motionY);

        rb.AddForce(movement * speed);
    }

}