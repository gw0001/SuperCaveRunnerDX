/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 01/02/22                      */
/* LAST MODIFIED - 22/02/22                */
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
    [Header("Ground feature settings")]
    [SerializeField] private bool _canFeatureObstacles = false; // Can feature obstacles
    [SerializeField] private bool _canFeatureHealth = false; // Can feature health boolean
    [SerializeField] private bool _canFeatureLightGate = false; // Can feature light gate
    [SerializeField, Range(0, 3)] private int _minNumberOfObstacles; // Maximum number of obstacles
    [SerializeField, Range(0, 4)] private int _maxNumberOfObstacles; // Maximum number of obstacles
    [SerializeField, Range(0f, 1f)] private float _safeAreaFromLeft = 0.5f; // Safe area from the left side of the ground
    [SerializeField, Range(0f, 1f)] private float _safeAreaFromRight = 0.5f; // Safa area from the right side of the ground
    [SerializeField, Range(0f, 1f)] private float _obstacleChance = 0.5f; // The chance value for obstacles to appear on the ground
    [SerializeField, Range(0f, 1f)] private float _lightGateChance = 0.5f; // The chance value for obstacles to appear on the ground
    [SerializeField, Range(0f, 1f)] private float _healthChance = 0.5f; // The chance value for health to appear on the ground
    [SerializeField, Range(0f, 1f)] private float _platformChance = 0.5f; // The chance value for health to appear on the ground

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
    private bool _hasHealth = false; // Has generated health
    private bool _hasLightGate = false; // Has generated light gate
    [SerializeField] private bool _willFeatureObstacles = false; // Will feature obstacles
    [SerializeField] private bool _willFeatureHealth = false; // Will feature health boolean
    [SerializeField] private bool _willFeatureLightGate = false; // Will feature light gate boolean

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
     * WILL FEATURE LIGHT GATE METHOD
     * 
     * Method returns the boolean value
     * held by the will feature light
     * gate variable.
     */
    public bool WillFeatureLightGate
    {
        get
        {
            return _willFeatureLightGate;
        }
        set
        {
            _willFeatureLightGate = value;
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
     * HAS LIGHT GATE GET METHOD
     * 
     * Method is used to return the value
     * held by the has light gate boolean
     * variable, and to set the variable
     * to a value.
     */
    public bool HasLightGate
    {
        get
        {
            return _hasLightGate;
        }
        set
        {
            _hasLightGate = value;
        }
    }

    /*
     * HAS HEALTH GET METHOD
     * 
     * Method is used to return the value
     * held by the has health boolean
     * variable, and to set the variable
     * to a value.
     */
    public bool HasHealth
    {
        get
        {
            return _hasHealth;
        }
        set
        {
            _hasHealth = value;
        }
    }

    public bool CanFeatureLightGate
    {
        set
        {
            _canFeatureLightGate = value;
        }
    }

    public bool CanFeatureHealth
    {
        set
        {
            _canFeatureHealth = value;
        }
    }

    public int MinObstacles
    {
        get
        {
            return _minNumberOfObstacles;
        }
    }

    public int MaxObstacles
    {
        get
        {
            return _maxNumberOfObstacles;
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
     * Method determines if the next 
     * object is a ground object or a 
     * platform object and populates the 
     * object with obstacles, light gates,
     * or a health item.
     */
    public void GenerateGround()
    {
        // Is ground object boolean, initialised to true
        bool isGroundObject = true;

        // Determine the chance that the new ground object will be a platform
        float chance = Random.Range(0f, 1f);

        // Check if the chance value is less than the platform chance value
        if(chance < _platformChance)
        {
            // Set is ground object to false
            isGroundObject = false;
        }

        // Initialise a new game object as null
        GameObject newGameObject = null;

        // Check if the object will be a ground object
        if(isGroundObject == true)
        {
            // Instance a new ground object
            newGameObject = Instantiate(NewGroundObject());

            // Set the sprite of the new ground object
            newGameObject.GetComponent<Ground>().SetRandomGroundSprite();
        }
        else
        {
            // Instantiate a new platform object
            newGameObject = Instantiate(NewPlatformObject());

            // Set the sprite of the new platform object
            newGameObject.GetComponent<Ground>().SetRandomPlatformSprite();
        }

        // Empty position vector
        Vector2 position = Vector2.zero;

        // Determine the maximum jump the player can achieve if holding the jump button
        float maxHoldJumpHeight = _player.MaxJumpVelocity * _player.HoldJumpTime;

        // Determine the time it would take for the player to reach the max jump height
        float maxHeightJumpTime = _player.MaxJumpVelocity / _player.Gravity;

        // Determine the jump when gravity is taking effect
        float naturalJumpHeight = (_player.MaxJumpVelocity * maxHeightJumpTime + (0.5f * (_player.Gravity * (maxHeightJumpTime * maxHeightJumpTime))));

        // Determine the maximum jump height
        float maxJumpHeight = maxHoldJumpHeight + naturalJumpHeight;

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
            newGameObject.GetComponent<Ground>().SetToMaxHeight();
        }

        // Determine the minimum ground height
        float minGroundHeight = _screenInfo.BottomEdge + _groundManager.MinHeightFromBottom;

        // Check if the new ground height is less than or equal to the maximum ground height
        if (newGroundHeight <= minGroundHeight)
        {
            // Set the new ground height to the minimum ground height
            newGroundHeight = minGroundHeight;

            // Invoke an at min height method here
            newGameObject.GetComponent<Ground>().SetToMinHeight();
        }

        // Set the position Y component based on the new ground height minus half height
        position.y = newGroundHeight - newGameObject.GetComponent<Ground>().HalfHeight;

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
        position.x = (actualGap * _groundManager.JumpDistanceBuffer) + _rightSide + newGameObject.GetComponent<Ground>().HalfWidth;

        // Apply the position to the new game object
        newGameObject.transform.position = position;

        // Recalculate the ground height of the game object
        newGameObject.GetComponent<Ground>().CalculateGroundHeight();

        // Set the parent of the new ground object
        newGameObject.GetComponent<Ground>().SetParent(transform.parent);

        // Check if the current ground object has a light gate
        if (_hasLightGate)
        {
            // Prevent the new ground object from featuring a light gate
            newGameObject.GetComponent<Ground>().CanFeatureLightGate = false;
        }

        // Check if the current ground object has a health item
        if(_hasHealth)
        {
            // Prevent the new ground object from featuring health
            newGameObject.GetComponent<Ground>().CanFeatureHealth = false;
        }

        // Determine the possible features that the new ground object can have
        newGameObject.GetComponent<Ground>().DeterminePossibleFeatures();

        // Determine the object the objects will be 
        newGameObject.GetComponent<Ground>().DetermineObjectType();

        // Check if the item will feature health items
        if(newGameObject.GetComponent<Ground>().WillFeatureHealth)
        {
            // Check that the current platform doesn't feature a healing item
            //if(!HasHealth)
            //{
                // Add health item to the ground
                AddHealth(newGameObject);

                // Set the has health boolean of the new game ground object to true
                newGameObject.GetComponent<Ground>().HasHealth = true;
            //}
        }

        // Check if the new game object will feature light gates
        if(newGameObject.GetComponent<Ground>().WillFeatureLightGate)
        {
            //// Check that the current platform doesn't feature a light gate
            //if(!HasLightGate)
            //{
                // Add light gate to the new game object
                AddLightGate(newGameObject);

                // Set the has light gate boolean of the new ground object to true
                newGameObject.GetComponent<Ground>().HasLightGate = true;
            //}
        }

        // Check if will feature obstacles is true
        if (newGameObject.GetComponent<Ground>().WillFeatureObstacles)
        {
            // Determine the number of obstacles to generate
            int numberOfObstacles = Random.Range(newGameObject.GetComponent<Ground>().MinObstacles, newGameObject.GetComponent<Ground>().MaxObstacles);

            // Generate the obstacles
            for (int i = 0; i < numberOfObstacles; i++)
            {
                // Invoke the add obstacle method
                AddObstacle(newGameObject);
            }
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
     * When invoked, the method obtains
     * and returns a ground game object 
     * from the ground manager.
     */
    private GameObject NewGroundObject()
    {
        return _groundManager.ReturnGround();
    }

    /*
     * NEW PLATFORM OBJECT
     * 
     * When invoked, the method obtains
     * and returns a platform game object
     * from the ground manager.
     */
    private GameObject NewPlatformObject()
    {
        return _groundManager.ReturnPlatform();
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

    /*
     * ADD HEALTH METHOD
     * 
     * When invoked, the method will add 
     * one health item to the 
     * ground/platform.
     */
    private void AddHealth(GameObject newGround)
    {
        // Create an instance of the health item
        GameObject healthItem = Instantiate(_groundManager.ReturnHealthItem());

        // New position vector
        Vector2 position;

        // Determine the Y position of the health item, based on the ground height of the new ground and 1.5 times the half height
        position.y = newGround.GetComponent<Ground>().GroundHeight + 1.5f * healthItem.GetComponent<HealthItem>().HalfHeight;

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

        // Set the transform of the health item
        healthItem.transform.position = position;
    }

    /*
     * ADD LIGHT GATE METHOD
     * 
     * Method is used to add a light gate to
     * a new ground object. Light gate is
     * positioned at either the left edge
     * of the ground object, the right
     * edge if the ground object, or the 
     * middle of the ground object.
     */
    private void AddLightGate(GameObject newGround)
    {
        // Create an instance of the obstacle
        GameObject lightGate = Instantiate(_groundManager.ReturnLightGate());

        // Invoke the find components method for the light gate
        lightGate.GetComponent<LightGate>().FindComponents();

        // Invoke the initialise components method for the light gate
        lightGate.GetComponent<LightGate>().InitialiseComponents();

        // New position vector
        Vector2 position;

        // Determine the Y position of the 
        position.y = newGround.GetComponent<Ground>().GroundHeight;

        // Determine the left side of the ground object with half with width of the light gate
        float leftSide = newGround.transform.position.x - newGround.GetComponent<Ground>().HalfWidth + lightGate.GetComponent<LightGate>().HalfWidth;

        // Determine the right side of the ground object with half the width of the light gate
        float rightSide = newGround.transform.position.x + newGround.GetComponent<Ground>().HalfWidth - lightGate.GetComponent<LightGate>().HalfWidth;

        // Determine the middle of the new ground object
        float middle = newGround.transform.position.x;

        // Determine a random integer from 0 (inclusive) to 3 (exclusive)
        int randomInt = Random.Range(0, 3);

        // Check the value of the random integer
        if (randomInt == 0)
        {
            // Set the x position to the left side of the new ground object
            position.x = leftSide;
        }
        else if (randomInt == 1)
        {
            // Set the position to the middle of the new ground object
            position.x = middle;
        }
        else
        {
            // Set the position to the right side of the game object
            position.x = rightSide;
        }

        // Set the transform of the light gate
        lightGate.transform.position = position;
    }

    /*
     * SET RANDOM GROUND SPRITE METHOD
     * 
     * Method obtains a random ground sprite from 
     * the ground manager, then applies the sprite
     * to the sprite renderer.
     */
    public void SetRandomGroundSprite()
    {
        // Determine a random index number based on the number of ground sprites
        int index = Random.Range(0, (_groundManager.NumberOfGroundSprites));

        // Obtain random ground sprite from the ground manager and apply to the sprite renderer
        _spriteRenderer.sprite = Instantiate(_groundManager.ReturnGroundSprite(index));
    }

    /*
     * SET RANDOM PLATFORM SPRITE METHOD
     * 
     * Method obtains a random ground sprite from 
     * the ground manager, then applies the sprite
     * to the sprite renderer.
     */
    public void SetRandomPlatformSprite()
    {
        // Determine a random index number based on the number of platform sprites
        int index = Random.Range(0, (_groundManager.NumberOfPlatformSprites));

        // Obtain random ground sprite from the ground manager and apply to the sprite renderer
        _spriteRenderer.sprite = Instantiate(_groundManager.ReturnPlatformSprite(index));
    }

    public void DeterminePossibleFeatures()
    {
        // Check the health of the player
        if (_player.Health < _player.MaxHealth)
        {
            // Set can feature health to true
            _canFeatureHealth = true;
        }
        else
        {
            // Set can feature health to false
            _canFeatureHealth = false;
        }

        // Check if can feature health is true
        if (_canFeatureHealth)
        {
            // Comparing to the health chance and determine if the ground object will feature a healing item
            _willFeatureHealth = true;
        }

        // Check if the ground object can feature obstacles
        if (_canFeatureObstacles)
        {
            // Determine a random chance value
            float chance = Random.Range(0f, 1f);

            // Check if the chance is less than, or equal to, the obstacle chance
            if (chance <= _obstacleChance)
            {
                // Set the will feature obstacles boolean to true
                _willFeatureObstacles = true;
            }
            else
            {
                // Set will feature obstacles to false
                _willFeatureObstacles = false;
            }
        }

        // Check if the ground object can feature light gates
        if (_canFeatureLightGate)
        {
            // Determine a random chance value
            float chance = Random.Range(0f, 1f);

            // Check if the chance is less than, or equal to, the light gate chance
            if (chance <= _lightGateChance)
            {
                // Set the will feature light gate boolean to true
                _willFeatureLightGate = true;
            }
            else
            {
                // Set will feature light gate to false
                _willFeatureLightGate = false;
            }
        }
    }

    /*
     * DETERMINE GROUND OBJECTS METHOD
     * 
     * Method determines if the ground object will
     * feature obstacles, light gates, healing item,
     * or nothing at all.
     */
    public void DetermineObjectType()
    {
        // Check for condition where will feature obstacles and will feature light gate are both true
        if (_willFeatureObstacles && _willFeatureLightGate)
        {
            // Check if the obstacle chance is greater than the light gate chance
            if(_obstacleChance > _lightGateChance)
            {
                // Set will feature obstacles to true
                _willFeatureObstacles = true;

                // Set will feature lightgates to false
                _willFeatureLightGate = false;
            }
            // Else, check if the obstacle chance is lower than the light gate chance
            else if(_obstacleChance < _lightGateChance)
            {
                // Set will feature obstacles to false
                _willFeatureObstacles = false;

                // Set will feature light gate to true
                _willFeatureLightGate = true;
            }
            // Else, both have same probability
            else
            {
                // Determine a random chance value
                float chance = Random.Range(0f, 1f);

                // Check if the value is less than 50%
                if (chance <= 0.5f)
                {
                    // Set will feature obstacles to true
                    _willFeatureObstacles = true;

                    // Set will feature lightgates to false
                    _willFeatureLightGate = false;
                }
                else
                {
                    // Set will feature obstacles to false
                    _willFeatureObstacles = false;

                    // Set will feature light gate to true
                    _willFeatureLightGate = true;
                }
            }
        }

        // Check if will feature health and will feature obstacles are both true
        if (_willFeatureHealth && _willFeatureObstacles)
        {
            // Randomly determine if the ground object will feature obstacles
            float chance = Random.Range(0f, 1f);

            // Check if chance is equal to 0
            if (chance <= _healthChance)
            {
                // Set will feature health to true
                _willFeatureHealth = true;

                // Set will feature obstacles to false
                _willFeatureObstacles = false;
            }
            else
            {
                // Set will feature health to false
                _willFeatureHealth = false;

                // Set will feature obstacles to true
                _willFeatureObstacles = true;
            }
        }
        // Check if will feature health and will feature light gates are both true
        else if (_willFeatureHealth && _willFeatureLightGate)
        {
            // Randomly determine if the ground object will feature obstacles
            float chance = Random.Range(0f, 1f);

            // Check if chance is equal to 0
            if (chance <= _healthChance)
            {
                // Set will feature health to true
                _willFeatureHealth = true;

                // Set will feature obstacles to false
                _willFeatureLightGate = false;
            }
            else
            {
                // Set will feature health to false
                _willFeatureHealth = false;

                // Set will feature obstacles to true
                _willFeatureLightGate = true;
            }
        }
    }















    /*
     * ADD OBSTACLES METHOD
     * 
     * Method under development...
     */
    private void AddObstacles(GameObject newGround)
    {
        // Determine the number of obstacles to generate
        int numberOfObstacles = Random.Range(_minNumberOfObstacles, _maxNumberOfObstacles + 1);

        // Generate the obstacles
        for (int i = 0; i < numberOfObstacles; i++)
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
    }
}