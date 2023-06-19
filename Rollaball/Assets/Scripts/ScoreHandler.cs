using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    // Public variables
    public int Score
    {
        get
        {
            return score;
        }
        set
        { 
            score = value; 
        }
    }

    // Private variables
    private int score = 0;
}
