/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 16/02/22                      */
/* LAST MODIFIED - 17/02/22                */
/* ======================================= */
/* LIGHT BEAM                              */
/* LightBeam.cs                            */
/* ======================================= */
/* Script for the light beam of the light  */
/* gate component.                         */
/* ======================================= */

// Directives
using UnityEngine;

public class LightBeam : MonoBehaviour
{
    // *** VARIABLES *** //
    private SpriteRenderer _beamSpriteRenderer; // Beam sprite renderer
    private bool _isColourOne; // Is colour one boolean

    /*
     * BEAM SPRITE RENDERER
     * 
     * Method is used to return the 
     * sprite renderer used for the 
     * light beam.
     */
    public SpriteRenderer BeamSpriteRenderer
    {
        get
        {
            return _beamSpriteRenderer;
        }
    }

    /*
     * IS COLOUR ONE GET AND SET METHOD
     * 
     * Method is used to set the value
     * of the is colour one variable
     * to a boolean value, and return
     * the value held by the variable.
     */
    public bool IsColourOne
    {
        get
        {
            return _isColourOne;
        }
        set
        {
            _isColourOne = value;
        }
    }

    /*
     * AWAKE METHOD
     * 
     * Method invoked when the script is woken up.
     * Obtains the sprite renderer component.
     */
    private void Awake()
    {
        _beamSpriteRenderer = GetComponent<SpriteRenderer>();
    }
}
