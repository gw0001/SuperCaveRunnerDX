/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 12/02/22                      */
/* LAST MODIFIED - 18/02/22                */
/* ======================================= */
/* BUTTON SPRITE MANAGER                   */
/* ButtonSpriteManager.cs                  */
/* ======================================= */
/* Script manages the button sprites       */
/* depending on the input device used by   */
/* the player.                             */
/* ======================================= */

// Directives
using UnityEngine;

public class ButtonSpriteManager : MonoBehaviour
{
    // *** SERIALISED ARRAYS *** //
    [Header ("Button Sprites")]
    [Header("Gamepad Sprites - White")]
    [SerializeField] private Sprite[] _gamepadButtonsWhite; // Gamepad buttons (white)

    [Header("Gamepad Sprites - Black")]
    [SerializeField] private Sprite[] _gamepadButtonsBlack; // Gamepad buttons (black)

    [Header("Keyboard Sprites - White")]
    [SerializeField] private Sprite[] _keyboardButtonsWhite; // Keyboard buttons (white)

    [Header("Keyboard Sprites - Black")]
    [SerializeField] private Sprite[] _keyboardButtonsBlack; // Keyboard buttons (black)

    [Header("Mouse Sprites - White")]
    [SerializeField] private Sprite[] _mouseButtonsWhite; // Mouse buttons (white)

    [Header("Mouse Sprites - Black")]
    [SerializeField] private Sprite[] _mouseButtonsBlack; // Mouse buttons (black)

    /*
     * RETURN GAMEPAD SPRITE METHOD
     * 
     * Method returns the button sprite
     * from the appropriate gamepad 
     * sprite array.
     */
    public Sprite ReturnGamepadSprite(int index, bool white)
    {
        // Check if white is true
        if(white == true)
        {
            // Return the white gamepad sprite at the index value
            return _gamepadButtonsWhite[index];
        }
        else
        {
            // Return the black gamepad sprite at the index value 
            return _gamepadButtonsBlack[index];
        }
    }

    /*
     * RETURN KEYBOARD SPRITE METHOD
     * 
     * Method returns the button sprite
     * from the appropriate keyboard 
     * sprite array.
     */
    public Sprite ReturnKeyboardSprite(int index, bool white)
    {
        // Check if white is true
        if (white == true)
        {
            // Return the white keyboard sprite at the index value
            return _keyboardButtonsWhite[index];
        }
        else
        {
            // Return the black keyboard sprite at the index value
            return _keyboardButtonsBlack[index];
        }
    }

    /*
     * RETURN MOUSE SPRITE METHOD
     * 
     * Method returns the button sprite
     * from the appropriate mouse 
     * sprite array.
     */
    public Sprite ReturnMouseSprite(int index, bool white)
    {
        // Check if white is true
        if (white == true)
        {
            // Return the white mouse sprite at the index value
            return _mouseButtonsWhite[index];
        }
        else
        {
            // Return the black mouse sprite at the index value
            return _mouseButtonsBlack[index];
        }
    }
}
