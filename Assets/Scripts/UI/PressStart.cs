/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 12/02/22                      */
/* LAST MODIFIED - 18/02/22                */
/* ======================================= */
/* PRESS START                             */
/* PressStart.cs                           */
/* ======================================= */
/* Script manages the press start text     */
/* that appears on the title screen.       */
/* ======================================= */

// Directives
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class PressStart : MonoBehaviour
{
    // *** SERIALIZED CLASS VARIABLES *** //
    [Header ("Press Start settings")]
    [SerializeField] private ControlScheme _controlScheme;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private float _fadeTime = 0.5f;
    [SerializeField] private float _flashOnTime = 1f;
    [SerializeField] private float _flashOffTime = 1f;
    [SerializeField] private float _onOffTime = 0.2f;
    [SerializeField] private float _disappearTime = 2.0f;

    // *** VARIABLES *** //
    private ButtonSpriteManager _buttonManager; // Button manager object
    private TextMeshProUGUI _startText; // Start text object
    private Image _startButton; // Start button image button
    private SoundEffect _startSound; // Start sound effect
    private Color _visible = new Vector4(1f, 1f, 1f, 1f); // Visible colour
    private Color _invisible = new Vector4(1f, 1f, 1f, 0f); // Invisible colour
    private string _playerDevice; // Player device string
    private bool _soundEffectPlayed; // Sound effect played boolean
    private bool _textOnScreen = false; // Text on screen boolean - initialised to false
    private bool _beginFade = false; // Begin Fade boolean - initialised to false
    private bool _canPressButton = false; // Can press button initialised to false
    private bool _buttonPressed = false; // Button pressed boolean - initialised to false
    private float _waitTimer = 0f; // Wait timer initialised to 0 seconds
    private float _fadeTimer = 0f; // Fade timer initialised to 0 seconds
    private float _textTimer = 0f; // Text timer initialised to 0 seconds
    private float _rapidTextTimer = 0f; // Rapid text timer initialised to 0 seconds
    private float _disappearTimer = 0f; // Disappear timer initialised to 0 seconds

    // Start is called before the first frame update
    void Awake()
    {
        // Obtain the control scheme from the game scene
        _controlScheme = GameObject.FindObjectOfType<ControlScheme>();

        // Obtain the button manager
        _buttonManager = GameObject.FindObjectOfType<ButtonSpriteManager>();

        // Obtain the text mesh component
        _startText = GetComponent<TextMeshProUGUI>();

        // Set the text alpha to 0 seconds
        _startText.alpha = 0;

        // Obtain the start button image component
        _startButton = GameObject.Find("StartButtonIcon").GetComponent<Image>();

        // Set the start button colour to the invisible colour
        _startButton.color = _invisible;

        // Obtain the start button sound effect
        _startSound = GameObject.Find("StartPressed").GetComponent<SoundEffect>();

        _soundEffectPlayed = false;
    }

    /*
     * UPDATE METHOD
     * 
     * Method is invoked at each frame.
     * 
     * Method handles how the "press start" text
     * appears on screen.
     */
    private void Update()
    {
        // Check if the text is not on screen
        if(!_textOnScreen)
        {
            // Check if the fade sequence has not begun yet
            if (!_beginFade)
            {
                // Increment the wait timer by delta time
                _waitTimer += Time.deltaTime;

                // Check if the wait timer has exceeded, or is equal to, the wait tim
                if (_waitTimer >= _waitTime)
                {
                    // Set begin fade to true
                    _beginFade = true;
                }
            }
            else
            {
                // Update the start button icon
                UpdateIcon();

                // Increment the fade in timer
                _fadeTimer += Time.deltaTime;

                // Determine the alpha value based on the value of the timer divided by the fade time
                float alpha = _fadeTimer / _fadeTime;

                // Set the alpha value of the start text to the alpha value determined
                _startText.alpha = alpha;

                // Set the alpha value of the start button image to the new colour with the determined alpha value
                _startButton.color = new Vector4(1f, 1f, 1f, alpha);

                // Check if the fade timer exceeds, or is equal to, the fade time
                if (_fadeTimer >= _fadeTime)
                {
                    // Set the value of the start text to 1f
                    _startText.alpha = 1f;

                    // Set the start button image colour to visible
                    _startButton.color = _visible;

                    // Set the text on screen boolean to true
                    _textOnScreen = true;

                    // Set the can press button boolean to true
                    _canPressButton = true;
                }
            }
        }
        else
        {
            // Check if the button has not been pressed
            if(!_buttonPressed)
            {
                // Update the start button icon
                UpdateIcon();

                // Flash the text on screen
                FlashText();
            }
            else
            {
                // Invoke the rapid flash method
                RapidFlashText();

                // Increment the disappear timer
                _disappearTimer += Time.deltaTime;

                // Check if the disappear timer has exceeded 
                if (_disappearTimer > _disappearTime)
                {
                    // Invoke action to make main menu appear
                    SceneManager.LoadScene("GameScene");
                }
            }
        }
    }

    /*
     * START GAME METHOD
     * 
     * Invoked when the player presses and
     * input button.
     * 
     * When invoked, context is checked to
     * see if it has just been performed
     * and if true, sets the button pressed
     * boolean to true.
     */
    public void StartGame(InputAction.CallbackContext context)
    {
        // Check if the context has been performed
        if(context.performed && _canPressButton)
        {
            // Set button pressed to true
            _buttonPressed = true;

            if(!_soundEffectPlayed)
            {
                // Play the start button sound effect
                _startSound.PlaySoundEffect();

                // Set the sound effect played boolean to true
                _soundEffectPlayed = true;
            }
        }
    }

    /*
     * UPDATE ICON METHOD
     * 
     * Method is used to update the start
     * button text icon, depending on the device
     * that the user is currently using.
     */
    private void UpdateIcon()
    {
        // Obtain the players device
        _playerDevice = _controlScheme.PlayerControlScheme;

        // Check if the player device is a gamepad
        if(_playerDevice == "Gamepad")
        {
            // Set the sprite to the A button sprite
            _startButton.sprite = _buttonManager.ReturnGamepadSprite(0, true);
        }
        // Check if the player device is the mouse
        else if(_playerDevice == "Mouse")
        {
            // Set the sprite to the LMB button sprite
            _startButton.sprite = _buttonManager.ReturnMouseSprite(0, true);
        }
        else if(_playerDevice == "Keyboard")
        {
            // Set the button to the Space bar sprite
            _startButton.sprite = _buttonManager.ReturnKeyboardSprite(0, true);
        }
    }

    /*
     * FLASH TEXT METHOD
     * 
     * When invoked, the method will flash
     * the text on the screen at a steady
     * speed define by the on screen time
     * and off screen time variables.
     */
    private void FlashText()
    {
        // Invoke the flash text method
        _textTimer += Time.deltaTime;

        // Check if the text timer is less than the flash on time
        if (_textTimer <= _flashOnTime)
        {
            // Set the text alpha to 1f
            _startText.alpha = 1f;

            // Set the start button image colour to visible
            _startButton.color = _visible;
        }
        // Check if the text timer is greater than, or equal to, the sum of the flash on time and flash off time
        else if (_textTimer >= _flashOnTime + _flashOffTime)
        {
            // Reset the text timer
            _textTimer = 0f;
        }
        else
        {
            // Set the start text alpha to 0f
            _startText.alpha = 0f;

            // Set the start button image colour to invisible
            _startButton.color = _invisible;
        }
    }

    /*
     * RAPID FLASH TEXT METHOD
     * 
     * When invoked, the method rapidly 
     * flashes the text on the screen to indicate
     * that the game has started.
     */
    private void RapidFlashText()
    {
        // Invoke the flash text method
        _rapidTextTimer += Time.deltaTime;

        // Check if the rapid text timer is less than, or equal to, the on off timer
        if (_rapidTextTimer <= _onOffTime)
        {
            // Set the start text alpha to 1f
            _startText.alpha = 1f;

            // Set the start button image to visible
            _startButton.color = _visible;
        }
        // Check if the rapid text timer is greater than, or equal to, twice the on off time
        else if (_rapidTextTimer >= 2 * _onOffTime)
        {
            // Reset the rapid text timer to 0 seconds
            _rapidTextTimer = 0f;
        }
        else
        {
            // Set the start text alpha to 0f
            _startText.alpha = 0f;

            // Set the button colour to invisible
            _startButton.color = _invisible;
        }
    }
}