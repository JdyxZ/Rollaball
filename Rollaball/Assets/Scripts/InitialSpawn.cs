using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class InitialSpawn : MonoBehaviour
{
    // Public vars
    public GameObject playerPrefab;
    public GameObject counterPannel;
    public static int numPlayers = 1;

    // Start is called before the first frame update
    public void Start()
    {
        switch(numPlayers)
        {
            case 1:
                initSinglePlayerMode(); break;
            case 2:
                initMultiplayerMode(); break;
        }        
    }

    private void initSinglePlayerMode()
    {
        // What is the var type? https://answers.unity.com/questions/1087276/why-woud-i-use-var-in-c.html 
        var player = PlayerInput.Instantiate(playerPrefab);

        // Set name
        player.transform.parent.name = "Player 1";

        // Get the player audio component
        AudioListener player_listener = player.transform.GetComponent<AudioListener>();

        // Get the player input component
        PlayerInput player_input = player.transform.GetComponent<PlayerInput>();

        // Enable player audio listener 
        player_listener.enabled = true;

        // Set player control scheme and action map
        player_input.SwitchCurrentControlScheme("Player 1");
        player_input.SwitchCurrentActionMap("Player");

        // Set player index
        player.GetComponent<PlayerController>().playerIndex = 0;

        // Deactivate counter planel for player 2
        counterPannel.transform.GetChild(1).gameObject.SetActive(false);
    }

    private void initMultiplayerMode()
    {
        // What is the var type? https://answers.unity.com/questions/1087276/why-woud-i-use-var-in-c.html 
        var player1 = PlayerInput.Instantiate(playerPrefab, 0, "Player 1", 0, Keyboard.current);
        var player2 = PlayerInput.Instantiate(playerPrefab, 1, "Player 2", 1, Keyboard.current);

        // Set names
        player1.transform.parent.name = "Player 1";
        player2.transform.parent.name = "Player 2";

        // Get camera components
        Camera player1_camera = player1.transform.parent.GetChild(1).GetComponent<Camera>();
        Camera player2_camera = player2.transform.parent.GetChild(1).GetComponent<Camera>();

        // Get audio components
        AudioListener player1_listener = player1.transform.GetComponent<AudioListener>();
        AudioListener player2_listener = player2.transform.GetComponent<AudioListener>();

        // Get player input components
        PlayerInput player1_input = player1.transform.GetComponent<PlayerInput>();
        PlayerInput player2_input = player2.transform.GetComponent<PlayerInput>();

        // Place players
        player1.transform.position = new Vector3(-2, 0.5f, 0);
        player2.transform.position = new Vector3(2, 0.5f, 0);

        // Set new rect in the cameras to split the screen in two slices
        player1_camera.rect = new Rect(0, 0, .5f, 1);
        player2_camera.rect = new Rect(.5f, 0, .5f, 1);

        // Enable player1 audio listener only
        player1_listener.enabled = true;
        player2_listener.enabled = false;

        // Set player control schemes and action maps
        /*
        player1_input.SwitchCurrentControlScheme("Player 1");
        player1_input.SwitchCurrentActionMap("Player");
        player2_input.SwitchCurrentControlScheme("Player 2");
        player2_input.SwitchCurrentActionMap("Player");
        */

        // Set player indexes
        player1.GetComponent<PlayerController>().playerIndex = 0;
        player2.GetComponent<PlayerController>().playerIndex = 1;
    }
}
