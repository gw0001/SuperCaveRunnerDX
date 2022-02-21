using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private PlayerController _player; // Player component
    private Animator _playerAnimator; // Player animator

    //[SerializeField] private Animation[] _colourOneAnimations;
    //[SerializeField] private Animation[] _colourTwoAnimations;



    private Animation _test;

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
            HandleRunning();
        }
        else if (_player.PlayerStatus == PlayerController.PlayerState.inAir)
        {
            HandleInAir();
        }
        else if (_player.PlayerStatus == PlayerController.PlayerState.dying)
        {
            HandleDying();
        }
    }


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

    private void HandleIdle()
    {
        // Set the "Is Idle" parameter to true
        _playerAnimator.SetBool("IsIdle", true);
    }

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


        _playerAnimator.SetBool("IsIdle", false);
        _playerAnimator.SetBool("IsInAir", false);
        _playerAnimator.SetBool("IsRunning", true);
        
    }

    private void HandleInAir()
    {
        _playerAnimator.SetBool("IsInAir", true);
        _playerAnimator.SetBool("IsRunning", false);
    }

    private void HandleDying()
    {
        _playerAnimator.SetBool("IsIdle", false);
        _playerAnimator.SetBool("IsInAir", false);
        _playerAnimator.SetBool("IsRunning", false);
        _playerAnimator.SetBool("IsDying", true);
    }
}
