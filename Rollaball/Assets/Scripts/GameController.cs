using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    // Public vars
    public GameObject endPanel;
    public TextMeshProUGUI[] counterText;
    public GameObject pauseButton;
    public GameObject pausePannel;

    // Private vars
    private bool gameFinished;

    // Start is called before the first frame update
    void Start()
    {
        // Deactivate pannel at the beginning
        endPanel.SetActive(false);

        // Set game finished state to false
        gameFinished = false;
    }

    public bool getGameState()
    {
        return gameFinished;
    }

    public void pauseGame()
    {
        // Stop time flow
        Time.timeScale = 0;

        // Deactive pause button
        pauseButton.SetActive(false);

        // Burst out pause pannel
        pausePannel.SetActive(true);
    }

    public void resumeGame()
    {
        // Continue time flow
        Time.timeScale = 1;

        // Reactivate pause button
        pauseButton.SetActive(true);

        // Hide pause pannel
        pausePannel.SetActive(false);
    }

    public void loseGame()
    {
        if(!gameFinished)
        {
            endPanel.SetActive(true);
            pauseButton.SetActive(false);
            endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Game over...";
            gameFinished = true;
        }
    }

    public void winGame()
    {
        if (!gameFinished)
        {
            endPanel.SetActive(true);
            pauseButton.SetActive(false);
            endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You win!!";
            gameFinished = true;
        }
    }

    public void setCounterText(int playerIndex, int count)
    {
        if(playerIndex < counterText.Length)
            counterText[playerIndex].text = "Harvested cubes: " + count.ToString();
    }
}
