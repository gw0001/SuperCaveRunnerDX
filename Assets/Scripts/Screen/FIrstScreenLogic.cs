/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 12/02/22                      */
/* LAST MODIFIED - 12/02/22                */
/* ======================================= */
/* FIRST SCREEN LOGIC                      */
/* FirstScreenLogic.cs                     */
/* ======================================= */
/* Script for managing the first screen of */
/* the game.                               */
/* ======================================= */

// Directives
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FirstScreenLogic : MonoBehaviour
{
    // *** SERIALISED SETTINGS *** //
    [Header ("Screen settings")]
    [SerializeField] private float _textAppearTime = 1f; // Text appear time
    [SerializeField] private float _textDisappearTime = 1f; // Text disappear time
    [SerializeField] private float _screenTime = 3f; // Screen time

    [SerializeField] private AudioClip _introClip;

    private AudioSource _introSound;
    private bool _introSoundPlayed = false;

    // *** VARIABLES *** //
    private TextMeshProUGUI _screenText;
    private float _screenTimer; // Screen timer

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is first
     * awoken.
     * 
     * Method obtains the screen text, then sets
     * the alpha of the text to 0f and initialises
     * the timer to 0 seconds.
     */
    void Awake()
    {
        // Obtain the screen text object
        _screenText = GameObject.FindObjectOfType<TextMeshProUGUI>();

        // Set the screen text alpha to 0f
        _screenText.alpha = 0f;

        // Set the screen text timer to 0 seconds
        _screenTimer = 0f;

        // Initialise the intro sound played boolean to false
        _introSoundPlayed = false;

        // Obtain the audio source component from the game object
        _introSound = GetComponent<AudioSource>();

        // Set the clip to the intro clip
        _introSound.clip = _introClip;

        // Prevent the clip from looping
        _introSound.loop = false;

        // Prevent the sound clip from playing when the audio source is awoken
        _introSound.playOnAwake = false;
    }

    /*
     * FIXED UPDATE METHOD
     * 
     * Method is updated at regular fixed time intervals
     * and handles the behaviour of the first screen.
     */
    void FixedUpdate()
    {
        // Increment the screen text timer by a fixed time increment
        _screenTimer += Time.fixedDeltaTime;

        // Check if the screen timer is greater than, or equal to, the text appear time
        if(_screenTimer >= _textAppearTime)
        {
            // Set the screen alpha to 1f
            _screenText.alpha = 1f;

            // Check if the intro sound has been played
            if(!_introSoundPlayed)
            {
                // Play Sound
                PlaySound();
            }
        }

        // Check if the screen timer is greater than, or equal to the screen time minus the text disappear time
        if(_screenTimer >= _screenTime - _textDisappearTime)
        {
            // Set the text alpha to 0f
            _screenText.alpha = 0f;
        }

        // Check if the screen timer is greater than, or equal to, the screen time
        if(_screenTimer >= _screenTime)
        {
            // Load the title screen
            SceneManager.LoadScene("TitleScreen");
        }
    }

    /*
     * PLAY SOUND METHOD
     * 
     * When invoked, the method plays the intro
     * sound and sets the sound played boolean
     * to true.
     */
    private void PlaySound()
    {
        // Play the audio source
        _introSound.Play();

        // Set intro sound played boolean to true
        _introSoundPlayed = true;
    }
}