/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 27/02/22                      */
/* LAST MODIFIED - 27/02/22                */
/* ======================================= */
/* MUSIC CONTROLLER                        */
/* MusicController.cs                      */
/* ======================================= */
/* Script allows the user to control the   */
/* music by means of muting and unmuting.  */
/* ======================================= */

// Directives
using UnityEngine;
using UnityEngine.InputSystem;

public class MusicControlller : MonoBehaviour
{
    // *** SERIALISED VARIABLES *** //
    [SerializeField] private float _musicCooldownTime = 0.5f; // Music cooldown time

    // *** VARIABLES *** //
    private MusicManager _musicManager;  // Music manager
    private float _musicCooldownTimer; // Music cooldown timer
    private bool _canMuteMusic; // Can mute music boolean
    private bool _musicMuted; // Music muted boolean

    /*
     *  AWAKE METHOD
     *  
     *  Method is invoked when the script is awoken.
     *  
     *  Obtains the music manager and initialises the
     *  booleans and timer.
     */
    private void Awake()
    {
        // Obtain the music manager from the scene
        _musicManager = GameObject.FindObjectOfType<MusicManager>();

        // Set the music muted to false
        _musicMuted = false;

        // Set can mute music to true
        _canMuteMusic = true;

        // Initialise the music cool down timer to 0 seconds
        _musicCooldownTimer = 0f;
    }

    /*
     * FIXED UPDATE METHOD
     * 
     * Method is invoked at regular fixed time intervals.
     */
    private void FixedUpdate()
    {
        // Check if the player cannot mute the music
        if(!_canMuteMusic)
        {
            // Increment the music coold down timer by the fixed time increment
            _musicCooldownTimer += Time.fixedDeltaTime;

            // Check if the music cool down timer is greater than, or equal to, the music cool down time
            if(_musicCooldownTimer >= _musicCooldownTime)
            {
                // Reset the music cool down timer
                _musicCooldownTimer = 0f;

                // Set can mute music to true
                _canMuteMusic = true;
            }
        }
    }

    /*
     * MUTE MUSIC METHOD
     * 
     * Method is invoked by the player pressing the 
     * appropriate button.
     */
    public void MuteMusic(InputAction.CallbackContext context)
    {
        // Check if the context has been performed and the user can mute the music
        if(context.performed && _canMuteMusic)
        {
            // Invert the value of music muted
            _musicMuted = !_musicMuted;

            // Check the value of music muted
            if (_musicMuted)
            {
                // Have the music manager mute the music
                _musicManager.MuteMusic();
            }
            else
            {
                // Have the music manager unmute the music
                _musicManager.UnmuteMusic();
            }

            // Set can mute music to false
            _canMuteMusic = false;
        }
    }
}
