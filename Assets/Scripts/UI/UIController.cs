using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
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