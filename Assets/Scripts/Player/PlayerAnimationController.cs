/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 20/02/22                      */
/* LAST MODIFIED - 25/02/22                */
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

        // Set animator "IsIdle" to false
        _playerAnimator.SetBool("IsIdle", false);

        // Set animator "IsInAir" to false
        _playerAnimator.SetBool("IsInAir", false);

        // Set animator "IsRunning" to true
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
        // Set animator "IsInAir" bool to true
        _playerAnimator.SetBool("IsInAir", true);

        // Set animator "IsRunning" bool to false
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
        // Set the animator speed to 1
        _playerAnimator.speed = 1f;

        // Set animator "IsIdle" to false
        _playerAnimator.SetBool("IsIdle", false);

        // Set animator "IsInAir" to false
        _playerAnimator.SetBool("IsInAir", false);

        // Set animator "Is Running" to false
        _playerAnimator.SetBool("IsRunning", false);

        // Set animator "IsDying" to true
        _playerAnimator.SetBool("IsDying", true);

        // Determine the animation based on the last collision encountered by the player
        if(_player.PlayerCollision == PlayerController.LastCollision.obstacle)
        {
            // Set animator "HitObstable" to true to display the obstacle death animation
            _playerAnimator.SetBool("HitObstacle", true);
        }
        else if (_player.PlayerCollision == PlayerController.LastCollision.lightgate)
        {
            // Set animator "HitBeam" to true to display the light gate death animation
            _playerAnimator.SetBool("HitBeam", true);
        }
        if (_player.PlayerCollision == PlayerController.LastCollision.ground || _player.PlayerCollision == PlayerController.LastCollision.pit)
        {
            // Set animator "HasFallen" to true to display a gravestone that will not be seen by the player (as far as I'm aware...)
            _playerAnimator.SetBool("HasFallen", true);
        }
    }
}
