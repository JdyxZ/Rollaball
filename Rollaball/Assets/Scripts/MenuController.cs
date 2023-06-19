using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void initGame(int numPlayers)
    {
        // Set number of players
        InitialSpawn.numPlayers = numPlayers;

        // Continue time flow
        Time.timeScale = 1;

        // Load MiniGame scene
        transitionScene("MiniGame");
    }

    public void transitionScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
