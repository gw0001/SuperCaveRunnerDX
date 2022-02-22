/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 20/02/22                      */
/* LAST MODIFIED - 22/02/22                */
/* ======================================= */
/* PLAYER ANIMATION CONTROLLER             */
/* PlayerAnimationController.cs            */
/* ======================================= */
/* Script handles the sprite animation for */
/* the player character, depending on the  */
/* player state.                           */
/* ======================================= */

// Directives
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private PlayerController _player; // Player component
    private Animator _playerAnimator; // Player animator

    // Start is called before the first frame update
    void Awake()
    {
        // Obtain the player component
        _player = GetComponent<PlayerController>();


        // Player animator
        _playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check the players colour
        CheckColour();

        // Check the state of the player
        if(_player.PlayerStatus == PlayerController.PlayerState.idle)
        {
            // Player state is idle, handle the idle animation
            HandleIdle();
        }
        else if (_player.PlayerStatus == PlayerController.PlayerState.running)
        {
            // Player is running, handle the running animation
            HandleRunning();
        }
        else if (_player.PlayerStatus == PlayerController.PlayerState.inAir)
        {
            // Player is in the air, handle the in air animation
            HandleInAir();
        }
        else if (_player.PlayerStatus == PlayerController.PlayerState.dying)
        {
            // Player is dying, handle the dying animation - STILL TO IMPLEMENT
            HandleDying();
        }
    }

    /*
     * CHECK COLOUR METHOD
     * 
     * Method is used to check the colour of
     * the player and set the appropriate
     * boolean for the animator.
     */
    private void CheckColour()
    {
        // Check if the player is colour one
        if(_player.IsColourOne)
        {
            // Set the "Is Colour One" boolean parameter to true
            _playerAnimator.SetBool("IsColourOne", true);
        }
        else
        {
            // Set the "Is Colour One" boolean parameter to false
            _playerAnimator.SetBool("IsColourOne", false);
        }
    }

    /*
     * HANDLE IDLE METHOD
     * 
     * Method handles the idle animation of
     * the player.
     */
    private void HandleIdle()
    {
        // Set the "Is Idle" parameter to true
        _playerAnimator.SetBool("IsIdle", true);
    }

    /*
     * HANDLE RUNNING METHOD
     * 
     * Method handles the running animation
     * of the player. Method also determines
     * the animation speed based on the 
     * speed of the player
     */
    private void HandleRunning()
    {
        // Determine the animation speed based on the players horizontal speed divided by the maximum run velocity
        float speed = _player.HorizontalVelocity / _player.MaxRunVelocity;

        // Check if the speed is less than 0.2f (20%)
        if(speed <= 0.2f)
        {
            // Set the speed to 20%
            speed = 0.2f;
        }


        // Set the speed of the animator to the determined speed
        _playerAnimator.speed = speed;

        // Set is idle to false
        _playerAnimator.SetBool("IsIdle", false);

        // Is in air to false
        _playerAnimator.SetBool("IsInAir", false);

        // Set is running to true
        _playerAnimator.SetBool("IsRunning", true);
        
    }

    /*
     * HANDLE IN AIR METHOD
     * 
     * Method handles the in air animation
     * of the player.
     */
    private void HandleInAir()
    {
        _playerAnimator.SetBool("IsInAir", true);
        _playerAnimator.SetBool("IsRunning", false);
    }

    /*
     * HANDLE IS DYING METHOD
     * 
     * Method handles the is dying animation
     * of the player.
     */
    private void HandleDying()
    {
        _playerAnimator.SetBool("IsIdle", false);
        _playerAnimator.SetBool("IsInAir", false);
        _playerAnimator.SetBool("IsRunning", false);
        _playerAnimator.SetBool("IsDying", true);
    }
}
