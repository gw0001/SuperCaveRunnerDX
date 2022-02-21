/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 13/02/22                      */
/* LAST MODIFIED - 14/02/22                */     
/* ======================================= */
/* RESULT ANALYSER                         */
/* ResultAnalyser.cs                       */
/* ======================================= */
/* Class is used to analyse and return     */
/* a message back to the user.             */
/* ======================================= */

// Directives
using UnityEngine;

public class ResultAnalyser : MonoBehaviour
{
    // *** VARIABLES ***//
    private PlayerController _player; // Player object
    private GameState _gameState; // Game state object
    private int _easyDistance; // Easy distance
    private int _mediumDistance; // Medium Distance
    private int _hardDistance; // Hard Distance
    private int _insaneDistance; // Insane Distance

    private void Awake()
    {
        // Obtain the player controller from the scene
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        // obtain the game state from the scene
        _gameState = GameObject.FindObjectOfType<GameState>();

        // Set distances from the game state object
        _easyDistance = _gameState.EasyDistance;
        _mediumDistance = _gameState.MediumDistance;
        _hardDistance = _gameState.HardDistance;
        _insaneDistance = _gameState.InsaneDistance;
    }

    /*
     * RESULT ANALYSIS METHOD
     * 
     * Method results a text string, based on
     * the players performance. Text string
     * consists of the players distance, the last
     * action they did before they died, and the 
     * last thing the collided into. Also returns
     * a small message of encouragement.
     */
    public string ResultAnalysis()
    {
        // Initialise the result text
        string resultText = "";

        // Obtain the distance from the player as an integer
        int distance = Mathf.FloorToInt(_player.Distance);

        // Initialise the starting text
        string startingText = "YOU RAN ";

        // Turn the distance into a string 
        string distanceText = distance.ToString() + "m BEFORE";

        // Initialise the last actions string
        string lastActions = "";

        // Obtain the last action the player performed
        PlayerController.LastAction lastAction = _player.PlayerAction;

        // Obtain the last collision the player encountered
        PlayerController.LastCollision lastCollision = _player.PlayerCollision;

        // Check if the last action the player did was jump
        if(lastAction == PlayerController.LastAction.jump)
        {
            // Include the players last action as jumping
            lastActions += " YOU JUMPED";

            // Check if the player collided with the ground
            if(lastCollision == PlayerController.LastCollision.ground)
            {
                // Relay the ground collision to the player
                lastActions += ", FACE PLANTED INTO A WALL, THEN FELL DOWN A PIT.";
            }
            // Check if the last collision was with an obstacle
            else if(lastCollision == PlayerController.LastCollision.obstacle)
            {
                // Relay the obstacle collision to the player
                lastActions += ", THEN LANDED ON A STALAGMITE.";
            }
            // Check if the last collision was with an obstacle
            else if (lastCollision == PlayerController.LastCollision.lightgate)
            {
                // Relay the last collision as an obstacle
                lastActions += " INTO A BEAM OF LIGHT AND SPONTANIOUSLY COMBUSTED.";
            }
            // Check if the player fell into the pit
            else if(lastCollision == PlayerController.LastCollision.pit)
            {
                // Relay the pit collision
                lastActions += " INTO A PIT.";
            }
        }
        // Else, assume the last action the player did was run
        else
        {
            // Relay the running action from the player
            lastActions += " YOU RAN";

            // Check if the last collision was with the ground
            if (lastCollision == PlayerController.LastCollision.ground)
            {
                // Relay the players collision
                lastActions += " OFF THE EDGE, THEN FACE PLANETED INTO A WALL.";
            }
            // Check if the last collision was with an obstacle
            else if (lastCollision == PlayerController.LastCollision.obstacle)
            {
                // Relay the last collision as an obstacle
                lastActions += " INTO A STALAGMITE.";
            }
            // Check if the last collision was with an obstacle
            else if (lastCollision == PlayerController.LastCollision.lightgate)
            {
                // Relay the last collision as an obstacle
                lastActions += " INTO A BEAM OF LIGHT AND CAUGHT FIRE.";
            }
            // Check if the players last collision was with a pit
            else if (lastCollision == PlayerController.LastCollision.pit)
            {
                // Relay the players collision with the pit back to them
                lastActions += " OFF THE EDGE AND INTO A PIT.";
            }
        }

        //Initialise an additional comment string with a couple of new line commands
        string additionalComment = "\n\n";

        // Check the distance against the cut off points
        if (distance < _easyDistance)
        {
            // Display the message for failing at very easy difficulty
            additionalComment += "C'MON! YOU CAN DO BETTER THAN THAT!!";
        }
        else if (distance >= _easyDistance && distance < _mediumDistance)
        {
            // Display the message for failing at easy difficulty
            additionalComment += "YOU'RE GETTING THE HANG OF IT, KEEP TRYING!!";
        }
        else if (distance > _mediumDistance && distance < _hardDistance)
        {
            // Display the message for failing at medium difficulty
            additionalComment += "GOOD DISTANCE, YOU'RE PRETTY GOOD!!";
        }
        else if (distance >= _hardDistance && distance < _insaneDistance)
        {
            // Display the message for failing at hard difficulty
            additionalComment += "WOW! NOW THAT'S FAR! THANKS FOR PLAYING!!";
        }
        else if (distance >= _insaneDistance)
        {
            // Display a message for at insane difficulty
            additionalComment += "HOW IN THE WORLD DID YOU GET THIS FAR!? I HOPE YOU REALISE THERE IS NO END TO THIS GAME!!";
        }

        // Finalise the result text
        resultText = startingText + distanceText + lastActions + additionalComment;

        // Return the result text
        return resultText;
    }
}