using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteManager : MonoBehaviour
{
    [Header("Gamepad Sprites - White")]
    [SerializeField] private Sprite[] _gamepadButtonsWhite;

    [Header("Gamepad Sprites - Black")]
    [SerializeField] private Sprite[] _gamepadButtonsBlack;

    [Header("Keyboard Sprites - White")]
    [SerializeField] private Sprite[] _keyboardButtonsWhite;

    [Header("Keyboard Sprites - Black")]
    [SerializeField] private Sprite[] _keyboardButtonsBlack;

    [Header("Mouse Sprites - White")]
    [SerializeField] private Sprite[] _mouseButtonsWhite;

    [Header("Mouse Sprites - Black")]
    [SerializeField] private Sprite[] _mouseButtonsBlack;


    public Sprite ReturnGamepadSprite(int index, bool white)
    {
        if(white == true)
        {
            return _gamepadButtonsWhite[index];
        }
        else
        {
            return _gamepadButtonsBlack[index];
        }
    }

    public Sprite ReturnKeyboardSprite(int index, bool white)
    {
        if (white == true)
        {
            return _keyboardButtonsWhite[index];
        }
        else
        {
            return _keyboardButtonsBlack[index];
        }
    }

    public Sprite ReturnMouseSprite(int index, bool white)
    {
        if (white == true)
        {
            return _mouseButtonsWhite[index];
        }
        else
        {
            return _mouseButtonsBlack[index];
        }
    }
}
