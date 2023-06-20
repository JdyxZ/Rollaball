using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Public vars
    public float speed = 0;
    public List<Transform> waypoints;

    // Private vars
    private int numWayPoints;
    private int waypointIndex;
    private float range;

    // Start is called before the first frame update
    void Start()
    {
        numWayPoints = waypoints.Count;
        waypointIndex = 0;
        range = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy movement
        Move();
    }

    // Other methods
    void Move()
    {
        // Calculate parameters
        Vector3 currentWayPoint = waypoints[waypointIndex].position;
        Vector3 motionVector = Vector3.forward * speed * Time.deltaTime;
        float waypointDistance = Vector3.Distance(transform.position, waypoints[waypointIndex].position);

        // Adjust direction
        transform.LookAt(currentWayPoint);

        // Perform movement
        transform.Translate(motionVector);

        // Set new direction
        if (waypointDistance < range)
        {
            waypointIndex++;
            waypointIndex %= numWayPoints;
        }
    }
}
