/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 04/02/22                      */
/* LAST MODIFIED - 18/02/22                */
/* ======================================= */
/* GROUND MANAGER                          */
/* GroundManager.cs                        */
/* ======================================= */
/* Script for the ground manager, which    */
/* manages the generation of new ground    */
/* objects.                                */
/* ======================================= */

// Directives
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    // *** SERIALIZED GROUND MANAGER VARIABLES *** //
    [Header ("Ground Game Objects")]
    [SerializeField] private GameObject[] _veryEasyGroundObjects; // Ground object array
    [SerializeField] private GameObject[] _easyGroundObjects; // Ground object array
    [SerializeField] private GameObject[] _mediumGroundObjects; // Ground object array
    [SerializeField] private GameObject[] _hardGroundObjects; // Ground object array
    [SerializeField] private GameObject[] _insaneGroundObjects; // Ground object array

    [Header("Platform Game Objects")]
    [SerializeField] private GameObject[] _veryEasyPlatformObjects; // Ground object array
    [SerializeField] private GameObject[] _easyPlatformObjects; // Ground object array
    [SerializeField] private GameObject[] _mediumPlatformObjects; // Ground object array
    [SerializeField] private GameObject[] _hardPlatformObjects; // Ground object array
    [SerializeField] private GameObject[] _insanePlatformObjects; // Ground object array

    [Header("Ground Manager Settings")]
    [SerializeField] private Sprite[] _groundSprites; // Ground sprites
    [SerializeField] private Sprite[] _platformSprites; // Platform sprites
    [SerializeField] private float _minHeightFromBottom = 1f; // Minimum height the ground appears from the bottom of the screen
    [SerializeField] private float _maxHeigthFromTop = 5f; // Minimum height the ground appears from the top of the screen
    [SerializeField] private float _jumpHeightBuffer = 0.7f; // Jump height buffer
    [SerializeField] private float _jumpDistanceBuffer = 0.7f; // Jump distance buffer
    [SerializeField] private float _minGapDistance = 10f; // Minium gap distance
    [SerializeField] private GameObject[] _obstacles; // Obstacles array
    [SerializeField] private GameObject _healthItem; // Health item
    [SerializeField] private GameObject _lightGate;

    // *** VARIABLE *** //
    private GameState _gameState; // Game state object

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
     * NUMBER OF GROUND SPRITES
     * 
     * Method returns length of the ground
     * sprites array.
     */
    public int NumberOfGroundSprites
    {
        get
        {
            return _groundSprites.Length;
        }
    }

    /*
     * NUMBER OF PLATFORM SPRITES
     * 
     * Method returns length of the platform
     * sprites array.
     */

    public int NumberOfPlatformSprites
    {
        get
        {
            return _platformSprites.Length;
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
     * UPDATE METHOD
     * 
     * Method is invoked at each frame.
     * 
     * Cheap hacky method to ensure the game state
     * is obtained from the scene.
     */
    private void Update()
    {
        // Check if the game state is null
        if(_gameState == null)
        {
            // Obtain the game state object from the scene
            _gameState = GameObject.FindObjectOfType<GameState>();
        }
    }

    /*
     * RETURN GROUND OBJECT METHOD
     * 
     * Method determines which array to
     * take the next ground object from
     * based on the difficulty of the 
     * game.
     */
    public GameObject ReturnGround()
    {
        // Create a new empty ground object
        GameObject ground = null;

        // Determine the difficulty of the game to determine which array to return the ground object from
        if (_gameState.GameDifficulty == GameState.Difficulty.veryEasy)
        {
            // Create a random index number for the very easy ground object array
            int idx = Random.Range(0, _veryEasyGroundObjects.Length);

            // Invoke the return very easy ground method
            ground = ReturnVeryEasyGround(idx);
        }
        else if (_gameState.GameDifficulty == GameState.Difficulty.easy)
        {
            // Create a random index number for the easy ground object array
            int idx = Random.Range(0, _easyGroundObjects.Length);

            // Invoke the return easy ground method
            ground = ReturnEasyGround(idx);
        }
        else if (_gameState.GameDifficulty == GameState.Difficulty.medium)
        {
            // Create a random index number for the medium ground object array
            int idx = Random.Range(0, _mediumGroundObjects.Length);

            // Invoke the return medium ground method
            ground = ReturnMediumGround(idx);
        }
        else if (_gameState.GameDifficulty == GameState.Difficulty.hard)
        {
            // Create a random index number for the medium ground object array
            int idx = Random.Range(0, _hardGroundObjects.Length);

            // Invoke the return hard ground method
            ground = ReturnHardGround (idx);
        }
        else if (_gameState.GameDifficulty == GameState.Difficulty.insane)
        {
            // Create a random index number for the medium ground object array
            int idx = Random.Range(0, _insaneGroundObjects.Length);

            // Invoke the return hard ground method
            ground = ReturnInsaneGround(idx);
        }

        // Return the ground object
        return ground;
    }

    /*
     * RETURN VERY EASY GROUND METHOD
     * 
     * Method returns a game object from the
     * very easy ground objects array.
     */
    public GameObject ReturnVeryEasyGround(int index)
    {
        return _veryEasyGroundObjects[index];
    }

    /*
     * RETURN EASY GROUND METHOD
     * 
     * Method returns a game object from the
     * easy ground objects array
     */
    public GameObject ReturnEasyGround(int index)
    {
        return _easyGroundObjects[index];
    }

    /*
     * RETURN MEDIUM GROUND METHOD
     * 
     * Method returns a game object from the
     * medium ground objects array
     */
    public GameObject ReturnMediumGround(int index)
    {
        return _mediumGroundObjects[index];
    }

    /*
     * RETURN HARD GROUND METHOD
     * 
     * Method returns a game object from the
     * hard ground objects array
     */
    public GameObject ReturnHardGround(int index)
    {
        return _hardGroundObjects[index];
    }

    /*
     * RETURN INSANE GROUND METHOD
     * 
     * Method returns a game object from the
     * insane ground objects array
     */
    public GameObject ReturnInsaneGround(int index)
    {
        return _insaneGroundObjects[index];
    }

    /*
     * RETURN PLATFORM METHOD
     * 
     * Method returns a platform game object
     * based on the difficulty of the game.
     */
    public GameObject ReturnPlatform()
    {
        // Create a new empty ground object
        GameObject platform = null;

        // Determine the difficulty of the game to determine which array to return the ground object from
        if (_gameState.GameDifficulty == GameState.Difficulty.veryEasy)
        {
            // Create a random index number for the very easy ground object array
            int idx = Random.Range(0, _veryEasyPlatformObjects.Length);

            // Invoke the return very easy ground method
            platform = ReturnVeryEasyPlatform(idx);
        }
        else if (_gameState.GameDifficulty == GameState.Difficulty.easy)
        {
            // Create a random index number for the easy ground object array
            int idx = Random.Range(0, _easyPlatformObjects.Length);

            // Invoke the return easy ground method
            platform = ReturnEasyPlatform(idx);
        }
        else if (_gameState.GameDifficulty == GameState.Difficulty.medium)
        {
            // Create a random index number for the medium ground object array
            int idx = Random.Range(0, _mediumPlatformObjects.Length);

            // Invoke the return medium ground method
            platform = ReturnMediumPlatform(idx);
        }
        else if (_gameState.GameDifficulty == GameState.Difficulty.hard)
        {
            // Create a random index number for the medium ground object array
            int idx = Random.Range(0, _hardPlatformObjects.Length);

            // Invoke the return hard ground method
            platform = ReturnHardPlatform(idx);
        }
        else if (_gameState.GameDifficulty == GameState.Difficulty.insane)
        {
            // Create a random index number for the medium ground object array
            int idx = Random.Range(0, _insanePlatformObjects.Length);

            // Invoke the return hard ground method
            platform = ReturnInsanePlatform(idx);
        }

        // Return the ground object
        return platform;
    }

    /*
     * RETURN VERY EASY PLATFORM METHOD
     * 
     * Method returns a game object from the 
     * very easy platform objects array.
     */
    public GameObject ReturnVeryEasyPlatform(int index)
    {
        return _veryEasyPlatformObjects[index];
    }

    /*
     * RETURN EASY PLATFORM METHOD
     * 
     * Method returns a game object from the 
     * easy platform objects array.
     */
    public GameObject ReturnEasyPlatform(int index)
    {
        return _easyPlatformObjects[index];
    }

    /*
     * RETURN MEDIUM PLATFORM METHOD
     * 
     * Method returns a game object from the 
     * medium platform objects array.
     */
    public GameObject ReturnMediumPlatform(int index)
    {
        return _mediumPlatformObjects[index];
    }

    /*
     * RETURN HARD PLATFORM METHOD
     * 
     * Method returns a game object from the 
     * hard platform objects array.
     */
    public GameObject ReturnHardPlatform(int index)
    {
        return _hardPlatformObjects[index];
    }

    /*
     * RETURN INSANE PLATFORM METHOD
     * 
     * Method returns a game object from the 
     * insane platform objects array.
     */
    public GameObject ReturnInsanePlatform(int index)
    {
        return _insanePlatformObjects[index];
    }

    /*
     * RETURN OBSTACLE METHOD
     * 
     * Method returns a random obstacle
     * from the obstacles array.
     */
    public GameObject ReturnObstacle()
    {
        // Create a random index number
        int randomIndex = Random.Range(0, _obstacles.Length);

        // Return the obstacle at the random index number
        return _obstacles[randomIndex];
    }

    /*
     * RETURN GROUND SPRITE METHOD
     * 
     * Method takes in an integer value as
     * an index and returns the sprite found
     * in the ground sprites array at the 
     * index value.
     */
    public Sprite ReturnGroundSprite(int index)
    {
        return _groundSprites[index];
    }

    /*
     * RETURN PLATFORM SPRITE METHOD
     * 
     * Method takes in an integer value as
     * an index and returns the sprite found
     * in the platform sprites array at the 
     * index value.
     */
    public Sprite ReturnPlatformSprite(int index)
    {
        return _platformSprites[index];
    }

    /*
     * RETURN HEALTH ITEM METHOD
     * 
     * Method returns the health item.
     */
    public GameObject ReturnHealthItem()
    {
        return _healthItem;
    }

    public GameObject ReturnLightGate()
    {
        return _lightGate;
    }
}