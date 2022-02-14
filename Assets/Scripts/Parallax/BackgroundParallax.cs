/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 07/02/22                      */
/* LAST MODIFIED - 10/02/22                */
/* ======================================= */
/* BACKGROUND PARALLAX                     */
/* BackgroundParallax.cs                   */
/* ======================================= */
/* Script handles the behaviour of the     */
/* background parallax objects.            */
/* ======================================= */

// Directives
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    // *** BACKGROUND PARALLAX SETTINGS *** //
    [Header ("Background Parallax Settings")]
    [SerializeField] private bool _hasGeneratedBackground = false; // Has generated background
    
    // *** VARIABLES *** //
    private float _depth = 1f; // Depth value - initialised to 1 to begin with
    private PlayerController _player; // Player controller
    private ScreenInfo _screenInfo; // Screen information
    private float _halfWidth; // Half width of the background object
    private BackgroundParallaxManager _parallaxManager; // Parallax Manager
    
    /*
     * HALF WIDTH
     * 
     * Method returns the value
     * held by the half width
     * variable.
     */
    public float HalfWidth
    {
        get 
        {
            return _halfWidth;
        }
    }

    /*
     * DEPTH 
     * 
     * Method is used to set
     * the depth variable to
     * a value.
     */
    public float Depth
    {
        set
        {
            _depth = value;
        }
    }

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is awoken.
     * When invoked, the player controller and screen 
     * info objects are obtained from the scene. The 
     * half width of the background object is then 
     * determined and stored.
     * 
     * The parallax manager is then obtained from
     * the parent gameobject. As this only works for
     * game objects connected to the parent, a check 
     * is then carried out to see if the parallax
     * manager exists. If it does, the depth is set
     * from the background parallax manager.
     * 
     * Background game objects that are instatiated
     * will not obtain the parallax manager via the
     * awake method. A separate method is used to
     * set the parallax manager and depth when
     * the object passes the offset screen position.
     */
    private void Awake()
    {
        // Obtain the player controller
        _player = GameObject.Find("Player").GetComponent<PlayerController>();

        // Obtain the screen information
        _screenInfo = GameObject.FindObjectOfType<ScreenInfo>();
        
        // Determine the half width of the object
        _halfWidth = transform.localScale.x / 2f;

        // Obtain the parallax manager from the parent
        _parallaxManager = GetComponentInParent<BackgroundParallaxManager>();

        // Check that the parallax manager exists
        if(_parallaxManager != null)
        {
            // Set the depth of the background object from the parallax manager
            _depth = _parallaxManager.Depth;
        }
    }

    /*
     * FIXED UPDATE
     * 
     * Method is invoked at set intervals.
     * 
     * When invoked, the velocity of the background
     * object is determined based on the players
     * current velocity and background objects 
     * depth. This velocity is then used to 
     * alter the position of the object
     * so that it travels to the left of the screen.
     * 
     * The right side of the background object and the
     * offscreen point is determined. Checks are then
     * carried out to determine if the object has
     * passed the offscreen point and the left edge of 
     * the screen.
     * 
     * If the object has passed the offscreen point and
     * has not generated a new background object, a new
     * background object is instantiated.
     * 
     * If the object has passed the left edge of the
     * screen, the background object is destroyed.
     */
    void FixedUpdate()
    {
        // Determine the velocity by obtaining the player velocity and dividing by the depth
        float velocity = _player.HorizontalVelocity / _depth;

        // Obtain the position of the object
        Vector2 position = transform.position;

        // Alter the position of the object based on the velocity multiplied by the fixed delta time
        position.x -= velocity * Time.fixedDeltaTime;

        // Set the transform position of the object
        transform.position = position;

        // Determine the right side of the background object
        float rightSide = transform.position.x + _halfWidth;

        // Determine the spawn point
        float offscreenPoint = _screenInfo.RightEdge + _parallaxManager.ParallaxOffset;

        // Whilst has generated background is false
        if(!_hasGeneratedBackground)
        {
            // Check if the right side X coordinate is less than, or equal to, the x coordinate of the exit point
            if (rightSide <= offscreenPoint)
            {
                // Generate new background
                GenerateBackground();

                // Set boolean to false
                _hasGeneratedBackground = true;
            }
        }

        // Check if the background object has exited the screen
        if(rightSide <= _screenInfo.LeftEdge)
        {
            // Destroy the game object
            Destroy(gameObject);

            // Return to prevent unnecessary calculation
            return;
        }
    }

    /*
     * GENERATE BACKGROUND METHOD
     * 
     * Method intantiates a new background object.
     * The position of the new background object
     * is altered to sit neatly to the right of
     * the current background object.
     * The method then sets the background 
     * parallax manager and depth for the new
     * background object.
     */
    private void GenerateBackground()
    {
        // Obtain a background object
        GameObject background = Instantiate(NewBackgroundObject());

        // Obtain the position of the current object
        Vector2 position = transform.position;

        // Alter the X coordinate by the half width of the current background object and new background object
        position.x += HalfWidth + background.GetComponent<BackgroundParallax>().HalfWidth;

        // Set the transform position to the position vector
        background.transform.position = position;

        // Set the parallax managager for the new background component
        background.GetComponent<BackgroundParallax>().SetBackgroundParallaxManager(_parallaxManager);

        // Set the depth of the background object
        background.GetComponent<BackgroundParallax>().Depth = _parallaxManager.Depth;

        // Set the parent of the new background object to the parent of the current background object
        background.GetComponent<BackgroundParallax>().SetParent(transform.parent);
    }

    /*
     * NEW BACKGROUND OBJECT METHOD
     * 
     * When invoked, a random index number
     * is generated, then used to obtain
     * and return background prefab from the 
     * background parallax manager.
     */
    private GameObject NewBackgroundObject() 
    {
        // Obtain a random index
        int index = Random.Range(0, (_parallaxManager.NumberOfObjects));

        // Obtain a background from the index 
        GameObject background = _parallaxManager.ReturnBackground(index);

        // Return the background object
        return background;
    }

    /*
     * SET PARALLAX MANAGER METHOD
     * 
     * Method is used to set the parallax manager
     * object when invoked.
     */
    public void SetBackgroundParallaxManager(BackgroundParallaxManager parallaxManager)
    {
        _parallaxManager = parallaxManager;
    }

    /*
     * SET PARENT METHOD
     * 
     * When invoked, the method sets the parent
     * of the ground object to the parent
     * in the argument.
     */
    public void SetParent(Transform parent)
    {
        transform.parent = parent;
    }
}
