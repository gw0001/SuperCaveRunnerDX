/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 10/02/22                      */
/* LAST MODIFIED - 14/02/22                */
/* ======================================= */
/* UI SPEED                                */
/* UISpeed.cs                              */
/* ======================================= */
/* Script controls the UI elements used    */
/* to display the players speed on screen. */
/* ======================================= */

// Directives
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISpeed : MonoBehaviour
{
    // *** SERIALISED UI SPEED SETTINGS *** //
    [Header("Speed UI Settings")]
    [SerializeField] private Image _arrowImage; // Arrow image
    [SerializeField] private float _arrowSpacing = 50f; // Arrow spacing
    [SerializeField] private float _yOffset = 7f; // Y offset
    [SerializeField] private int _numberOfIcons = 5; // Number of icons
    [SerializeField] private int _maxNumberOfIcons = 15; // Maximum number of icons

    [Header("Arrow Sprites")]
    [SerializeField] private Sprite _emptyArrow; // Empty arrow sprite
    [SerializeField] private Sprite _fullArrow; // Full arrow sprite
    [SerializeField] private Sprite _maxArrow; // Max speed arrow sprite

    //*** VARIABLES ***//
    private Image[] _arrows; // Arrow image array
    private TextMeshProUGUI _speedText; // Speed label text object
    private PlayerController _player; // Player object
    private Vector2 _speedTextEnd; // Speed label text end coordinates

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

        // Obtain the position at the end of the speed text
        _speedTextEnd = new Vector2((GetComponent<RectTransform>().anchoredPosition.x + GetComponent<RectTransform>().rect.width / 2f), (GetComponent<RectTransform>().anchoredPosition.y / 2f - _yOffset));

        // Initialise the arrow array
        InitialiseArrowArray();
    }

    /*
     * INITIALISE ARROW ARRAY METHOD
     * 
     * When invoked, the method first initialises
     * the arrows to the 
     */
    private void InitialiseArrowArray()
    {
        // Check if the array is populated
        if(_arrows != null)
        {
            foreach (Image arrow in _arrows)
            {
                // Destroy the arrow game object
                Destroy(arrow.gameObject);
            }
        }

        // Initialise the arrow image array
        _arrows = new Image[_numberOfIcons];

        // Iterate over each element of the arrow array
        for(int i = 0; i < _numberOfIcons; i++)
        {
            // Set the Image at index i to an instance of the arrow image
            _arrows[i] = Instantiate(_arrowImage);

            // Obtain the rect transform
            RectTransform rectTransform = _arrows[i].GetComponent<RectTransform>();

            // Set the parent to the parent the script is attatched to 
            rectTransform.SetParent(transform);

            // Set the scale
            rectTransform.localScale = new Vector3(1f, 1f, 1f); // Set the scale

            // Set the position of the rect transform
            rectTransform.anchoredPosition = new Vector2((_speedTextEnd.x + (i * _arrowSpacing)), _speedTextEnd.y);

            // Set the image of the sprite to an empty arrow
            _arrows[i].sprite = _emptyArrow;
        }
    }

    /*
     * FIXED UPDATE METHOD
     * 
     * Method is invoked at regular time intervals.
     * Used to display the players velocity to the screen.
     */
    private void FixedUpdate()
    {
        // Display the player velocity to the screen
        DisplayPlayerVelocity();
    }

    /*
     * DISPLAY PLAYER VELOCITY METHOD
     * 
     * Method is used to take the players velocity
     * and display it to the screen in the form
     * of arrows. A low number of arrows indicate
     * the player is moving slow, whereas a high
     * number of arrows indicates the player is
     * moving fast
     */
    private void DisplayPlayerVelocity()
    {
        // Determine the ratio between the players horizontal velocity and maximum run velocity
        float ratio = _player.HorizontalVelocity / _player.MaxRunVelocity;

        // Iterate over all 
        for (int i = 0; i < _numberOfIcons; i++)
        {
            // Determine the increment
            float increment = ((float)i / (float)_numberOfIcons);

            // Check if the index number is less than the index nubmer of the final icon
            if (i < _numberOfIcons - 1)
            {
                // Check if the ratio is greater than the increment + 10%
                if (ratio > increment + 0.1f)
                {
                    // Set the sprite to the full arrow
                    _arrows[i].sprite = _fullArrow;
                }
                else
                {
                    // Set the sprite to the empty arrow
                    _arrows[i].sprite = _emptyArrow;
                }
            }
            // Else, the final arrow icon is used
            else
            {
                // Check if the increment is greater than the ratio + 5%
                if (ratio > increment + 0.05f)
                {
                    // Set the sprite to the maximum speed sprite
                    _arrows[i].sprite = _maxArrow;
                }
                else
                {
                    // Set the sprite to the empty arrow
                    _arrows[i].sprite = _emptyArrow;
                }
            }
        }
    }

    /*
     * INCREASE ICONS METHOD
     * 
     * Method is used to increase the number
     * of icons on screen by a number.
     */
    private void IncreaseIcons(int aNumber)
    {
        // Increment the number of icons by the value passed through the argument
        _numberOfIcons += aNumber;

        // Check if the number of icons exceeds the maximum number of icons
        if(_numberOfIcons > _maxNumberOfIcons)
        {
            // Set the number of icons to the maximum number of icons
            _numberOfIcons = _maxNumberOfIcons;
        }

        // Initialise the arrow array
        InitialiseArrowArray();

        // Display the player velocity to the screen
        DisplayPlayerVelocity();
    }

    /*
     * DECREASE ICONS METHOD
     * 
     * Method is used to decrease the speed
     * icons on the screen by a number.
     */
    private void DecreaseIcons(int aNumber)
    {
        // Decrement the number of icons by the value passed through the argument
        _numberOfIcons -= aNumber;

        // Check if the number of icons is now less than one
        if (_numberOfIcons < 5)
        {
            // Set the number of icons to 1
            _numberOfIcons = 5;
        }

        // Initialise the arrow array
        InitialiseArrowArray();

        // Display the player velocity
        DisplayPlayerVelocity();
    }
}