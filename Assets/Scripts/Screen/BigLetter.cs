/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 28/02/22                      */
/* LAST MODIFIED - 28/02/22                */
/* ======================================= */
/* BIG LETTER                              */
/* BigLetter.cs                            */
/* ======================================= */
/* Script manages the behaviour of the     */
/* big letter components of the main       */
/* title.                                  */
/* ======================================= */

// Directives
using UnityEngine;
using UnityEngine.UI;

public class BigLetter : MonoBehaviour
{
    private Image _image; // Image
    private RectTransform _rect; // Rect transform 
    private SoundEffect _thudSound;
    private Vector2 _origin; // Origin
    private Color _invisible = new Color(1f, 1f, 1f, 0f); // Invisible colour
    private Color _visible = new Color(1f, 1f, 1f, 1f); // Visible colour
    private float _jitterLife = 0.3f; // Jitter life value
    private float _jitterTimer; // Jitter timer
    private float _minX = -5f; // Minimum X value
    private float _maxX = 5f; // Maximum X value
    private float _minY = -5f; // Minimum Y value
    private float _maxY = 5f; // Maximum Y value
    private bool _jitterDisabled = false; // Jitter disabled boolean
    private bool _canPlaySound = false;

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is 
     * awoken.
     */
    private void Awake()
    {
        // Obtain the image component
        _image = GetComponent<Image>();

        // Obtain the rect transform
        _rect = _image.rectTransform;

        // Obtain the origin from the rect anchored position
        _origin = _rect.anchoredPosition;

        // Obtain the thud sound effect component
        _thudSound = GetComponent<SoundEffect>();

        _image.color = _invisible;

        // Set the jitter timer to 0 seconds
        _jitterTimer = 0f;
    }

    /*
     * FIXED UPDATE METHOD
     * 
     * Method is invoked 
     */
    void FixedUpdate()
    {
        // Check if the sound can be played
        if(_canPlaySound)
        {
            // Play thud sound with a random pitch
            _thudSound.PlayWithRandomPitch();

            // Set can be played to false
            _canPlaySound = false;
        }



        // Check if the jitter hasn't been disabled
        if(!_jitterDisabled)
        {
            // Invoke the jitter method
            Jitter();
        }
    }

    /*
     * JITTER METHOD
     * 
     * Method handles the jitter of the letter
     * image. If the letter still has life 
     * left, the image will jitter. If not,
     * the image will no longer jitter.
     */
    private void Jitter()
    {
        // Increment the jitter timer by delta time
        _jitterTimer += Time.fixedDeltaTime;

        // Check if the jitter timer is less than, or equal to the jitter life
        if(_jitterTimer <= _jitterLife)
        {
            // Obtain a random Y value
            float randomY = Random.Range(_minY, _maxY);

            // Obtain a random X value
            float randomX = Random.Range(_minX, _maxX);

            // Position vector initialised to the origin
            Vector2 position = _origin;

            // Alter the X position with the random X value
            position.x += randomX;

            // Alter the Y position with the random Y value
            position.y += randomY;

            // Set the position of the rect
            _rect.anchoredPosition = position;
        }
        else
        {
            // Disable the jitter
            DisableJitter();
        }
    }

    public void DisableJitter()
    {
        // Set the anchored position to the origin
        _rect.anchoredPosition = _origin;

        // Set jitter disabled to true
        _jitterDisabled = true;
    }

    private void OnEnable()
    {
        // Set the jitter timer to 0 seconds
        _jitterTimer = 0f;

        // Set can play sound to true
        _canPlaySound = true;

        // Set the image colour to visible
        _image.color = _visible;

        // Enable the jitter
        _jitterDisabled = false;
    }

    private void OnDisable()
    {
        // Disable the jitter
        DisableJitter();

        // Set the colour of the image to invisible
        _image.color = _invisible;
    }
}
