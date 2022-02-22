/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 16/02/22                      */
/* LAST MODIFIED - 22/02/22                */
/* ======================================= */
/* LIGHT EMITTER                           */
/* LightEmitter.cs                         */
/* ======================================= */
/* Script for the light emitter component  */
/* of the light gate the player can        */
/* encounter.                              */
/* ======================================= */

// Directives
using UnityEngine;

public class LightEmitter : MonoBehaviour
{
    // *** VARIABLES *** //
    private SpriteRenderer _emitterSpriteRenderer; // Emitter sprite renderer
    private float _halfWidth; // Half width

    /*
     * EMITTER SPRITE RENDERER GET METHOD
     * 
     * Method returns the sprite renderer used
     * for the emitter.
     */
    public SpriteRenderer EmitterSpriteRenderer
    {
        get
        {
            return _emitterSpriteRenderer;
        }
    }

    /*
     * HALF WIDTH GET METHOD
     * 
     * Method returns the value held
     * by the half width variable.
     */
    public float HalfWidth
    {
        get
        {
            return _halfWidth;
        }
    }

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is awoken.
     * 
     * Obtains the sprite renderer for the beam emitter
     * and determines the half width from the sprite
     * renderer.
     */
    private void Awake()
    {
        // Obtain the sprite renderer component
        _emitterSpriteRenderer = GetComponent<SpriteRenderer>();

        // Determine the half width
        _halfWidth = transform.localScale.x / 2f;
        //_halfWidth = _emitterSpriteRenderer.size.x / 2f;
    }
}
