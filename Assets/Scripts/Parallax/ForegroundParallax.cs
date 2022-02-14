/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 07/02/22                      */
/* LAST MODIFIED - 08/02/22                */
/* ======================================= */
/* FOREGROUND PARALLAX                     */
/* ForegroundParallax.cs                   */
/* ======================================= */
/* Script for the managing the behaviour   */
/* of foreground parallax objects.         */
/* ======================================= */

// Directives
using UnityEngine;

public class ForegroundParallax : MonoBehaviour
{
    private PlayerController _player; // Player controller
    private ForegroundParallaxManager _parallaxManager; // Parallax manager
    private ScreenInfo _screenInfo; // Screen info
    private float _appearTimer; // Appear timer
    private float _halfWidth; // Half width
    private float _depth; // Depth
    private bool _hasLeftScreen; // Has left screen boolean

    /*
     * APPEAR TIMER SET METHOD
     * 
     * Method sets the appear timer
     * to a float value.
     */
    public float AppearTimer
    {
        set
        {
            _appearTimer = value;
        }
    }

    /*
     * DEPTH SET METHOD
     * 
     * Method is used to set the depth
     * to a float value.
     */
    public float Depth
    {
        set
        {
            _depth = value;
        }
    }

    /*
     * HALF WIDTH GET METHOD
     * 
     * Method obtains and returns the 
     * half width value.
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
     * Method is invoked when the script is
     * first woken up.
     * 
     * When invoked, the "Has Left Screen" 
     * boolean is set to true. The player 
     * controller and screen info components
     * are then obtained from the screen. 
     * The half width of the game object is
     * then determined.
     * 
     * If this is the first foreground game
     * object, the parallax manager is 
     * determined. If the parallax manager
     * exists, the depth and the appear times
     * are randomly set.
     * 
     * The latter part of this method will not
     * work for newly instanced foreground
     * objects as they will not have a parent.
     * A seperate method is used to to set the 
     * parallax manager.
     */
    private void Awake()
    {
        // Set the "has left screen" boolean to false
        _hasLeftScreen = false;

        // Obtain the player controller
        _player = GameObject.Find("Player").GetComponent<PlayerController>();

        // Obtain the screen info object
        _screenInfo = GameObject.FindObjectOfType<ScreenInfo>();

        // Determine the half width of the object
        _halfWidth = transform.localScale.x / 2;

        // Obtain the parallax manager from the parent
        _parallaxManager = GetComponentInParent<ForegroundParallaxManager>();

        // Check that the parallax manager exists
        if(_parallaxManager != null)
        {
            // Set the depth
            Depth = _parallaxManager.RandomDepth();

            // Set the appear timer
            AppearTimer = _parallaxManager.RandomTime();
        }
    }

    /*
     * FIXED UPDATE METHOD
     * 
     * Method is invoked at set time intervals.
     * 
     * When invoked, the method first decrements
     * the appear timer by the fixed delta time.
     * 
     * A check is then carried out to see if the 
     * timer is less than or equal to 0. If so,
     * calculations are then carried out to 
     * determine the position of the game object
     * at the current fixed frame update.
     * 
     * A check is then carried out to determine
     * if the foreground object has left the 
     * screen. If it has not, a check is carried
     * out to determine has gone beyond the left
     * side of the screen. If it has, a new
     * foreground object is generated, then the 
     * "has left screen" boolean is set to true.
     * 
     * If the "has left screen" boolean is true,
     * the game object is then destroyed.
     */
    void FixedUpdate()
    {
        // Decrement the appear timer by fixed delta time
        _appearTimer -= Time.fixedDeltaTime;

        // Check if the timer is less than, or equal to, 0 seconds
        if(_appearTimer <= 0f)
        {
            // Determine the velocity by obtaining the player velocity and dividing by the depth
            float velocity = _player.HorizontalVelocity / _depth;

            // Obtain the position of the object
            Vector2 position = transform.position;

            // Alter the position of the object based on the velocity multiplied by the fixed delta time
            position.x -= velocity * Time.fixedDeltaTime;

            // Set the transform position of the object
            transform.position = position;

            // Determine the X coordinate right side of the foreground object
            float rightSide = transform.position.x + _halfWidth;

            // Whilst has generated background is false
            if (!_hasLeftScreen)
            {
                // Check if the right side X coordinate is less than, or equal to, the x coordinate of the exit point
                if (rightSide <= _screenInfo.LeftEdge)
                {
                    // Generate new background
                    GenerateForeground();

                    // Set boolean to false
                    _hasLeftScreen = true;
                }
            }

            // Check if the background object has exited the screen
            if (_hasLeftScreen)
            {
                // Destroy the game object
                Destroy(gameObject);
            }
        }
    }

    /*
     * GENERATE FOREGROUND METHOD
     * 
     * When invoked, the method first creates an 
     * instance of a foreground object.
     * 
     * The X component of the new objects position
     * is first set to appear to the right of the 
     * screen and out of frame.
     * 
     * The method then sets the parallax manager,
     * assigns a random depth, the assigns a random
     * time.
     */
    private void GenerateForeground()
    {
        // Create an instance of a foreground object
        GameObject foreground = Instantiate(NewForegroundObject());

        // Obtain the position of the current object
        Vector2 position = Vector2.zero;

        // Alter the X coordinate by the half width of the new foreground object and the right edge of the screen
        position.x = foreground.GetComponent<ForegroundParallax>().HalfWidth + _screenInfo.RightEdge;

        // Set the transform position to the position vector
        foreground.transform.position = position;

        // Set the parallax managager for the new foreground object
        foreground.GetComponent<ForegroundParallax>().SetForegroundParallaxManager(_parallaxManager);

        // Set the depth of the new foreground object
        foreground.GetComponent<ForegroundParallax>().Depth = _parallaxManager.RandomDepth();

        // Set the timer of the new foreground object
        foreground.GetComponent<ForegroundParallax>().AppearTimer = _parallaxManager.RandomTime();

        // Set the transform parent of the new foreground parallax object
        foreground.GetComponent<ForegroundParallax>().SetParent(transform.parent);
    }

    /*
     * NEW FOREGROUND OBJECT METHOD
     * 
     * When invoked, the method generates a random
     * index number between 0 and the maximum number
     * of objects from the parallax manager.
     * 
     * The random number is then used to obtain a
     * random game object from the parallax manager.
     */
    private GameObject NewForegroundObject()
    {
        // Obtain a random index
        int index = Random.Range(0, (_parallaxManager.NumberOfObjects));

        // Obtain a background from the index 
         return _parallaxManager.ReturnForegroundObject(index);
    }

    /*
     * SET FOREGROUND PARALLAX MANAGER
     * 
     * When invoked, the method sets the parallax manager
     * to the foreground parallax manager taken in.
     */
    public void SetForegroundParallaxManager(ForegroundParallaxManager parallaxManager)
    {
        _parallaxManager = parallaxManager;
    }

    public void SetParent(Transform parent)
    {
        transform.parent = parent;
    }
}