/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 05/02/22                      */
/* LAST MODIFIED - 14/02/22                */
/* ======================================= */
/* GROUND MANAGER                          */
/* GroundManager.cs                        */
/* ======================================= */
/* Script for the ground manager, which    */
/* manages the generation of new ground    */
/* objects.                                */
/* ======================================= */

// Directives
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    // *** SERIALIZED GROUND MANAGER VARIABLES *** //
    [Header ("Ground Manager Settings")]
    [SerializeField] private GameObject[] _groundObjects; // Ground object array
    [SerializeField] private Sprite[] _groundSprites; // Ground sprites
    [SerializeField] private float _minHeightFromBottom = 1f; // Minimum height the ground appears from the bottom of the screen
    [SerializeField] private float _maxHeigthFromTop = 5f; // Minimum height the ground appears from the top of the screen
    [SerializeField] private float _jumpHeightBuffer = 0.7f; // Jump height buffer
    [SerializeField] private float _jumpDistanceBuffer = 0.7f; // Jump distance buffer
    [SerializeField] private float _minGapDistance = 10f; // Minium gap distance
    [SerializeField] private GameObject _obstacle; // Obstacle
    [SerializeField] private GameObject _health; // Health item



    private GameState _gameState; // Game state object

    // *** SERIALIZED GROUND ARRAYS FOR DIFFERENT OBJECTS *** //
    [SerializeField] private GameObject[] _veryEasyGroundObjects; // Ground object array
    [SerializeField] private GameObject[] _veryEasyPlatformObjects; // Ground object array
    [SerializeField] private GameObject[] _easyGroundObjects; // Ground object array
    [SerializeField] private GameObject[] _easyPlatformObjects; // Ground object array
    [SerializeField] private GameObject[] _mediumGroundObjects; // Ground object array
    [SerializeField] private GameObject[] _mediumPlatformObjects; // Ground object array
    [SerializeField] private GameObject[] _hardGroundObjects; // Ground object array
    [SerializeField] private GameObject[] _hardPlatformObjects; // Ground object array
    [SerializeField] private GameObject[] _insaneGroundObjects; // Ground object array
    [SerializeField] private GameObject[] _insanePlatformObjects; // Ground object array

    /*
     * JUMP BUFFFER GET METHOD
     * 
     * Method returns the value held by the
     * jump height buffer variable.
     */
    public float JumpHeightBuffer
    {
        get
        {
            return _jumpHeightBuffer;
        }
    }

    /*
     * JUMP DISTANCE BUFFER GET METHOD
     * 
     * Method returns the value held by the
     * jump distance buffer variable.
     */
    public float JumpDistanceBuffer
    {
        get
        {
            return _jumpDistanceBuffer;
        }
    }

    /*
     * NUMBER OF OBJECTS GET METHOD
     * 
     * Method returns the value held
     * by the number of objects
     * variable.
     */
    public int NumberOfObjects
    {
        get
        {
            return _groundObjects.Length;
        }
    }

    /*
     * NUMBER OF SPRITES
     * 
     * 
     */
    public int NumberOfSprites
    {
        get
        {
            return _groundSprites.Length;
        }
    }

    /*
     * MAX HEIGHT FROM TOP GET METHOD
     * 
     * Method returns the value held
     * bu the max height from top 
     * variable
     */
    public float MaxHeightFromTop
    {
        get
        {
            return _maxHeigthFromTop;
        }
    }

    /*
     * MIN HEIGHT FROM BOTTOM GET METHOD
     * 
     * Method returns the value held
     * by the min height from bottom
     * variable.
     */
    public float MinHeightFromBottom
    {
        get
        {
            return _minHeightFromBottom;
        }
    }

    /*
     * MIN GAP DISTANCE GET METHOD
     * 
     * Method returns the value held by the
     * min gap distance variable.
     */
    public float MinGapDistance
    {
        get
        {
            return _minGapDistance;
        }
    }

    /*
     * RETURN GROUND OBJECT METHOD
     * 
     * Method takes in an integer value and uses
     * the value as an index to return a game
     * object from the ground objects array.
     */
    public GameObject ReturnGround(int index)
    {
        // Obtain a ground object from the index
        return _groundObjects[index];
    }
    //public GameObject ReturnGround(int index)
    //{
    //    GameObject ground = new GameObject();





    //    if(_gameState.GameDifficulty == GameState.Difficulty.veryEasy)
    //    {
    //        ground = ReturnVeryEasyGround();
    //    }
    //    else if (_gameState.GameDifficulty == GameState.Difficulty.easy)
    //    {

    //    }
    //    else if (_gameState.GameDifficulty == GameState.Difficulty.medium)
    //    {

    //    }
    //    else if (_gameState.GameDifficulty == GameState.Difficulty.hard)
    //    {

    //    }
    //    else if (_gameState.GameDifficulty == GameState.Difficulty.insane)
    //    {

    //    }

    //    return ground;
    //}


    //public GameObject ReturnVeryEasyGround(int index)
    //{
    //    return _veryEasyGroundObjects[index];
    //}

    //public GameObject ReturnEasyGround(int index)
    //{
    //    return _easyGroundObjects[index];
    //}

    //public GameObject ReturnMediumGround(int index)
    //{
    //    return _mediumGroundObjects[index];
    //}

    //public GameObject ReturnHardGround(int index)
    //{
    //    return _hardGroundObjects[index];
    //}

    //public GameObject ReturnInsaneGround(int index)
    //{
    //    return _insaneGroundObjects[index];
    //}


    /*
     * RETURN OBSTACLE METHOD
     * 
     * Method returns an obstacle
     */
    public GameObject ReturnObstacle()
    {
        return _obstacle;
    }

    /*
     * RETURN GROUND SPRITE METHOD
     * 
     * Method takes in an integer value as
     * an index and returns the sprite found
     * in the ground sprites array at the 
     * index.
     */
    public Sprite ReturnGroundSprite(int index)
    {
        return _groundSprites[index];
    }

    public GameObject ReturnHealth()
    {
        return _health;
    }
}
