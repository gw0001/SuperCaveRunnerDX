/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 04/02/22                      */
/* LAST MODIFIED - 10/02/22                */
/* ======================================= */
/* BACKGROUND PARALLAX MANAGER             */
/* FileName.cs                             */
/* ======================================= */
/* Script handles the behaviour of the     */ 
/* background parallax manager, which is   */ 
/* used to manage the creation of          */
/* background parallax objects.            */
/* ======================================= */

// Directives
using UnityEngine;

public class BackgroundParallaxManager : MonoBehaviour
{
    // *** SERIALIZED BACKGROUND PARALLAX MANAGER SETTINGS *** //
    [Header ("Background Parallax Manager Settings")]
    [SerializeField] private float _depth; // Depth
    [SerializeField] private GameObject[] _backgrounds; // Backgrounds array
    [SerializeField] private float _parallaxOffset = 2f; // Parallax offset value

    // *** VARIABLES *** //
    private int _numberOfObjects; // Number of objects in array

    /*
     * DEPTH GET METHOD
     * 
     * Method returns the value held
     * by the depth variable.
     */
    public float Depth
    {
        get
        {
            return _depth;
        }
    }

    /*
     * NUMBER OF OBJECTS GET METHOD
     * 
     * Method returns the value held
     * by the number of objects
     * variable.
     */
    public int NumberOfObjects
    {
        get
        {
            return _numberOfObjects;
        }
    }

    /*
     * PARALLAX OFFSET METHOD
     * 
     * Method returns the value held by
     * the parallax offset variable.
     */
    public float ParallaxOffset
    {
        get
        {
            return _parallaxOffset;
        }
    }

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is
     * initialised.
     * 
     * When invoked, the method simply 
     * stores the length of the backgrounds
     * array
     */
    private void Awake()
    {
        // Set the number of objects from the length of the array
        _numberOfObjects = _backgrounds.Length;
    }

    /*
     * RETURN BACKGROUND METHOD
     * 
     * Method takes in an integer as an index 
     * and returns the game object held at
     * the index of the background array.
     */
    public GameObject ReturnBackground(int index)
    {
        return _backgrounds[index];
    }
}