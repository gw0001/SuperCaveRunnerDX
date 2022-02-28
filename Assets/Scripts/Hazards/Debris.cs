/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 27/02/22                      */
/* LAST MODIFIED - 28/02/22                */
/* ======================================= */
/* DEBRIS                                  */
/* Debris.cs                               */
/* ======================================= */
/* Script handles the behaviour of the     */
/* debris object.                          */
/* ======================================= */

// Directives
using UnityEngine;

public class Debris : MonoBehaviour
{
    private ScreenInfo _screenInfo;
    private Vector2 _velocity; // Velocitu
    private float _maxAngle = 70f; // Maximum angle
    private float _minAngle = 15f; // Minimum angle
    private float _lifeTime = 1.25f; // Life time
    private float _timer; // Timer
    private float _gravity = 200f; // Gravity
    private float _dampening = 0.7f; // dampening
    private float _rotationDegrees = -10f;
    private float _halfWidth; // Half width
    private float _halfHeight; // Half height

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is
     * awoken and determines the direction 
     * of the debris object.
     */
    private void Awake()
    {
        // Obtain the screen info component
        _screenInfo = GameObject.FindObjectOfType<ScreenInfo>();

        // Initialise the timer to 0 seconds
        _timer = 0f;

        // Determine a random angle from the min and max angle in radiams
        float randomAngle = Random.Range(_minAngle, _maxAngle) * Mathf.Deg2Rad;

        // Determine the random vector
        Vector2 randomVector = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));

        // Set the velocity vector to the normalised vector
        _velocity = randomVector;

        // Obtain the half width
        _halfWidth = transform.localScale.x / 2f;

        // Obtain the half height
        _halfHeight = transform.localScale.y / 2f;
    }

    /*
     * SET VELOCITY METHOD
     * 
     * Method is used to set the velocity 
     * of the debris object.
     */
    public void SetVelocity(float velocity)
    {
        // Mutliply the velocity 
        _velocity *= velocity;
    }

    /*
     * FIXED UPDATE METHOD
     * 
     * Method is invoked at fixed time intervals.
     * 
     * Updates the position of the debris object,
     * based on the velocity of the object.
     */
    void FixedUpdate()
    {
        // Obtain the position of the debris
        Vector2 position = transform.position;

        // Increment the timer by the fixed delta time increment
        _timer += Time.fixedDeltaTime;

        // Determine the velocity in the x direction
        _velocity.x *= 0.95f;

        // Determine the velocity in the y direction
        _velocity.y -= _gravity * Time.fixedDeltaTime;

        // Obtain the ray origin
        Vector2 rayOrigin = transform.position;

        // Cast forward ray 
        RaycastHit2D down = Physics2D.Raycast(rayOrigin, Vector2.up, _velocity.y * Time.fixedDeltaTime);

        // Check that if the crash forward ray has hit a collider
        if (down.collider != null)
        {
            // Obtain a ground object from the collider
            Ground ground = down.collider.GetComponent<Ground>();

            // Check the ground exists
            if (ground != null)
            {
                // Allter the position
                position.y = ground.GroundHeight + _halfHeight;

                // Invert the X component of velocity
                _velocity.y = -_velocity.y * _dampening;
            }
        }

        // Cast forward ray 
        RaycastHit2D forward = Physics2D.Raycast(rayOrigin, Vector2.right, _velocity.x * Time.fixedDeltaTime);

        // Check that if the crash forward ray has hit a collider
        if (forward.collider != null)
        {
            // Obtain a ground object from the collider
            Ground ground = forward.collider.GetComponent<Ground>();

            // Check the ground exists
            if (ground != null)
            {
                // Alter the position
                position.x = ground.transform.position.x - ground.HalfWidth - _halfWidth;

                // Invert the X component of velocity and dampen
                _velocity.x = -_velocity.x * _dampening;
            }
        }

        // Alter the X component of position by multiplying the X velocity component by the fixed time increment
        position.x += _velocity.x * Time.fixedDeltaTime;

        // Alter the Y component of position by multiplying the Y velocity component by the fixed time increment
        position.y += _velocity.y * Time.fixedDeltaTime;

        // Set the transform position to the newly determined position vector
        transform.position = position;

        // Rotate the object
        transform.Rotate(Vector3.forward * _rotationDegrees);

        // Check if the debris has left the screen
        if(transform.position.y <= _screenInfo.BottomEdge - _halfHeight)
        {
            // Invoke the destroy debris function
            DestroyDebris();
        }

        // Check if the timer is greater than, or equal to, the life time value
        if (_timer >= _lifeTime)
        {
            // Invoke the destroy debris function
            DestroyDebris();
        }
    }

    /*
     * DESTROY DEBRIS
     * 
     * Method destroys the game object
     * when invoked.
     */
    private void DestroyDebris()
    {
        // Destroy this game object
        Destroy(this.gameObject);
    }
}
