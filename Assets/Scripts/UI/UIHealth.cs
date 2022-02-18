/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 10/02/22                      */
/* LAST MODIFIED - 14/02/22                */
/* ======================================= */
/* UI HEALTH                               */
/* UIHealth.cs                             */
/* ======================================= */
/* Script controls the behaviour of the    */
/* UI elements to display the players      */
/* health to the screen.                   */
/* ======================================= */

// Directives
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHealth : MonoBehaviour
{
    // *** SERIALISED UI SPEED SETTINGS *** //
    [Header("Speed UI Settings")]
    [SerializeField] private Image _heartImage; // Arrow image
    [SerializeField] private float _heartSpacing = 50f; // Arrow spacing
    [SerializeField] private float _xOffsetFromLabel = 1f; // Spacing distance from the health label
    [SerializeField] private float _yOffset = 7f; // Y offset
    [SerializeField] private int _numberOfIcons = 3; // Number of icons
    [SerializeField] private int _maxNumberOfIcons = 15; // Maximum number of icons

    [Header("Arrow Sprites")]
    [SerializeField] private Sprite _emptyHeart; // Empty arrow sprite
    [SerializeField] private Sprite _fullHeart; // Full arrow sprite

    //*** VARIABLES ***//
    private Image[] _hearts; // Arrow image array
    private TextMeshProUGUI _speedText; // Speed label text object
    private PlayerController _player; // Player object
    private Vector2 _healthTextEnd; // Speed label text end coordinates

    /*
     * AWAKE METHOD
     * 
     * Invoked when the script is first woken up.
     * 
     * When invoked, the method obtains the player
     * and speed label text from the scene. The
     * coordinates at the end of the speed label
     * text, then the method to initialise the
     * arrow array is then invoked.
     */
    private void Awake()
    {
        // Obtain the player from the scene
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        // Obtain the speed text TMPro component
        _speedText = GetComponent<TextMeshProUGUI>();

        // Obtain the position at the end of the health text
        _healthTextEnd = new Vector2((GetComponent<RectTransform>().anchoredPosition.x + GetComponent<RectTransform>().rect.width / 2f) + _xOffsetFromLabel, (GetComponent<RectTransform>().anchoredPosition.y / 2f - _yOffset));

        // Initialise the heart array
        InitialiseHeartArray();
    }

    /*
     * INITIALISE ARROW ARRAY METHOD
     * 
     * When invoked, the method first initialises
     * the arrows to the 
     */
    private void InitialiseHeartArray()
    {
        // Initialise the arrow image array
        _hearts = new Image[_numberOfIcons];

        // Iterate over each element of the arrow array
        for (int i = 0; i < _numberOfIcons; i++)
        {
            // Set the Image at index i to an instance of the arrow image
            _hearts[i] = Instantiate(_heartImage);

            // Obtain the rect transform
            RectTransform rectTransform = _hearts[i].GetComponent<RectTransform>();

            // Set the parent to the parent the script is attatched to 
            rectTransform.SetParent(transform);

            // Set the scale
            rectTransform.localScale = new Vector3(1f, 1f, 1f); // Set the scale

            // Set the position of the rect transform
            rectTransform.anchoredPosition = new Vector2((_healthTextEnd.x + (i * _heartSpacing)), _healthTextEnd.y);

            // Set the image of the sprite to an empty arrow
            _hearts[i].sprite = _emptyHeart;
        }
    }

    /*
     * FIXED UPDATE METHOD
     * 
     * Method is invoked at regular time intervals.
     * Invokes the method to display the health
     * to the screen.
     */
    private void FixedUpdate()
    {
        // Display the player velocity to the screen
        DisplayPlayerHealth();
    }

    /*
     * DISPLAY PLAYER HEALTH METHOD
     * 
     * Method is used to control how the player
     * health is displayed to the screen.
     */
    private void DisplayPlayerHealth()
    {
        // Iterate over all elements of the hearts array
        for (int i = 0; i < _numberOfIcons; i++)
        {
            // Check if the index number is less than, or equal to, the player health minus 1
            if(i <= _player.Health - 1)
            {
                // Setto the full heart sprite
                _hearts[i].sprite = _fullHeart;
            }
            else
            {
                // Setto the empty heart sprite
                _hearts[i].sprite = _emptyHeart;
            }
        }
    }

    /*
     * INCREASE ICONS METHOD
     * 
     * Method is used to increase the number of 
     * icons on the screen.
     */
    private void IncreaseIcons(int aNumber)
    {
        // Increment the number of icons by a number
        _numberOfIcons += aNumber;

        // Check if the number of icons is greater than the maximum number of icons
        if (_numberOfIcons > _maxNumberOfIcons)
        {
            // Set the number of icons to the maximum number of icons
            _numberOfIcons = _maxNumberOfIcons;
        }

        // Reinitialise the heart array
        InitialiseHeartArray();

        // Display the health to the screen after the new update
        DisplayPlayerHealth();
    }

    /*
     * DECREASE ICONS METHOD
     * 
     * Method is used to decrease the number
     * of icons displayed on screen by a number.
     */
    private void DecreaseIcons(int aNumber)
    {
        // Decrement the number of icons by the value passed through the argument
        _numberOfIcons -= aNumber;

        // Check if the number of icons is now less than one
        if (_numberOfIcons < 1)
        {
            // Set the number of icons to 1
            _numberOfIcons = 1;
        }

        // Initialise the arrow array
        InitialiseHeartArray();

        // Display the player velocity
        DisplayPlayerHealth();
    }
}
