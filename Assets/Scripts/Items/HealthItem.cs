/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 13/02/22                      */
/* LAST MODIFIED - 14/02/22                */
/* ======================================= */
/* HEALTH ITEM                             */
/* HealthItem.cs                           */
/* ======================================= */
/* Script controls the behaviour of the    */
/* health items that the player will       */
/* encounter.                              */
/* ======================================= */

// Directives
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    // *** SERIALIZED VARIABLES *** //
    [SerializeField] private float _floatSpeed = 2f; // Float speed
    [SerializeField] private float _floatHeight = 2f; // Float height

    // *** VARIABLES *** //
    private PlayerController _player; // Player
    private ScreenInfo _screenInfo; // Screen info
    private float _halfHeight; // Half height
    private float _halfWidth; // Half width

    /*
     * HALF HEIGHT GET METHOD
     * 
     * Method returns the value held by
     * the half height variable.
     */
    public float HalfHeight
    {
        get
        {
            return _halfHeight;
        }


    }

    /*
     * HALF WIDTH GET METHOD
     * 
     * Method returns the value held by
     * the half width variable.
     */
    public float HalfWidth
    {
        get
        {
            return _halfWidth;
        }
    }

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is woken
     * up.
     * 
     * Method obtains the player and screen info
     * objects from the scene, before determining
     * the half height and half widths.
     */
    private void Awake()
    {
        // Obtain the player controller from the scene
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        // Obtain the screen info object from the scene
        _screenInfo = GameObject.FindObjectOfType<ScreenInfo>();

        // Set the half height
        _halfHeight = transform.localScale.y / 2f;

        // Set the half width
        _halfWidth = transform.localScale.x / 2f;
    }

    /*
     * FIXED UPDATE METHOD
     * 
     * Method is invoked at regular time intervals.
     * 
     * Method handles the position of the object
     * in relation to the player speed and how the 
     * object floats in the scene. Also handles
     * destruction of the object when it disappears
     * off the screen.
     */
    private void FixedUpdate()
    {
        // Obtain the current position of the object
        Vector2 position = transform.position;

        // Alter the X position based on the players run velocity multiplied by the fixed time increment
        position.x -= _player.HorizontalVelocity * Time.fixedDeltaTime;

        // Alter the Y position with a sine curve based on the item float speed at the fixed time increment and float height
        position.y += Mathf.Sin(Time.fixedTime * Mathf.PI * _floatSpeed) * _floatHeight;

        // Check if the object has disappeared off the screen
        if (position.x + HalfWidth <= _screenInfo.LeftEdge)
        {
            // Destroy the game object
            Destroy(gameObject);
        }

        // Set the position to the new position
        transform.position = position;
    }

    /*
     * PICKED UP METHOD
     * 
     * Method invokes the add health function
     * before destroying itself.
     */
    public void PickedUp()
    {
        // Invoke the add health function
        _player.AddHealth();

        // Destroy the game object
        Destroy(gameObject);
    }
}