using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Public vars
    public float speed = 0;
    public List<Transform> waypoints;

    // Private vars
    private int waypointIndex;
    private float range;

    // Start is called before the first frame update
    void Start()
    {
        waypointIndex = 0;
        range = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Other methods
}
