/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 01/02/22                      */
/* LAST MODIFIED - 25/02/22                */
/* ======================================= */
/* PLAYER CONTROLLER                       */
/* PlayerController.cs                     */
/* ======================================= */
/* Script controls the behaviour of the    */
/* player object and allows for player     */
/* input to alter the behaviour.           */
/* ======================================= */

// Directives
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // *** SERIALIZED PLAYER SETTINGS *** //
    [Header("Player Settings")]
    [SerializeField] private int _maxHealth = 3; // Maximum health
    [SerializeField] private float _gravity = 100f; // Gravity - Set to 100 by default
    [SerializeField] private float _maxJumpVelocity = 20f; // Maximum jump velocity
    [SerializeField] private float _maxRunVelocity = 50f; // Maximum run velocity
    [SerializeField] private float _maxAcceleration = 10f; // Maximum acceleration
    [SerializeField] private float _maxHoldJumpTime = 0.25f; // Maximum hold time
    [SerializeField] private float _jumpGroundThreshold = 2f; // Jump to ground threshold - allows the player a small chance to jump when they are slightly above the ground
    [SerializeField] private float _checkRayOffset = 0.7f; // Offset of the rays that check if the player is grounded and can jump
    [SerializeField] private float _obstacleHitSpeedLoss = 0.3f; // Obstacle speed loss;
    [SerializeField] private float _invicibilityTime = 0.5f; // Invincibility time
    [SerializeField] private float _invinFlashTime = 0.1f; // Invincibility flash time
    [SerializeField] private float _colourCooldownTime = 0.5f; // Colour cool down timer
    [SerializeField] private float _playerHeadSpace = 3f; // Space above the players head
    [SerializeField] private float _obstacleDeathTime = 2f; // Death time
    [SerializeField] private float _lightGateDeathTime = 2f; // Beam death time
    [SerializeField] private float _fallDeathTime = 2f; // Fall death time
    [SerializeField] private float _spriteFadeTime = 1f; // Sprite fade time
    [SerializeField] private float _spriteFlashTime = 0.025f; // Sprite flash time
    
    // *** SERIALIZED MENU OPTIONS *** //
    [Header ("Menu object settings")]
    [SerializeField] private bool _isMenuObject; // Working on scrolling background for menu, will override player and ground objects
    [SerializeField] private float _menuMoveSpeed = 50f; // Menu movement speed - used to set the horizontal velocity

    // *** VARIABLES *** //
    private PlayerState _playerState; // Player state
    private LastAction _lastAction; // Last action
    private LastCollision _lastCollision; // Last collision
    private SpriteRenderer _playerSprite; // player sprite
    private ScreenInfo _screenInfo; // Screen information
    private Vector2 _velocity; // Velocity vector
    private float _aboveHead; // Y coordinate above the player sprite
    private float _distance; // Distance
    private float _groundHeight; // Ground height
    private float _holdJumpTime; // Hold jump time
    private float _halfHeight; // Half height of player
    private float _halfWidth; // Half width of player
    private float _holdJumpTimer; // Hold jump timer
    private float _invincibilityTimer; // Invincibility timer
    private float _invinFlashTimer; // Invincibility flash timer
    private float _colourCooldownTimer; // Colour cool down timer
    private float _deathTime; // Death time
    private float _deathTimer; // Death timer
    private float _spriteFlashTimer; // Sprite flash timer
    private bool _isGrounded = false; // Is Grounded boolean
    private bool _isHoldingJump = false; // Is Holding Jump boolean
    private bool _isInvincible = false; // Is invincible boolean
    private bool _gameStarted; // Game started boolean
    private bool _hasHitGround; // Has hit ground boolean
    private bool _isColourOne = true; // Is colour one initialised to true
    private bool _canChangeColour = true; // Can change colour
    private bool _hasChangedColour = false; // Has changed colour
    private int _health; // Player health

    // Player State Enumerator
    public enum PlayerState
    {
        idle,
        running,
        inAir,
        dying,
        dead
    }

    // Last action enumerator
    public enum LastAction
    {
        run,
        jump
    }

    // Last collision enumerator
    public enum LastCollision
    {
        pit,
        ground,
        obstacle,
        lightgate
    }

    /*
     * PLAYER STATE GET METHOD
     * 
     * Method returns the state held by the
     * player state variable.
     */
    public PlayerState PlayerStatus
    {
        get
        {
            return _playerState;
        }
    }

    /*
     * PLAYER ACTION GET METHOD
     * 
     * Method returns the last action the
     * player performed, held by the 
     * last action variable.
     */
    public LastAction PlayerAction
    {
        get
        {
            return _lastAction;
        }
    }

    /*
     * PLAYER COLLISION GET METHOD
     * 
     * Method returns the last object the
     * player collided with, held by the
     * last collision variable.
     */
    public LastCollision PlayerCollision
    {
        get
        {
            return _lastCollision;
        }
    }

    /*
     * HALF HEIGHT GET METHOD
     * 
     * Method returns the value held
     * by the half height variable.
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
     * Method returns the value held
     * by the half width method.
     */
    public float HalfWidth
    {
        get
        {
            return _halfWidth;
        }
    }

    /*
     * DISTANCE GET METHOD
     * 
     * Method returns the value held
     * by the distance variable.
     */
    public float Distance
    {
        get
        {
            return _distance;
        }
    }

    /*
     * RUN VELOCITY GET METHOD
     * 
     * Method returns the X value held
     * by the velocity vector.
     */
    public float HorizontalVelocity
    {
        get
        {
            return _velocity.x;
        }
    }

    /*
     * JUMP VELOCITY GET METHOD
     * 
     * Method returns the Y value held
     * by the velocity vector.
     */
    public float VerticalVelocity
    {
        get
        {
            return _velocity.y;
        }
    }

    /*
     * MAX JUMP VELOCITY GET METHOD
     * 
     * Method returns the value held by
     * the maximum jump velocity
     * variable.
     */
    public float MaxJumpVelocity
    {
        get
        {
            return _maxJumpVelocity;
        }
    }

    /*
     * HOLD JUMP TIME GET METHOD
     * 
     * Method returns the value held
     * by the hold jump time variable.
     */
    public float HoldJumpTime
    {
        get
        {
            return _holdJumpTime;
        }
    }

    /*
     * MAX HOLD JUMP TIME GET METHOD
     * 
     * Method returns the value held
     * by the max hold jump time
     * variable.
     */
    public float MaxHoldJumpTime
    {
        get
        {
            return _maxHoldJumpTime;
        }
    }

    /*
     * GRAVITY GET METHOD
     * 
     * Method returns the value held
     * by the gravity variable.
     */
    public float Gravity
    {
        get
        {
            return _gravity;
        }
    }

    /*
     * MAX RUN VELOCITY GET METHOD
     * 
     * Method returns the value held by
     * the max run velocity variable.
     */
    public float MaxRunVelocity
    {
        get
        {
            return _maxRunVelocity;
        }
    }

    /*
     * HEALTH GET METHOD
     * 
     * Method returns the value held by
     * the health variable.
     */
    public int Health
    {
        get
        {
            return _health;
        }
    }

    /*
     * MAX HEALTH GET METHOD
     * 
     * Method returns the value held by
     * the max health variable.
     */
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
    }

    /*
     * IS COLOUR ONE GET METHOD
     * 
     * Method returns the boolean value held
     * by the is colour one variable.
     */
    public bool IsColourOne
    {
        get
        {
            return _isColourOne;
        }
    }

    /*
     * ABOVE HEAD GET METHOD
     * 
     * Method returns the value held by the 
     * above head variable.
     */
    public float AboveHead
    {
        get
        {
            return _aboveHead;
        }
    }

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is 
     * awoken.
     */
    private void Awake()
    {
        // Obtain the screen info
        _screenInfo = GameObject.FindObjectOfType<ScreenInfo>();

        // Determine half of the player height
        _halfHeight = transform.localScale.y / 2f;

        // Determine the hald width of the player
        _halfWidth = transform.localScale.x / 2f;

        // Set the distance to 0
        _distance = 0f;

        // Initialise the ground height to 0 seconds
        _groundHeight = 0f;

        // Set the hold jump timer to 0 seconds
        _holdJumpTimer = 0f;

        // Set the invincibility timer to 0 seconds
        _invincibilityTimer = 0f;

        // Set the invinvibility flash timer to 0 seconds
        _invinFlashTimer = 0f;

        // Set the death timer to 0 seconds
        _deathTimer = 0f;

        // Set the sprite flash timer to 0 seconds
        _spriteFlashTimer = 0f;

        // Set the player state to idle
        _playerState = PlayerState.idle;

        // Set game started to false
        _gameStarted = false;

        // Obtain the sprite renderer
        _playerSprite = GetComponent<SpriteRenderer>();

        // Set the last collision to "pit"
        _lastCollision = LastCollision.pit;

        // Initialise has hit ground to false
        _hasHitGround = false;

        // Initialise the player with maximum health
        _health = _maxHealth;
        
        // Check if the object is a menu object
        if(_isMenuObject)
        {
            // Set the x velocity of the player to the menu speed
            _velocity.x = _menuMoveSpeed;
        }
        else
        {
            // Set the player position
            transform.position = new Vector2((_screenInfo.LeftEdge + _screenInfo.SegmentWidth()), HalfHeight);
        }

        // Determine a random colour number
        float randomColour = Random.Range(0f, 1f);

        // Determine the starting colour based on the random colour number
        _isColourOne = randomColour <= 0.5f ? true : false;

        //_playerSprite.color = _colourOne;

        // Set the can change colour to true
        _canChangeColour = true;

        // Set the has changed colour to false
        _hasChangedColour = false;

        // Determine the Y value above the players head
        _aboveHead = transform.position.y + HalfHeight + _playerHeadSpace;
    }

    /*
     * FIXED UPDATE METHOD
     * 
     * Method is invoked at regular fixed time intervals.
     * 
     * Method is used to update the player position, based
     * on if they are jumping or falling.
     * 
     * Checks are carried out to determine if the player
     * is grounded and if they can jump when they are grounded.
     */
    private void FixedUpdate()
    {
        // Check if the player object is not a menu object
        if(!_isMenuObject)
        {
            // Check if the game has started
            if(_gameStarted)
            {
                // Check that the player is not dying
                if(_playerState != PlayerState.dying)
                {
                    // Obtain the position of the player
                    Vector2 position = transform.position;

                    // Check if the player has fallen off the screen
                    if (position.y <= _screenInfo.BottomEdge - HalfHeight)
                    {
                        // Check if the last collision is not with the ground
                        if (_lastCollision != LastCollision.ground)
                        {
                            // Set last collision to pit
                            _lastCollision = LastCollision.pit;
                        }

                        // Set the player state to dying
                        _playerState = PlayerState.dying;

                        // Set the velocity of the player to a zero vector
                        _velocity = Vector2.zero;

                        // Return to avoid unecessary calculation
                        return;
                    }

                    // Check if the player has life left
                    if (Health < 1)
                    {
                        // Set the player state to dying
                        _playerState = PlayerState.dying;

                        // Check the last thing the player collided with
                        if (_lastCollision == LastCollision.obstacle)
                        {
                            // Player hit obstacle, set the death time for an obstacle death
                            _deathTime = _obstacleDeathTime;
                        }
                        else if (_lastCollision == LastCollision.lightgate)
                        {
                            // Player last hit a light gate, set the death time for a light gate death
                            _deathTime = _lightGateDeathTime;
                        }
                        else
                        {
                            // Player has fallen into a pit, set the death time for a fall death
                            _deathTime = _fallDeathTime;
                        }

                        // Set the velocity to a zero vector
                        _velocity = Vector2.zero;

                        // Return to avoid unecessary calculation
                        return;
                    }

                    // Check if colour has changed
                    if (_hasChangedColour)
                    {
                        // Increment the cool down timer by the fixed time increment
                        _colourCooldownTimer += Time.fixedDeltaTime;

                        // Check if the colour cool down timer is greater than, or equal to, the colour cool down time
                        if (_colourCooldownTimer >= _colourCooldownTime)
                        {
                            // Change the has changed boolean to false
                            _hasChangedColour = false;

                            // Set the can change colour boolean to true
                            _canChangeColour = true;
                        }
                    }

                    // Check if the player is invinvicle
                    if (_isInvincible)
                    {
                        // Flash the sprite
                        InvincibilityEffect();

                        // Invoke the invincibility method
                        Invincibilty();
                    }

                    // Determine the crash ray origin
                    Vector2 crashRayOrigin = new Vector2(position.x, position.y);

                    // Cast crash ray forward 
                    RaycastHit2D crashForward = Physics2D.Raycast(crashRayOrigin, Vector2.right, (0.5f * HalfWidth) + 0.1f);

                    // Check that if the crash forward ray has hit a collider
                    if (crashForward.collider != null)
                    {
                        // Obtain an obstacle from the collider
                        Obstacle obstacle = crashForward.collider.GetComponent<Obstacle>();

                        // Check obstacle exists
                        if (obstacle != null)
                        {
                            // Invoke the Hit Obstacle method
                            HitObstacle(obstacle);
                        }

                        // Obtain a light beam from the collider
                        LightBeam lightBeam = crashForward.collider.GetComponent<LightBeam>();

                        // Check if the light beam exists
                        if (lightBeam != null)
                        {
                            // Check if the player is not the same colour as the light beam
                            if (_isColourOne != lightBeam.IsColourOne)
                            {
                                // Invoke the hit light gate method
                                HitLightGate();
                            }
                        }

                        // Obtain the ground from the collider
                        Ground ground = crashForward.collider.GetComponent<Ground>();

                        // Check if the ground exists
                        if (ground != null)
                        {
                            // Invoke the hit ground method
                            HitGround();
                        }

                        // Obtain the item collider
                        HealthItem healthItem = crashForward.collider.GetComponent<HealthItem>();

                        // Check that a health item exists
                        if (healthItem != null)
                        {
                            // Invoke the picked up method from the health item
                            healthItem.PickedUp();
                        }
                    }

                    // Draw the ray in the scene view
                    Debug.DrawRay(crashRayOrigin, Vector2.right * (HalfWidth + 0.1f), Color.cyan);

                    // Cast crash ray downwards
                    RaycastHit2D crashDown = Physics2D.Raycast(crashRayOrigin, Vector2.up, VerticalVelocity * Time.fixedDeltaTime);

                    // Check if the crash down collider exists
                    if (crashDown.collider != null)
                    {
                        // Obtain an obstacle from the crash down collider
                        Obstacle obstacle = crashDown.collider.GetComponent<Obstacle>();

                        // Check obstacle exists
                        if (obstacle != null)
                        {
                            // Invoke the Hit Obstacle method
                            HitObstacle(obstacle);
                        }

                        // Obtain a health item from the crash down collider
                        HealthItem healthItem = crashDown.collider.GetComponent<HealthItem>();

                        // Check health item exists
                        if (healthItem != null)
                        {
                            // Invoke the picked up method from the health item
                            healthItem.PickedUp();
                        }
                    }

                    // Draw the ray in the scene view
                    Debug.DrawRay(crashRayOrigin, Vector2.up * VerticalVelocity * Time.fixedDeltaTime, Color.yellow);

                    // Check if the player is not grounded
                    if (!_isGrounded)
                    {
                        // Check if the player is holding the jump button
                        if (_isHoldingJump)
                        {
                            // Increment the hold jump timer by fixed delta time
                            _holdJumpTimer += Time.fixedDeltaTime;

                            // Check if the hold jump timer is greater than, or equal to, the max hold jump tim
                            if (_holdJumpTimer >= _holdJumpTime)
                            {
                                // Set "is holding jump" to false
                                _isHoldingJump = false;
                            }
                        }

                        // Determine the Y component of the position according to the Y component of velocity multiplied by the fixed delta time
                        position.y += _velocity.y * Time.fixedDeltaTime;

                        // Check if the player isn't holding the jump button
                        if (!_isHoldingJump)
                        {
                            // Adjust the Y component of velocity 
                            _velocity.y -= _gravity * Time.fixedDeltaTime;
                        }

                        // Obtain the ground check ray origin
                        Vector2 groundCheckRayOrigin = position;

                        // Alter the X component of the ray origin by the ground check ray offset
                        groundCheckRayOrigin.x = groundCheckRayOrigin.x + _checkRayOffset;

                        // Set ray direction to up (will become down when the velocity in Y is negative)
                        Vector2 groundCheckRayDirection = Vector2.up;

                        // determine the ground check ray distance based on the velocity multiplied by the fixed delta time
                        float groundCheckRayDistance = (_velocity.y * Time.fixedDeltaTime) - HalfHeight;

                        // Cast ray and store as a 2D raycast hit
                        RaycastHit2D hit2D = Physics2D.Raycast(groundCheckRayOrigin, groundCheckRayDirection, groundCheckRayDistance);

                        // Check if the collider exists and the X value of the collision point is the same as the X value of the ray origin
                        if (hit2D.collider != null && !_hasHitGround)
                        {
                            // Obtain the ground object from the collision
                            Ground ground = hit2D.collider.GetComponent<Ground>();

                            // Check that the ground object exists
                            if (ground != null)
                            {
                                // Set the ground height
                                _groundHeight = ground.GroundHeight + HalfHeight;

                                // Set the Y component to the ground height
                                position.y = _groundHeight;

                                // Set the y velocity to 0
                                _velocity.y = 0f;

                                // Set the player state to running
                                _playerState = PlayerState.running;

                                // Set the player last action to run
                                _lastAction = LastAction.run;

                                // Set "is grounded" to true
                                _isGrounded = true;
                            }
                        }

                        // Draw the ray in the scene view
                        Debug.DrawRay(groundCheckRayOrigin, groundCheckRayDirection * groundCheckRayDistance, Color.red);
                    }

                    // Add the distance travelled at the current fixed time
                    _distance += (_velocity.x * Time.fixedDeltaTime) / transform.localScale.x;

                    // Check if player is grounded
                    if (_isGrounded)
                    {
                        // Determine the velocity ratio of the horizontal velocity
                        float velocityRatio = _velocity.x / _maxRunVelocity;

                        // Determine the the acceleration
                        float acceleration = _maxAcceleration * (1 - velocityRatio);

                        // Determine the hold time for the jump based on the max hold jump time and the velocity ratio
                        _holdJumpTime = _maxHoldJumpTime * velocityRatio;

                        // Increment the X component of velocity
                        _velocity.x += acceleration * Time.fixedDeltaTime;

                        // Check if the X component of velocity is greater than the max run velocity
                        if (_velocity.x >= _maxRunVelocity)
                        {
                            // Set the X component to the max run velocity
                            _velocity.x = _maxRunVelocity;
                        }

                        // Obtain the jump check ray origin
                        Vector2 jumpCheckRayOrigin = position;

                        // Alter the X component of the ray origin by the ground check ray offset
                        jumpCheckRayOrigin.x = jumpCheckRayOrigin.x - _checkRayOffset;

                        // Set ray direction to up (will become down when the velocity in Y is negative)
                        Vector2 jumpCheckRayDirection = Vector2.up;

                        // determine the ground check ray distance based on the velocity multiplied by the fixed delta time
                        float jumpCheckRayDistance = (_velocity.y * Time.fixedDeltaTime) - HalfHeight;

                        // Cast ray and store as a 2D raycast hit
                        RaycastHit2D hit2D = Physics2D.Raycast(jumpCheckRayOrigin, jumpCheckRayDirection, jumpCheckRayDistance);

                        // Check if the collider exists
                        if (hit2D.collider == null)
                        {
                            // Set "is grounded" to true
                            _isGrounded = false;
                        }

                        // Draw the ray in the scene view
                        Debug.DrawRay(jumpCheckRayOrigin, jumpCheckRayDirection * jumpCheckRayDistance, Color.green);
                    }

                    // Set the transform position to the position vector
                    transform.position = position;

                    // Update the space above the players head
                    _aboveHead = transform.position.y + HalfHeight + _playerHeadSpace;
                }
                else
                {
                    // Invoke the player dying method
                    PlayerDying();
                }
            }
        }
    }

    /*
     * HIT OBSTACLE METHOD
     * 
     * Method is invoked when the player 
     * collides with an obstacle. 
     * 
     * Upon collision, the method to destroy
     * the obstacle is invoked
     */
    private void HitObstacle(Obstacle obstacle)
    {
        // Destroy the obstacle - will need to remove this and control this from the obstacle object
        obstacle.DestroyObstacle();

        // Check if the player is not invincible
        if (!_isInvincible)
        {
            // Lose one hit point
            _health--;

            // Alter the X velocity value by multiplying by the remaining speed
            _velocity.x *= (1 - _obstacleHitSpeedLoss);

            // Set the last collision made by the player to the obstacle
            _lastCollision = LastCollision.obstacle;

            // Check if the players health is greater than 0
            if (Health > 0)
            {
                // Set the is invincible boolean to true
                _isInvincible = true;
            }
        }
    }

    /*
     * HIT LIGHT GATE METHOD
     * 
     * When invoked, the method alters the
     * player health
     */
    private void HitLightGate()
    {
        // Check if the player is not invincible
        if (!_isInvincible)
        {
            // Lose one hit point
            //_health--;

            _health = 0;

            // Set the last collision made by the player to the obstacle
            _lastCollision = LastCollision.lightgate;

            //// Check if the players health is greater than 0
            //if (Health > 0)
            //{
            //    // Set the is invincible boolean to true
            //    _isInvincible = true;
            //}
        }
    }

    /*
     * INVINCIBILITY METHOD
     * 
     * Method handles the ability for the player
     * to become invincible for a short period
     * of time.
     */
    private void Invincibilty()
    {
        // Increment the invincibility timer by the fixed delta time
        _invincibilityTimer += Time.fixedDeltaTime;

        // Check if the invincibility timer is greater than, or equal to, the invincibility time
        if (_invincibilityTimer >= _invicibilityTime)
        {
            // Set invincibility to false
            _isInvincible = false;

            // Reset the invincibility timer
            _invincibilityTimer = 0f;

            // Reset the invincibility flash timer
            _invinFlashTimer = 0f;

            // Set the colour of the sprite to white with 1f alpha value
            _playerSprite.color = new Vector4(_playerSprite.color.r, _playerSprite.color.g, _playerSprite.color.b, 1f);
        }
    }

    /*
     * INVINCIBILITY EFFECT METHOD
     * 
     * Method handles the flashing of the
     * player sprite when player is 
     * invincible.
     */
    private void InvincibilityEffect()
    {
        // Increment the invincibility flash timer by delta time
        _invinFlashTimer += Time.fixedDeltaTime;

        // Check if the invincibility timer is less than the flash time
        if (_invinFlashTimer < _invinFlashTime)
        {
            // Set the sprite colour to white with 0 alpha value
            _playerSprite.color = new Vector4(_playerSprite.color.r, _playerSprite.color.g, _playerSprite.color.b, 0f);
        }
        // Check if the invincibility flash timer is greater than, or equal to, twice the invincibility flash time
        else if (_invinFlashTimer >= 2 * _invinFlashTime)
        {
            // Reset the invincibility flash timer
            _invinFlashTimer = 0f;
        }
        else
        { 
            // Set the colour of the sprite to white with 1f alpha value
            _playerSprite.color = new Vector4(_playerSprite.color.r, _playerSprite.color.g, _playerSprite.color.b, 1f);
        }
    }

    /*
     * HIT GROUND METHOD
     * 
     * Method is invoked when the player
     * has face planted into a ground
     * object. Simply sets the horizontal
     * velocity to 0
     */
    private void HitGround()
    {
        if(!_hasHitGround)
        {
            // Set velocity to 0 for both X and Y components
            _velocity = new Vector2(0f, 0f);
        }

        // Set the has hit ground boolean to true
        _hasHitGround = true;

        // Set the last collision to the ground
        _lastCollision = LastCollision.ground;
    }

    /*
     * JUMP METHOD
     * 
     * Method takes in an callback context action.
     * 
     * When invoked, the method checks if the context
     * has been performed, the method then determines 
     * position of the player and the ground distance.
     * A check is then performed to check if the player
     * is grounded or just slightly above the jump ground
     * threshold. If so, the Y component of velocity is
     * adjusted, the "is grounded" boolean is set to
     * false, the "is holding jump" boolean is set to
     * true and the hold jump timer is set to 0 
     * seconds.
     * 
     * If the context has been canceled, the "is holding
     * jump" boolean is set to false.
     * 
     * This allows the player to hold the jump button
     * to achieve a big jump, or tap the jump button
     * to perform a small jump.
     */
    public void Jump(InputAction.CallbackContext context)
    {
        // Check if the context is performed
        if(context.performed && _gameStarted)
        {
            // Obtain the position of the player
            Vector2 position = transform.position;

            // Determine the ground distance based on the absolute value between the player position and the ground height
            float groundDistance = Mathf.Abs(position.y - _groundHeight);

            // Check if the player is grounded or if the player is slightly above the ground
            if (_isGrounded || groundDistance <= _jumpGroundThreshold)
            {
                // Set "is grounded" to false
                _isGrounded = false;

                // Set the Y component of velocity to the jump velocity
                _velocity.y = _maxJumpVelocity;

                // Assume player is holding the jump button
                _isHoldingJump = true;

                // Set the player state to In Air
                _playerState = PlayerState.inAir;

                // Set the last action by the player to jump
                _lastAction = LastAction.jump;

                // Reset the hold jump timer
                _holdJumpTimer = 0.0f;
            }
        }

        // Check if the context has been canceled
        if(context.canceled)
        {
            // Set "is holding jump" to false
            _isHoldingJump = false;
        }
    }

    /*
     * CHANGE COLOUR METHOD
     * 
     * Method allows for the user to press a
     * button and change the colour of the
     * player.
     */
    public void ChangeColour(InputAction.CallbackContext context)
    {
        // Check if the context has been performed and the game has started
        if(context.performed && _gameStarted)
        {
            // Check if the player can change colour
            if (_canChangeColour )
            {
                // Invert the colour
                _isColourOne = !_isColourOne;

                // Set the has changed colour boolean to true
                _hasChangedColour = true;

                // Reset the cooldown timer
                _colourCooldownTimer = 0f;

                // Set the can change colour boolean to false
                _canChangeColour = false;
            }
        }
    }

    /*
     * GAME START METHOD
     * 
     * Invoked to start the game. Sets
     * the game started boolean to 
     * true and the player status
     * to running.
     */
    public void GameStart()
    {
        // Set game started to true
        _gameStarted = true;

        //_gameStarted = true;
        _playerState = PlayerState.running;
    }

    /*
     * GAME OVER METHOD
     * 
     * When invoked, the game is considered 
     * over. Sets the players velocity to 0,
     * health to 0, and the player state to
     * dead.
     */
    private void GameOver()
    {
        // Set the x velocity to 0
        _velocity.x = 0;

        // Set the health to 0
        _health = 0;

        // Set the player status to dead
        _playerState = PlayerState.dead;
    }


    /*
     * ADD HEALTH
     * 
     * When invoked, the health variable
     * is incremented by one. A check is
     * carried out to ensure that the 
     * player's health does not exceed 
     * the maximum health.
     */
    public void AddHealth()
    {
        // Check if the health is greater than the max health
        if (_health >= _maxHealth)
        {
            // Return as player is at full health
            return;
        }

        // Increment the health variable
        _health++;
    }

    /*
     * DYING EFFECT METHOD
     * 
     * Method gives the sprite a flashing
     * effect when the player is dying
     */
    private void DyingEffect()
    { 
        // Increment the sprite flash timer
        _spriteFlashTimer += Time.fixedDeltaTime;

        if (_spriteFlashTimer < _spriteFlashTime)
        {
            // Set the sprite colour to white with 0 alpha value
            _playerSprite.color = new Vector4(_playerSprite.color.r, _playerSprite.color.g, _playerSprite.color.b, 0f);
        }
        else if(_spriteFlashTimer >= 2 * _spriteFlashTime)
        {
            // Reset the sprite flash timer to 0 seconds
            _spriteFlashTimer = 0f;
        }
        else
        {
            // Set the colour of the sprite to white with 1f alpha value
            _playerSprite.color = new Vector4(_playerSprite.color.r, _playerSprite.color.g, _playerSprite.color.b, 1f);
        }
    }

    /*
     * PLAYER DYING METHOD
     * 
     * When invoked, the method handles the
     * time it takes to show the player die
     * on screen, then consider the game
     * over.
     */
    private void PlayerDying()
    {
        // Prevent the player from changing colour
        _canChangeColour = false;

        // Increment the death timer
        _deathTimer += Time.fixedDeltaTime;

        // Check that the last collision made by the player was with an obstacle or a light gate
        if(_lastCollision == LastCollision.lightgate || _lastCollision == LastCollision.obstacle)
        {
            // Check if the death timer is greater than, or equal to, the death time minus the sprite fade time
            if (_deathTimer >= _deathTime - _spriteFadeTime)
            {
                // Invoke the dying effect
                DyingEffect();
            }
        }

        // Check if the death timer is greater than, or equal to the death time
        if(_deathTimer >= _deathTime)
        {

            // Check that the last collision made by the player was with an obstacle or a light gate
            if (_lastCollision == LastCollision.lightgate || _lastCollision == LastCollision.obstacle)
            {

                // Set the sprite colour to white with 0 alpha value, to make the sprite disappear from the screen
                _playerSprite.color = new Vector4(_playerSprite.color.r, _playerSprite.color.g, _playerSprite.color.b, 0f);
            }


            // Set the sprite colour to white with 0 alpha value, to make the sprite disappear from the screen
            //_playerSprite.color = new Vector4(_playerSprite.color.r, _playerSprite.color.g, _playerSprite.color.b, 0f);

            // Invoke the game over method
            GameOver();
        }
    }
}