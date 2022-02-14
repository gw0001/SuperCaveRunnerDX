/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 13/02/22                      */
/* LAST MODIFIED - 14/02/22                */
/* ======================================= */
/* GROUND                                  */
/* Ground.cs                               */
/* ======================================= */
/* Script manages the behaviour of ground  */
/* objects.                                */
/* ======================================= */

// Directives
using UnityEngine;

public class Ground : MonoBehaviour
{
    // *** SERIALIZED VARIABLES *** //
    [Header("Standard Ground Settings")]
    [SerializeField] private bool _canFeatureObstacles; // Can feature obstacles
    [SerializeField] private bool _willFeatureObstacles; // Will feature obstacles
    [SerializeField] private bool _canFeatureHealth;
    [SerializeField] private bool _willFeatureHealth;
    [SerializeField, Range(1, 3)] private int _minNumberOfObstacles; // Maximum number of obstacles
    [SerializeField, Range(3, 5)] private int _maxNumberOfObstacles; // Maximum number of obstacles
    [SerializeField, Range(0f, 1f)] private float _safeAreaFromLeft = 0.5f; // Safe area from the left side of the ground
    [SerializeField, Range(0f, 1f)] private float _safeAreaFromRight = 0.5f; // Safa area from the right side of the ground
    [SerializeField, Range(0f, 1f)] private float _obstacleChance = 0.5f; // The chance value for obstacles to appear on the ground
    [SerializeField, Range(0f, 1f)] private float _healthChance = 0.5f; // The chance value for health to appear on the ground

    // *** VARIABLES **** //
    private GroundManager _groundManager; // Ground manager
    private ScreenInfo _screenInfo; // Screen info
    private PlayerController _player; // Player Object
    private BoxCollider2D _collider;// Ground Collider
    private SpriteRenderer _spriteRenderer; // Ground sprite renderer
    private float _rightSide; // Right side of the ground object
    private float _groundHeight; // Ground height
    private float _halfWidth; // Ground half width
    private float _halfHeight; // Ground half height
    private bool _hasGeneratedGround = false; // Has generated ground boolean
    private bool _atMaxHeight = false; // At max height boolean
    private bool _atMinHeight = false; // At min height boolean

    /*
     * WILL FEATURE OBSTACLES GET METHOD
     * 
     * Method returns the boolean value held by
     * the will feature obstacles variable.
     */
    public bool WillFeatureObstacles
    {
        get
        {
            return _willFeatureObstacles;
        }
    }

    /*
     * WILL FEATURE HEALTH GET METHOD
     * 
     * Method returns the boolean value held by
     * the will feature health variable.
     */
    public bool WillFeatureHealth
    {
        get
        {
            return _willFeatureHealth;
        }
    }

    /*
     * SAFE AREA FROM LEFT GET METHOD
     * 
     * Method returns the float value held by
     * the safe area from left variable.
     */
    public float SafeAreaFromLeft
    {
        get
        {
            return _safeAreaFromLeft;
        }
    }

    /*
     * SAFE AREA FROM RIGHT GET METHOD
     * 
     * Method returns the float value held by
     * the safe area from right variable.
     */
    public float SafeAreaFromRight
    {
        get
        {
            return _safeAreaFromRight;
        }
    }


    /*
     * GROUND HEIGHT GET METHOD
     * 
     * Used to return the value of the
     * ground height.
     */
    public float GroundHeight
    {
        get
        {
            return _groundHeight;
        }
    }

    /*
     * HALF WIDTH GET METHOD
     * 
     * Method returns the value held by
     * the half width variable.
     */
    public float HalfWidth
    {
        get
        {
            return _halfWidth;
        }
    }

    /*
     * HALF HEIGHT GET METHOD
     * 
     * Method returns the value held by
     * the half height variable.
     */
    public float HalfHeight
    {
        get
        {
            return _halfHeight;
        }
    }


    /*
     * AT MAX HEIGHT GET METHOD
     * 
     * Method returns the value held by the
     * at max height boolean variable.
     */
    public bool AtMaxHeight
    {
        get
        {
            return _atMaxHeight;
        }
    }

    /*
     * AT MIN HEIGHT GET METHOD
     * 
     * Method returns the value held by
     * the at min height boolean variable.
     */
    public bool AtMinHeight
    {
        get
        {
            return _atMinHeight;
        }
    }

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is initialised.
     * 
     * When invoked, the screen info, ground manager, and
     * player objects are obtained from the scene. The
     * ground height of the ground object is also
     * determined.
     */
    private void Awake()
    {
        // Obtain the screen info
        _screenInfo = GameObject.FindObjectOfType<ScreenInfo>();

        // Obtain the ground manager
        _groundManager = GameObject.FindGameObjectWithTag("GroundManager").GetComponent<GroundManager>();

        // Obtain the player game object
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        // Obtain the sprite renderer
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // Otain the box collider attached to the ground object
        _collider = GetComponent<BoxCollider2D>();

        // Determine the half width from the collider
        _halfWidth = _collider.size.x / 2f;

        // Determine the half height from the collider
        _halfHeight = _collider.size.y / 2f;

        // Calculate the ground height based on the Y coordinate of the objects position and half the Y value of the collider
        CalculateGroundHeight();

        // Check the health of the player
        if(_player.Health < _player.MaxHealth)
        {
            // Set can feature health to true
            _canFeatureHealth = true;
        }
        else
        {
            // Set can feature health to false
            _canFeatureHealth = false;
        }

        // WILL NEED TO REWORK THIS BIT COMPLETELY
        // WILL NEED TO INCLUDE THE CHANCE VALUES OF THE OBSTACLES AND HEALTH PICKUPS
        // 1. DETERMINE IF THE GROUND OBJECT WILL FEATURE OBSTACLES BASED ON THE OBSTACLE CHANCE VALUE
        // 2. DETERMINE IF THE GROUND OBJECT WILL FEATURE HEALTH BASED ON THE HEALTH CHANCE
        // 3. IF OBJECT CAN FEATURE HEALTH AND OBSTACLES, DETERMINE IF ONE SHOULD OVERRIDE THE OTHER OR BOTH BE FEATURED

        // 1.
        // Check if can feature health is true
        if (_canFeatureHealth)
        {
            // Randomly determine if the ground object will generate health
            int willFeatureHealth = Random.Range(0, 2);

            // Convert integer value to boolean value
            _willFeatureHealth = willFeatureHealth == 0 ? false : true;
        }

        // 2.
        // Check if the ground object can feature obstacles
        if (_canFeatureObstacles)
        {
            // Randomly determine if the ground object will feature obstacles
            int willFeatureObstacles = Random.Range(0, 2);

            // Convert integer value to boolean value
            _willFeatureObstacles = willFeatureObstacles == 0 ? false : true;
        }

        // 3.
        if(_willFeatureHealth && _willFeatureObstacles)
        {
            // Randomly determine if the ground object will feature obstacles
            int chance = Random.Range(0, 2);

            // Check if chance is equal to 0
            if(chance == 0)
            {
                // Set will feature obstacles to true
                _willFeatureObstacles = true;

                // Set will feature health to false
                _willFeatureHealth = false;
            }
            else
            {
                // Set will feature obstacles to false
                _willFeatureObstacles = false;

                // Set will feature health to true
                _willFeatureHealth = true;
            }
        }
    }

    /*
     * FIXED UPDATE METHOD
     * 
     * Method is invoked at regular time intervals.
     * 
     * Method updates the position of the ground 
     * object. Determines if the object has
     * entered the screen, and if the object has
     * not generated a new ground component, the
     * method then invokes the method to do so.
     * 
     * The method also checks if the object has
     * exited the screen, and destroys the object.
     */
    private void FixedUpdate()
    {
        // Obtain the ground position
        Vector2 position = transform.position;

        // Determine the X coordinate in relation to the player's velocity at the fixed delta time
        position.x -= _player.HorizontalVelocity * Time.fixedDeltaTime;

        // Determine the right side of the object
        _rightSide = transform.position.x + HalfWidth;

        // Check if the ground object has exited the scene
        if (_rightSide < _screenInfo.LeftEdge)
        {
            // Destory the object
            Destroy(gameObject);

            // Return to prevent unnecessary calculation
            return;
        }

        // Check if another ground object has been generated
        if (!_hasGeneratedGround)
        {
            // Check if the ground object is fully in the gamespace
            if (_rightSide < _screenInfo.RightEdge)
            {
                // Set has generated to true to prevent further ground generation
                _hasGeneratedGround = true;

                // Invoke the generate ground method
                GenerateGround();
            }
        }

        // Set the position of the ground to the altered ground position
        transform.position = position;
    }

    /*
     * GENERATE GROUND METHOD
     * 
     * 
     */
    public void GenerateGround()
    {
        // Instantiate a copy of the game object
        GameObject ground = Instantiate(NewGroundObject());

        // Empty position vector
        Vector2 position = Vector2.zero;

        // Determine the maximum Y jump bas
        float grountToPrevGroundHeight = (GroundHeight - HalfHeight);

        // Determine the maximum jump the player can achieve if holding the jump button
        float maxHoldJumpHeight = _player.MaxJumpVelocity * _player.HoldJumpTime;

        // Determine the time it would take for the player to reach the max jump height
        float maxHeightJumpTime = _player.MaxJumpVelocity / _player.Gravity;

        // Determine the jump when gravity is taking effect
        float naturalJumpHeight = (_player.MaxJumpVelocity * maxHeightJumpTime + (0.5f * (_player.Gravity * (maxHeightJumpTime * maxHeightJumpTime))));

        // Determine the maximum jump height
        float maxJumpHeight = (maxHoldJumpHeight + naturalJumpHeight);

        // Determine the new maximum ground height by taking the current ground height and adding the maximum jump height multiplied by a jump buffer
        float newHeightMax = GroundHeight + (maxJumpHeight * _groundManager.JumpHeightBuffer);

        // Determine the new minimum ground height
        float newHeightMin = _screenInfo.BottomEdge + _groundManager.MinHeightFromBottom;

        // Determine the new ground height
        float newGroundHeight = Random.Range(newHeightMin, newHeightMax);

        // Determine the maximum ground height
        float maxGroundHeight = _screenInfo.TopEdge - _groundManager.MaxHeightFromTop;

        // Check if the new ground height is greater than the maximum ground height
        if (newGroundHeight >= maxGroundHeight)
        {
            // Set the new ground height to the maximum ground height
            newGroundHeight = maxGroundHeight;

            // Invoke the max height method for the new ground object
            ground.GetComponent<Ground>().SetToMaxHeight();
        }

        // Determine the minimum ground height
        float minGroundHeight = _screenInfo.BottomEdge + _groundManager.MinHeightFromBottom;

        // Check if the new ground height is less than or equal to the maximum ground height
        if (newGroundHeight <= minGroundHeight)
        {
            // Set the new ground height to the minimum ground height
            newGroundHeight = minGroundHeight;

            // Invoke an at min height method here
            ground.GetComponent<Ground>().SetToMinHeight();
        }

        // Set the position Y component based on the new ground height minus half height
        position.y = newGroundHeight - ground.GetComponent<Ground>().HalfHeight;

        // Determine the time to reach the maximum jump based on the time to reach the max jump height and the players hold jump time
        float timeToReachMaxJump = maxHeightJumpTime + _player.HoldJumpTime;

        // Determine the time it would take for the player to fall to the ground
        float timeToFall = Mathf.Sqrt(2f * (newHeightMax - (newGroundHeight - _player.HalfHeight)) / _player.Gravity);

        // Determine the time to travel the jump distance
        float totalDistanceTime = timeToReachMaxJump + timeToFall;

        // Determine the maximum gap distance
        float maxGap = totalDistanceTime * _player.HorizontalVelocity;

        // Obtain the minimum gap distance
        float minGap = _groundManager.MinGapDistance;

        // Determine a gap distance from the min and max gap range
        float actualGap = Random.Range(minGap, maxGap);

        // Alter the X position based on the gap distance, the right side of the current ground object and half the width of the new ground object
        position.x = (actualGap * _groundManager.JumpDistanceBuffer) + _rightSide + ground.GetComponent<Ground>().HalfWidth;

        // Apply the position to the new game object
        ground.transform.position = position;

        // Recalculate the ground height of the game object
        ground.GetComponent<Ground>().CalculateGroundHeight();

        // Set the parent of the new ground object
        ground.GetComponent<Ground>().SetParent(transform.parent);

        // Set the sprite of the new ground object
        ground.GetComponent<Ground>().SetRandomGroundSprite();

        // Check if will feature obstacles is true
        if (ground.GetComponent<Ground>().WillFeatureObstacles)
        {
            // Determine the number of obstacles to generate
            int numberOfObstacles = Random.Range(_minNumberOfObstacles, _maxNumberOfObstacles);

            // Generate the obstacles
            for (int i = 0; i < numberOfObstacles; i++)
            {
                // Invoke the add obstacle method
                AddObstacle(ground);
            }
        }

        // Check if the item will feature health items
        if(ground.GetComponent<Ground>().WillFeatureHealth)
        {
            // Add health item to the ground
            AddHealth(ground);
        }
    }

    /*
     * AT MAX HEIGHT METHOD
     * 
     * Method is used to set the "at
     * max height" boolean to true
     * when invoked.
     */
    public void SetToMaxHeight()
    {
        _atMaxHeight = true;
    }

    /*
     * AT MIN HEIGHT METHOD
     * 
     * Method is used to set the "at
     * max height" boolean to true
     * when invoked.
     */
    public void SetToMinHeight()
    {
        _atMinHeight = true;
    }

    /*
     * NEW GROUND OBJECT METHOD
     * 
     * When invoked, the method first determines
     * a random index number. The index number
     * is then used to obtain a ground object
     * from the ground manager and returns the 
     * found game object.
     */
    private GameObject NewGroundObject()
    {
        // Obtain a random index
        int index = Random.Range(0, (_groundManager.NumberOfObjects));

        // Obtain a ground game object from the ground manager
        GameObject ground = _groundManager.ReturnGround(index);

        // Return the ground object
        return ground;
    }

    /*
     * CALCULATE GROUND HEIGHT
     * 
     * Method is used to determine the ground
     * height in relation to the Y component
     * of the game object's transform position
     * and half the Y component of the object's
     * scale.
     */
    public void CalculateGroundHeight()
    {
        _groundHeight = transform.position.y + HalfHeight;
    }

    /*
     * SET PARENT METHOD
     * 
     * When invoked, the parent of the 
     * ground object is set to the 
     * parent in the argument.
     */
    public void SetParent(Transform parent)
    {
        transform.parent = parent;
    }

    /*
     * ADD OBSTACLE METHOD
     * 
     * Method is used to add an obstacle to
     * the ground object.
     */
    private void AddObstacle(GameObject newGround)
    {
        // Create an instance of the obstacle
        GameObject obstacle = Instantiate(_groundManager.ReturnObstacle());

        // New position vector
        Vector2 position;

        // Determine the Y position
        position.y = newGround.GetComponent<Ground>().GroundHeight + obstacle.GetComponent<Obstacle>().HalfHeight;

        // Obtain the left side of the new ground object
        float obstacleLeftSide = newGround.transform.position.x - (1f - newGround.GetComponent<Ground>().SafeAreaFromLeft) * newGround.GetComponent<Ground>().HalfWidth;

        // Obtain the right side of the new ground object
        float obstacleRightSide = newGround.transform.position.x + (1f - newGround.GetComponent<Ground>().SafeAreaFromRight) * newGround.GetComponent<Ground>().HalfWidth;

        // Determine the minimum X position
        float minXPosition = (obstacleLeftSide) + obstacle.GetComponent<Obstacle>().HalfWidth;

        // Determine the maximum X position
        float maxXPosition = (obstacleRightSide) - obstacle.GetComponent<Obstacle>().HalfWidth;

        // Randomly determine the X position from the min and max range
        float obstacleXPosition = Random.Range(minXPosition, maxXPosition);

        // Set the X component of the position vector
        position.x = obstacleXPosition;

        // Set the transform of the obtstacle
        obstacle.transform.position = position;
    }

    private void AddHealth(GameObject newGround)
    {
        // Create an instance of the obstacle
        GameObject healthItem = Instantiate(_groundManager.ReturnHealth());

        // New position vector
        Vector2 position;

        // Determine the Y position
        position.y = newGround.GetComponent<Ground>().GroundHeight + (healthItem.GetComponent<HealthItem>().HalfHeight);

        // Obtain the left side of the new ground object
        float healthLeftSide = newGround.transform.position.x - (1f - newGround.GetComponent<Ground>().SafeAreaFromLeft) * newGround.GetComponent<Ground>().HalfWidth;

        // Obtain the right side of the new ground object
        float healthRightSide = newGround.transform.position.x + (1f - newGround.GetComponent<Ground>().SafeAreaFromRight) * newGround.GetComponent<Ground>().HalfWidth;

        // Determine the minimum X position
        float minXPosition = (healthLeftSide) + healthItem.GetComponent<HealthItem>().HalfWidth;

        // Determine the maximum X position
        float maxXPosition = (healthRightSide) - healthItem.GetComponent<HealthItem>().HalfWidth;

        // Randomly determine the X position from the min and max range
        float healthItemXPosition = Random.Range(minXPosition, maxXPosition);

        // Set the X component of the position vector
        position.x = healthItemXPosition;

        // Set the transform of the obtstacle
        healthItem.transform.position = position;
    }
    /*
     * SET RANDOM SPRITE METHOD
     * 
     * Method obtains a random sprite from the 
     * ground manager, then applies the sprite
     * to the sprite renderer.
     */
    public void SetRandomGroundSprite()
    {
        // Determine a random index number
        int index = Random.Range(0, (_groundManager.NumberOfSprites));

        // Obtain random ground sprite from the ground manager and apply to the sprite renderer
        _spriteRenderer.sprite = Instantiate(_groundManager.ReturnGroundSprite(index));
    }
}