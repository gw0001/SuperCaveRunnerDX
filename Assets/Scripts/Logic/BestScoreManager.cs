/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 25/02/22                      */
/* LAST MODIFIED - 25/02/22                */
/* ======================================= */
/* BEST SCORE MANAGER                      */
/* BestScoreManager.cs                     */
/* ======================================= */
/* Script controls the state and behaviour */
/* of the game.                            */
/* ======================================= */

// Directives
using UnityEngine;

public class BestScoreManager : MonoBehaviour
{
    private static BestScoreManager _instance; // Best Score Instance
    private int _bestDistance = 0; // Best distance

    /*
     * BEST DISTANCE GET METHOD
     * 
     * Method returns the value held
     * by the best distance variable,
     * and allows for the value of
     * this variable to be set.
     */
    public int BestDistance
    {
        get
        {
            return _bestDistance;
        }
        set
        {
            _bestDistance = value;
        }
    }

    /*
     * INSTANCE GET METHOD
     * 
     * Method returns the instance of
     * the best score manager.
     */
    public static BestScoreManager Instance 
    { 
        get 
        { 
            return _instance; 
        } 
    }

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is
     * awoken.
     * 
     * Method determines which instance of the
     * best score manager to use
     */
    private void Awake()
    {
        // Check if instance does not equal null or instance does not equal this game object
        if (_instance != null && _instance != this)
        {
            // Destroy this game object
            Destroy(this.gameObject);
        }

        // Set instance to this game object
        _instance = this;

        // Prevent the object from being destroyed between scenes
        DontDestroyOnLoad(this.gameObject);
    }
}