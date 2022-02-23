/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 23/02/22                      */
/* LAST MODIFIED - 23/02/22                */
/* ======================================= */
/* UI DIFFICULTY                           */
/* UIDifficulty.cs                         */
/* ======================================= */
/* Script for displaying the difficulty on */
/* game screen. Will be used for test      */
/* builds.                                 */
/* ======================================= */

// Directives
using UnityEngine;
using TMPro;

public class UIDifficulty : MonoBehaviour
{
    private TextMeshProUGUI _difficultyText; // Difficulty text
    private GameState _gameState; // Game state object

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is first
     * awoken. Obtains the text mesh component 
     * used to display the difficulty and 
     * the game state object from the scene.
     */
    private void Awake()
    {
        // Obtain the difficulty text
        _difficultyText = GetComponent<TextMeshProUGUI>();

        // Obtain the game state from the scene
        _gameState = GameObject.FindObjectOfType<GameState>();
    }


    /*
     * FIXED UPDATE
     * 
     * Method is invoked at fixed time intervals.
     * Method updates the difficulty text.
     */
    private void FixedUpdate()
    {
        // Set the difficulty text with the current difficulty based on distance.
        _difficultyText.text = "DIFFICULTY: " + _gameState.DistanceDifficulty.ToString();
    }
}