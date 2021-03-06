/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 06/02/22                      */
/* LAST MODIFIED - 28/02/22                */
/* ======================================= */
/* OBSTACLE                                */
/* Obstacle.cs                             */
/* ======================================= */
/* Script is used to handle the obstacles  */
/* that the player will encounter in the   */
/* game.                                   */
/* ======================================= */

// Directives
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // *** SERIALISED VARIABLES *** //
    [SerializeField] private Debris[] _debrisObjects; // Debris object array

    // *** OBSTACLE VARIABLES *** //
    private PlayerController _player; // Player object
    private ScreenInfo _screenInfo; // Screen info object
    private float _halfHeight; // Half height
    private float _halfWidth; // Half width
    private int _minDebris = 3; // Minimum number of debris objects
    private int _maxDebris = 10; // Maximum number of debris objects

    /*
     * HALF HEIGHT GET METHOD
     * 
     * Method returns the value held by the
     * half height variable.
     */
    public float HalfHeight
    {
        get
        {
            return _halfHeight;
        }
    }

    /*
     * HALF WIDTH GET METHOD
     *
     * Method returns the value held by the
     * half width variable.
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
     * Method is invoked when the script is first
     * awoken.
     * 
     * Method first determines the half height and
     * half width of the object, before obtaining
     * the player and screen info objects.
     */
    private void Awake()
    {
        // Set the half height
        _halfHeight = transform.localScale.y / 2f;

        // Set the half width
        _halfWidth = transform.localScale.x / 2f;

        // Obtain the player game object
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        // Obtain the screen info
        _screenInfo = GameObject.FindObjectOfType<ScreenInfo>();
    }

    /*
     * FIXED UPDATE METHOD
     * 
     * Method is invoked at regular time
     * intervals.
     * 
     * Method determines how the object should
     * move and if it disappears off screen,
     * it is destroyed.
     */
    void FixedUpdate()
    {
        // Obtain the position of the obstacle
        Vector2 position = transform.position;

        // Alter the X position based on the players run velocity multiplied by the fixed time increment
        position.x -= _player.HorizontalVelocity * Time.fixedDeltaTime;

        // Determine the X coordinate of the right side of the obstacle
        float rightSide = transform.position.x + HalfWidth;

        // Check if the obstacle has left the screen
        if(rightSide <= _screenInfo.LeftEdge)
        {
            // Destroy the game object
            Destroy(gameObject);

            // Return to prevent unnecessary calculation
            return;
        }

        // Set the transform position
        transform.position = position;
    }

    /*
     * DESTROY OBSTACLE METHOD
     * 
     * Method adds a number of debris objects
     */
    public void DestroyObstacle(float velocity)
    {
        // Determine a the number of debris objects between the min and max debris objects
        int numberOfDebris = Random.Range(_minDebris, _maxDebris);

        // Iterate up to the number of debris objects
        for(int i = 0; i < numberOfDebris; i++)
        {
            // Random index
            int randomIndex = Random.Range(0, _debrisObjects.Length);

            // Instantiate a random debris object
            Debris debris = Instantiate(_debrisObjects[randomIndex]);

            // Determine the maximum Y position for the object to spawn
            float maxY = transform.position.y + HalfHeight;

            // Determine the minimum y position for the object to spawn
            float minY = transform.position.y;

            // Determine the minimum X position for the object to spawn
            float minX = transform.position.x - HalfWidth;

            // Determine the maximum X position for the object to spawn
            float maxX = transform.position.x + HalfWidth;

            // New empty position vector
            Vector2 position;

            // Determine the X position from the maximum and minimum X values
            position.x = Random.Range(minX, maxX);

            // Determine the Y position from the maximum and minimum Y values
            position.y = Random.Range(minY, maxY);

            // Set the transform of the debris object
            debris.transform.position = position;

            // Set the velocity of the debris object
            debris.SetVelocity(velocity);
        }

        // Destroy this game object
        Destroy(this.gameObject);
    }
}
