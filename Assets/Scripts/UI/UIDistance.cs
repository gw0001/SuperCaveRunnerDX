/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 02/02/22                      */
/* LAST MODIFIED - 18/02/22                */
/* ======================================= */
/* UI DISTANCE                             */
/* UIDistance.cs                           */
/* ======================================= */
/* Script manages the behaviour of the     */
/* distance text to relay how far the      */
/* player has travelled.                   */
/* ======================================= */

// Directives
using UnityEngine;
using TMPro;

public class UIDistance : MonoBehaviour
{
    // *** VARIABLES *** //
    private PlayerController _player; // Player character
    private TextMeshProUGUI _distanceText; // UI Distance text

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is first 
     * awoken.
     * 
     * When invoked, the player and distance text
     * objects are obtained from the scene.
     */
    void Awake()
    {
        // Obtain the player from the game scene
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
        
        // Obtain the distance UI text from the camera
        _distanceText = GameObject.Find("DistanceText").GetComponent<TextMeshProUGUI>();
    }

    /*
     * FIXED UPDATE METHOD
     * 
     * Method is invoked at fixed time intervals.
     * 
     * When invoked, the distance text is updated to
     * show how far the player has travelled.
     */
    void FixedUpdate()
    {
        // Update the distance text
        _distanceText.text = Mathf.FloorToInt(_player.Distance).ToString() + " m";
    }
}