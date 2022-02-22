/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 13/02/22                      */
/* LAST MODIFIED - 22/02/22                */
/* ======================================= */
/* GAME STATE                              */
/* GameState.cs                            */
/* ======================================= */
/* Script controls the state and behaviour */
/* of the game.                            */
/* ======================================= */

// Directives
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class GameState : MonoBehaviour
{
    // *** SERIALISED GAME SETTINGS *** //
    [Header ("Game Settings")]
    [SerializeField] private float _waitTime; // Wait time
    [SerializeField] private float _goTime; // Go time
    [SerializeField] private string _readyMessage = "READY..."; // Ready message for the player
    [SerializeField] private string _goMessage = "GO!!"; // Go message for the player
    [SerializeField] private int _easyDistance; // Easy distance begin
    [SerializeField] private int _mediumDistance; // Medium distance begin
    [SerializeField] private int _hardDistance; // Hard distance begin
    [SerializeField] private int _insaneDistance; // Insane distance begin
    [SerializeField] private float _resultButtonWaitTime = 2f; // Button wait time

    // *** VARIABLES *** //
    private TextMeshProUGUI _messageText; // Message UI text
    private PlayerController _player; // Player
    private PlayerInput _playerInput; // Player input
    private GameObject _resultsPanel; // Result panel
    private TextMeshProUGUI _retryText; // Retry Text
    private Image _retryButton; // Retry Button
    private TextMeshProUGUI _resultsText; // Results text
    private ResultAnalyser _resultAnalyser; // Result analyser object
    private AnnouncerManager _announcer; // Announcer object
    [SerializeField] private ControlScheme _controlScheme;
    private ButtonSpriteManager _buttonManager; // Button manager object
    private GameObject _healthUI; // Health UI game object
    private GameObject _speedUI; // Speed UI game object
    private GameObject _distanceUI; // Distance UI game object
    private float _resultButtonWaitTimer; // Button wait timer
    private float _timer; // Timer
    private bool _gameStarted; // Game started boolean
    private bool _gameEnded; // Game ended boolean
    private bool _canPressButton; // Can press button boolean
    private bool _resultsDisplayed; // Result displayed boolean
    private bool _readySoundPlayed; // Ready sound played boolean
    private bool _goSoundPlayed; // Go sound played boolean
    [SerializeField] private Difficulty _distanceDifficulty; // Game Difficulty

    // Difficulty enumerator
    public enum Difficulty
    {
        veryEasy,
        easy,
        medium,
        hard,
        insane
    }

    /*
     * GAME DIFFICULTY GET METHOD
     * 
     * Method returns the value of held by the 
     * game difficulty varaible.
     */
    public Difficulty DistanceDifficulty
    {
        get
        {
            return _distanceDifficulty;
        }
    }

    /*
     * EASY DISTANCE GET METHOD
     * 
     * Method returns the value held by the 
     * easy distance variable.
     */
    public int EasyDistance
    {
        get
        {
            return _easyDistance;
        }
    }

    /*
     * MEDIUM DISTANCE GET METHOD
     * 
     * Method returns the value held by the
     * medium distance variable.
     */
    public int MediumDistance
    {
        get
        {
            return _mediumDistance;
        }
    }

    /*
     * HARD DISTANCE GET METHOD
     * 
     * Method returns the value held by the
     * hard distance variable.
     */
    public int HardDistance
    {
        get
        {
            return _hardDistance;
        }
    }

    /*
     * INSANE DISTANCE GET METHOD
     * 
     * Method returns the value held by the 
     * insane distance variable.
     */
    public int InsaneDistance
    {
        get
        {
            return _insaneDistance;
        }
    }

    /*
     * AWAKE METHOD
     */
    void Awake()
    {
        // Obtain the control scheme from the game scene
        _controlScheme = GameObject.FindObjectOfType<ControlScheme>();

        // Obtain the button manager
        _buttonManager = GameObject.FindObjectOfType<ButtonSpriteManager>();

        // Obtain the player
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        // Obtain the player input
        _playerInput = FindObjectOfType<PlayerInput>();

        // Set the player input the to player controller action map
        _playerInput.SwitchCurrentActionMap("PlayerController");

        // Obtain the message text object
        _messageText = GameObject.Find("MessageText").GetComponent<TextMeshProUGUI>();

        // Obtain the reults panel
        _resultsPanel = GameObject.Find("ResultPanel");

        // Obtain the results text
        _resultsText = GameObject.Find("ResultText").GetComponent<TextMeshProUGUI>();

        // Obtain the retry text
        _retryText = GameObject.Find("RetryText").GetComponent<TextMeshProUGUI>();

        // Obtain the retry button
        _retryButton = GameObject.Find("RetryButtonIcon").GetComponent<Image>();

        // Set the alpha of the reults text to 0f
        _retryText.alpha = 0f;

        // Set the retry button to colour with alpha set to 0f
        _retryButton.color = new Vector4(_retryButton.color.r, _retryButton.color.g, _retryButton.color.b, 0f);

        // Set the results panel to inactive
        _resultsPanel.SetActive(false);

        // Obtain the result analyser
        _resultAnalyser = GameObject.FindObjectOfType<ResultAnalyser>();

        // Obtain the announcer
        _announcer = GameObject.FindObjectOfType<AnnouncerManager>();

        // Obtain the health UI element
        _healthUI = GameObject.Find("HealthLabel");

        // Obtain the speed UI element
        _speedUI = GameObject.Find("SpeedLabel");

        // Obtain the distance text UI element
        _distanceUI = GameObject.Find("DistanceText");

        // Set the ready sound effect played to false
        _readySoundPlayed = false;

        // Set the go sound played to false
        _goSoundPlayed = false;

        // Set the message text to the ready message
        _messageText.text = _readyMessage;

        // Set the timer to 0 seconds
        _timer = 0f;

        // Set the result button wait timer to 0 seconds
        _resultButtonWaitTimer = 0f;

        // Set the game started boolean to false
        _gameStarted = false;

        // Set the game enabled boolean to false
        _gameEnded = false;

        // Set the results displayed to false
        _resultsDisplayed = false;

        // Set can press button to false
        _canPressButton = false;
    }

    /*
     * FIXED UPDATE METHOD
     * 
     * Method is invoked at fixed time intervals.
     */
    private void FixedUpdate()
    {
        // Set the difficulty
        _distanceDifficulty = SetDifficulty();

        // Check if the game has not started
        if(!_gameStarted)
        {
            // Increment the timer by delta time
            _timer += Time.fixedDeltaTime;

            // Check if the ready sound hasn't been played
            if(!_readySoundPlayed)
            {
                // Stop any audio the announcer may be saying
                _announcer.GameAnnouncer.Stop();

                // Play the "Ready" sound effect from the announcer
                _announcer.GameAnnouncer.PlaySound(3);

                // Set the ready sound played boolean to true
                _readySoundPlayed = true;
            }

            // Check if the timer is greater than, or equal to, the wait time
            if (_timer >= _waitTime)
            {
                // Start the game
                _player.GameStart();

                // Display the go message
                _messageText.text = _goMessage;

                // Check if the go sound hasn't been played
                if (!_goSoundPlayed)
                {
                    // Stop any audio the announcer may be saying
                    _announcer.GameAnnouncer.Stop();

                    // Play the "Go" sound effect from the announcer
                    _announcer.GameAnnouncer.PlaySound(4);

                    // Set the go sound played boolean to true
                    _goSoundPlayed = true;
                }
            }

            // Check if the timer 
            if(_timer >= _waitTime + _goTime)
            {
                // Hide the go message
                _messageText.enabled = false;

                // Set the game started boolean to true
                _gameStarted = true;
            }
        }
        // Else, check if the game has ended
        else if(_gameEnded)
        {
            // Check if the results cannot be displayed
            if(!_resultsDisplayed)
            {
                // Disable the game UI elements
                DisableUIElements();

                // Enable the UI controller action map
                _playerInput.SwitchCurrentActionMap("UIController");

                // Set the panel as active
                _resultsPanel.SetActive(true);

                // Set the text of the results
                _resultsText.text = _resultAnalyser.ResultAnalysis();

                // Set results displayed boolean to true
                _resultsDisplayed = true;
            }

            // Check if the button can be pressed
            if(!_canPressButton)
            {
                // Increment the button wait timer
                _resultButtonWaitTimer += Time.fixedDeltaTime;

                // Check if the button timer is greater than, or equal to, 
                if(_resultButtonWaitTimer >= _resultButtonWaitTime)
                {
                    // Will need to enable text to tell the player to press a button to reset the game
                    _retryText.alpha = 1f;

                    // Set the retry button to the a colour with alpha value of 1f
                    _retryButton.color = new Vector4(_retryButton.color.r, _retryButton.color.g, _retryButton.color.b, 1f);

                    // Set can press button to true
                    _canPressButton = true;
                }
            }
        }
        else
        {
            // Check the players status
            CheckPlayer();
        }
    }

    private void Update()
    {
        // Obtain the button sprite to use
        UpdateIcon();
    }

    /*
     * CHECK PLAYER METHO
     * 
     * Method checks the player state when invoked.
     * Is the player is found to be dead, the game
     * ended boolean is set to true.
     */
    private void CheckPlayer()
    {
        // Check if player is dead
        if(_player.PlayerStatus == PlayerController.PlayerState.dead)
        {
            // Set game ended to true
            _gameEnded = true;
        }
    }

    /*
     * RESET GAME METHOD
     * 
     * Method is invoked when the user presses
     * the appropriate button. 
     */
    public void ResetGame(InputAction.CallbackContext context)
    {
        // Check if the context has been performed and the user can press the button
        if(context.performed && _canPressButton)
        {
            // Reload the current scene to resart the game
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    /*
     * DISABLE UI ELEMENTS METHOD
     * 
     * Method is used to disable the UI 
     * game objects from the screen.
     */
    private void DisableUIElements()
    {
        // Deactivate the health UI element
        _healthUI.SetActive(false);

        // Deactivate the speed UI element
        _speedUI.SetActive(false);

        // Deactivate the distance UI element
        _distanceUI.SetActive(false);
    }

    /*
     * SET DIFFICULTY METHOD
     * 
     * Method sets the difficulty of the game,
     * based on the distance the player has 
     * travelled. As the player travels further,
     * the difficulty increases allowing for 
     * more difficult ground objects to be 
     * generated via the ground manager.
     */
    private Difficulty SetDifficulty()
    {
        // New difficulty object initialised to intro
        Difficulty difficulty = Difficulty.veryEasy;

        // Obtain the distance as an integer from the player
        int distance = Mathf.FloorToInt(_player.Distance);

        // Check the state of the distance agains the other 
        if (distance < _easyDistance)
        {
            // Set the difficulty to very easy
            difficulty = Difficulty.veryEasy;
        }
        else if (distance >= _easyDistance && distance < _mediumDistance)
        {
            // Set the difficulty to easy
            difficulty = Difficulty.easy;
        }
        else if (distance >= _mediumDistance && distance < _hardDistance)
        {
            // Set the difficulty to medium
            difficulty = Difficulty.medium;
        }
        else if (distance >= _hardDistance && distance < _insaneDistance)
        {
            // Set the difficulty to hard
            difficulty = Difficulty.hard;
        }
        else if (distance >= _insaneDistance)
        {
            // Set the difficulty to insane
            difficulty = Difficulty.insane;
        }

        // Return difficulty
        return difficulty;
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
         string playerDevice = _controlScheme.PlayerControlScheme;

        // Check if the player device is a gamepad
        if (playerDevice == "Gamepad")
        {
            // Set the sprite to the A button sprite
            _retryButton.sprite = _buttonManager.ReturnGamepadSprite(0, true);
        }
        // Check if the player device is the mouse
        else if (playerDevice == "Mouse")
        {
            // Set the sprite to the LMB button sprite
            _retryButton.sprite = _buttonManager.ReturnMouseSprite(0, true);
        }
        else if (playerDevice == "Keyboard")
        {
            // Set the button to the Space bar sprite
            _retryButton.sprite = _buttonManager.ReturnKeyboardSprite(0, true);
        }
    }
}