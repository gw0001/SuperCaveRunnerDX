/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 07/02/22                      */
/* LAST MODIFIED - 09/02/22                */
/* ======================================= */
/* FOREGROUND PARALLAX MANAGER             */
/* ForegroundParallaxManager.cs            */
/* ======================================= */
/* Script for the parallax manager which   */
/* manages the generation of parallax      */
/* objects in a scene.                     */
/* ======================================= */

// Directives
using UnityEngine;

public class ForegroundParallaxManager : MonoBehaviour
{
    // *** SERIALIZED FOREGROUND SETTINGS *** //
    [Header ("Foreground Settings")]
    [SerializeField] private GameObject[] _foregroundObjects; // Foreground object array
    [SerializeField, Range(30f, 99f)] private float _maxTime = 45f; // Maximum time before a cavern object appears
    [SerializeField, Range(0.5f, 29.9f)] private float _minTime = 2.5f; // Minimum time before cavern object appears
    [SerializeField, Range(0.5f, 0.675f)] private float _minDepth = 0.5f; // Minimum depth to camera
    [SerializeField, Range(0.675f, 0.85f)] private float _maxDepth = 0.85f; // Maximum depth to camera

    private int _numberOfObjects; // Number of objects

    /*
     * NUMBER OF OBJECTS GET METHOD
     * 
     * Method gets and returns the value
     * held by the number of objects
     * variable
     */
    public int NumberOfObjects
    {
        get
        {
            return _numberOfObjects;
        }
    }

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is first
     * awoken.
     * 
     * When invoked, the method obtains the length
     * of the foreground objects array and stores
     * the value to the number of object variable.
     */
    private void Awake()
    {
        _numberOfObjects = _foregroundObjects.Length;
    }

    /*
     * RETURN FOREGROUND OBJECT METHOD
     * 
     * When invoked, the method takes in an
     * index integer, then finds and returns
     * the game object stored in the index
     * value of the foreground objects array.
     */
    public GameObject ReturnForegroundObject(int index)
    {
        GameObject foreground = _foregroundObjects[index];

        return foreground;
    }

    /*
     * RANDOM TIME METHOD
     * 
     * Method generates and returns a random
     * time between the minimum and maximum
     * time variables.
     */
    public float RandomTime()
    {
        return Random.Range(_minTime, _maxTime);
    }

    /*
     * RANDOM DEPTH
     * 
     * Method generates and returns a random
     * depth between the minimum and maximum
     * depth variables.
     */
    public float RandomDepth()
    {
        return Random.Range(_minDepth, _maxDepth);
    }
}
