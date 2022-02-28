/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 28/02/22                      */
/* LAST MODIFIED - 28/02/22                */
/* ======================================= */
/* TITLE SUPER                             */
/* TitleSuper.cs                           */
/* ======================================= */
/* Script manages the super component of   */
/* the main title.                         */
/* ======================================= */

// Directives
using UnityEngine;
using UnityEngine.UI;

public class TitleSuper : MonoBehaviour
{
    // *** SERIALISED VARIABLES *** //
    [SerializeField] private float _speed = 100f; // Soeed

    // *** VARIABLES *** //
    private RectTransform _rect; // Rect transform
    private Vector2 _origin; // Origin
    private Vector2 _startingPosition; // Starting position
    private float _xOffset = 500f; // X Offset
    private bool _canMove = false; // Can move
    private bool _soundEffectPlayed = false; // Sound effect played
    private SoundEffect _wooshEffect; // Woosh sound effect

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is first 
     * awoken.
     */
    private void Awake()
    {
        // Obtain the rect transform
        _rect = GetComponent<Image>().rectTransform;

        // Obtain the woosh sound effect component
        _wooshEffect = GetComponent<SoundEffect>();

        // Obtain the origin
        _origin = _rect.anchoredPosition;

        // Obtain the position from the anchored position
        _startingPosition = _rect.anchoredPosition;

        // Modify the x position
        _startingPosition.x -= _xOffset;

        // Set the anchored position to the modified position
        _rect.anchoredPosition = _startingPosition;
    }

    /*
     * FIXED UPDATE METHOD
     * 
     * Method is invoked at fixed time intervals.
     */
    void FixedUpdate()
    {
        // Check if the object can move
        if(_canMove)
        {
            // Check if the sound effect has not been played
            if(!_soundEffectPlayed)
            {
                // Play the woosh sound effect
                _wooshEffect.PlaySoundEffect();

                // Set sound effect played as true
                _soundEffectPlayed = true;
            }

            // Position
            Vector2 position = _rect.anchoredPosition;

            // Determine the new position based on speed multiplied by the fixed time increment
            position.x += _speed * Time.fixedDeltaTime;

            // Check if position 
            if (position.x >= _origin.x)
            {
                // Set the X position to the origin X value
                position.x = _origin.x;

                // Set can move to false
                _canMove = false;
            }

            // Set the position of the rect transform
            _rect.anchoredPosition = position;
        }
    }

    /*
     * CAN MOVE METHOD
     * 
     * When invoked, the method
     */
    public void CanMove()
    {
        _canMove = true;
    }
}