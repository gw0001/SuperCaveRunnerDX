/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 3/02/22                       */
/* LAST MODIFIED - 03/02/22                */
/* ======================================= */
/* SCREEN INFO                             */  
/* ScreenInfo.cs                           */
/* ======================================= */
/* Script is used to determine the edge of */
/* the screen as real world coordinates.   */
/* ======================================= */

// Directives
using UnityEngine;

public class ScreenInfo : MonoBehaviour
{
    // *** SERIALIZED SCREEN INFO SETTINGS *** //
    [SerializeField] private int _screenSegments = 8;

    // *** VARIABLES *** //
    private Camera _camera; // Camera
    private float _rightEdge; // Right Edge
    private float _leftEdge; // Left Edge
    private float _topEdge; // Top Edge
    private float _bottomEdge; // Bottom Edge

    /*
     * LEFT EDGE GET METHOD
     * 
     * Method returns the value held by
     * the left edge variable
     */
    public float LeftEdge
    {
        get
        {
            return _leftEdge;
        }
    }

    /*
     * RIGHT EDGE GET METHOD
     * 
     * Method returns the value held bu
     * the right edge variable.
     */
    public float RightEdge
    {
        get
        {
            return _rightEdge;
        }
    }

    /*
     * TOP EDGE GET METHOD
     * 
     * Method returns the value held by
     * the top edge variable.
     */
    public float TopEdge
    {
        get
        {
            return _topEdge;
        }
    }

    /*
     * BOTTOM EDGE GET METHOD
     * 
     * Method returns the value held by
     * the bottom edge variable.
     */
    public float BottomEdge
    {
        get
        {
            return _bottomEdge;
        }
    }

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is
     * awoken.
     * 
     * Method obtains the the camera from the
     * scene and then invokes the method to
     * update the edge coordinates.
     */
    void Awake()
    {
        // Obtain the main camera from the scene
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // Update the edge coordinates
        UpdateEdgeCoords();

    }

    /*
     * SEGMENT WIDTH METHOD
     * 
     * When invoked, the method determines
     * and returns the width of a screen
     * segment.
     */
    public float SegmentWidth()
    {
        // Determine the screen size based on the world coordinates
        float screenSize = _rightEdge - _leftEdge;

        // Determine the width a screen segment
        float segmentWidth = screenSize / _screenSegments;

        // Return the segment width
        return segmentWidth;
    }

    /*
     * UPDATE EDGE COORDS METHOD
     * 
     * When invoked, the method first determines the top right
     * and the bottom left scree corner coordinates to world 
     * coordinates.
     * 
     * The method then stores the appropriate values to the
     * appropriate edge variables.
     */
    public void UpdateEdgeCoords()
    {
        // Obtain the world coordinates of the top right corner
        Vector2 topRightCorner = (Vector2)_camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, _camera.nearClipPlane));

        // Obtain the world coordinates of the bottom left corner
        Vector2 bottomleftCorner = (Vector2)_camera.ScreenToWorldPoint(new Vector3(0, 0, _camera.nearClipPlane));

        // Set the left edge
        _leftEdge = bottomleftCorner.x;

        // Set the bottom edge value
        _bottomEdge = bottomleftCorner.y;

        // Set the right edge value
        _rightEdge = topRightCorner.x;

        // Set the top edge value
        _topEdge = topRightCorner.y;
    }
}