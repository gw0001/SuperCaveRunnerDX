/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 08/02/22                      */
/* LAST MODIFIED - 08/02/22                */
/* ======================================= */
/* CONTROL SCEHEME                         */
/* ControlScheme.cs                        */
/* ======================================= */
/* Script is used to obtain the control    */
/* scheme and store the name as a string.  */
/* ======================================= */

// Directives
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlScheme : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput; // Player Input component
    [SerializeField] private string _playerControlScheme; // Player control scheme string

    /*
     * PLAYER CONTROL SCHEME GET METHOD
     * 
     * Method returns the value held by 
     * the player control sceheme variable.
     */
    public string PlayerControlScheme
    {
        get
        {
            return _playerControlScheme;
        }
    }

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is first
     * awoken.
     * 
     * Method simply obtains the player input
     * from the game scene.
     */
    void Awake()
    {
        // Obtain the player input controller
        _playerInput = GetComponent<PlayerInput>();

        // Obtain the control scheme
        _playerControlScheme = _playerInput.currentControlScheme;
    }

    // Update is called once per frame
    void Update()
    {
        // Obtain the control scheme based on the player's current control scheme
        _playerControlScheme = _playerInput.currentControlScheme;
    }
}
