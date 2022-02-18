/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 16/02/22                      */
/* LAST MODIFIED - 17/02/22                */
/* ======================================= */
/* LIGHT GATE                              */
/* LightGate.cs                            */
/* ======================================= */
/* Script manages the behaviour of the     */
/* light gate obstacle the player can      */
/* encounter.                              */
/* ======================================= */

// Directives
using UnityEngine;

public class LightGate : MonoBehaviour
{
    // *** SERIALISED VARIABLES *** //
    [SerializeField] private Sprite _colourOneEmitter; // Colour one emitter sprite
    [SerializeField] private Sprite _colourTwoEmitter; // Colour two emitter sprite

    // *** VARIABLES *** //
    private PlayerController _player; // Player component
    private ScreenInfo _screenInfo; // Screen info
    private LightBeam _lightBeam; // Light beam component
    private LightEmitter _lightEmitter; // Light emitter component
    private Color _colourOne = new Color(0.337f, 0.705f, 0.913f, 1.0f); // Colour One
    private Color _colourTwo = new Color(0.901f, 0.623f, 0.0f, 1.0f); // Colour Two
    private bool _isColourOne; // Is colour one boolean
    private float _halfWidth; // Half width variable

    /*
     * HALF WIDTH GET VARIABLE
     * 
     * Method returns the float value held
     * by the half width variable.
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
     * Method is invoked when the script is first
     * awoken.
     * 
     * Obtains the player, screen info, light
     * emitter, and light beam components
     */
    private void Awake()
    {
        // Obtain the player object from the scene
        _player = FindObjectOfType<PlayerController>();

        // Obtain the screen info object from the scene
        _screenInfo = FindObjectOfType<ScreenInfo>();

        // Determine a random integer to either 0 or 1
        int randomInt = Random.Range(0, 2);

        // Determine the value of the colour one boolean based on the value of the random in
        _isColourOne = randomInt == 0 ? true : false;
    }

    /*
     * FIND COMPONENTS METHOD
     * 
     * When invoked, the method obtains
     * the light beam and light emitter
     * components held in the child
     * game object components.
     */
    public void FindComponents()
    {
        // Obtain the light component from one of the child components
        _lightBeam = GetComponentInChildren<LightBeam>();

        // Obtain the light emitter from one of the child components
        _lightEmitter = GetComponentInChildren<LightEmitter>();
    }

    /*
     * INITIALISE COMPONENTS METHOD
     * 
     * When invoked, the method initialises
     * all the components that are used
     * to create a light gate.
     */
    public void InitialiseComponents()
    {
        // Set the half width of the light gate to the half width of the light emitter
        _halfWidth = _lightEmitter.HalfWidth;

        // Set the light beam is colour one variable to the value of the light gate is colour one variable
        _lightBeam.IsColourOne = _isColourOne;

        // Check the state of is colour one
        if (_isColourOne)
        {
            // Set the emitter sprite to an instance of the colour one emitter sprite
            _lightEmitter.EmitterSpriteRenderer.sprite = Instantiate(_colourOneEmitter);

            // Set the colour of the light beam to colour one
            _lightBeam.BeamSpriteRenderer.color = _colourOne;
            
        }
        else
        {
            // Set the emitter sprite to an instance of the colour two emitter sprite
            _lightEmitter.EmitterSpriteRenderer.sprite = Instantiate(_colourTwoEmitter);

            // Set the colour of the light beam to colour two
            _lightBeam.BeamSpriteRenderer.color = _colourTwo;
        }
    }

    /*
     * FIXED UPDATE METHOD
     * 
     * Method is invoked at fixed time intervals.
     * 
     * Method determines the position of the light
     * gate in relation to the players velocity
     * at the current fixed update.
     * 
     * Method also determines if the light gate
     * has disappeared from the left edge of
     * the screen and is destroyed.
     */
    void FixedUpdate()
    {
        // Obtain the position of the light gate
        Vector2 position = transform.position;

        // Decrement the X position based on the player velocity multiplied by the fixed time increment
        position.x -= _player.HorizontalVelocity * Time.fixedDeltaTime;

        // Check if the light gate has disappeared off the screen
        if (transform.position.x <= _screenInfo.LeftEdge)
        {
            // Destroy the game object
            Destroy(gameObject);
        }

        // Set the transform position of the light gate to the determined position
        transform.position = position;
    }
}