/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 25/02/22                      */
/* LAST MODIFIED - 25/02/22                */
/* ======================================= */
/* PLAYER SOUND MANAGER                    */
/* PlayerSoundManager.cs                   */
/* ======================================= */
/* Script manages the player sounds for    */
/* jumping, crashing, picking up health,   */
/* losing health, and dying.               */
/* ======================================= */

// Directives
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    // *** SERIALISED VARIABLES *** //
    [Header("Jump audio settings")]
    [SerializeField] private AudioClip _jumpClip; // Jump audio clip
    [SerializeField] private float _jumpPitchMin = 0.85f; // Minimum pitch value
    [SerializeField] private float _jumpPitchMax = 1.0f; // Maximum pitch value

    [Header ("Crash audio settings")]
    [SerializeField] private AudioClip _crashClip; // Crash audio clip
    [SerializeField] private float _crashPitchMin = 0.85f; // Minimum pitch value
    [SerializeField] private float _crashPitchMax = 1.0f; // Maximum pitch value

    [Header ("Health audio settings")]
    [SerializeField] private AudioClip _healthGainedClip; // Healh gained clip
    [SerializeField] private AudioClip _healthLostClip; // Health lost 
    [SerializeField] private float _healthPitchMin = 0.85f; // Minimum health pitch
    [SerializeField] private float _healthPitchMax = 1.0f; // Maximum health pitch

    [Header ("Death audio settings")]
    [SerializeField] private AudioClip _deathFallClip; // Death fall clip
    [SerializeField] private AudioClip _deathBeamClip; // Death light gate beam clip
    [SerializeField] private AudioClip _deathObstacleClip; // Death obstacle clip

    // *** VARIABLES *** //
    private AudioSource _jumpSource; // Jump audio source
    private AudioSource _crashSource; // Crash audio source
    private AudioSource _healthSource; // Health audio source
    private AudioSource _deathSource; // Death source
    private bool _jumpPlaying; // Jump plauing boolean
    private bool _crashPlaying; // Crash playing boolean
    private bool _healthPlaying; // Health playing boolean
    private bool _deathPlaying; // Death playing boolean

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is awoken.
     * Method invokes the methods it initialise the 
     * audio sources.
     */
    private void Awake()
    {
        // Initialise jump source
        InitialiseJumpSource();

        // Initialise the crash source
        InitialiseCrashSource();

        // Initialise health source
        InitialiseHealthSource();

        // Initialise the death source
        InitialiseDeathSource();
    }


    /*
     * UPDATE METHOD
     * 
     * Method is invoked once every frame.
     * 
     * Method checks if the audio sources are
     * not playing and sets the appripriate
     * booleans to false.
     */
    private void Update()
    {
        // Check if the jump source is not playing any audio
        if (!_jumpSource.isPlaying)
        {
            // Set jump played to false
            _jumpPlaying = false;
        }

        // Check if the crash source is not playing audio
        if(!_crashSource.isPlaying)
        {
            // Set crash playing to false
            _crashPlaying = false;
        }

        // Check if the health source is not playing any audio
        if(!_healthSource.isPlaying)
        {
            // Set health played to false
            _healthPlaying = false;
        }
    }

    /*
     * INITIALISE JUMP SOURCE METHOD
     * 
     * Method initialises the jump audio source.
     */
    private void InitialiseJumpSource()
    {
        // Obtain the jump source from the game scene
        _jumpSource = GameObject.Find("JumpAudioChannel").GetComponent<AudioSource>();

        // Load the jump clip to an instance of the jump clip
        _jumpSource.clip = Instantiate(_jumpClip);

        // Prevent the jump sound effect from looping
        _jumpSource.loop = false;

        // Prevent the jump sound being played when audio source is awoken
        _jumpSource.playOnAwake = false;

        // Initialise the jump playing boolean to false
        _jumpPlaying = false;
    }

    /*
     * INITIALISE CRASH SOURCE METHOD
     * 
     * Method initialises the crash audio source.
     */
    private void InitialiseCrashSource()
    {
        // Obtain the crash source channel
        _crashSource = GameObject.Find("CrashAudioChannel").GetComponent<AudioSource>();

        // Set the audio clip to an instance of the crash clip
        _crashSource.clip = Instantiate(_crashClip);

        // Prevent the crash source from looping
        _crashSource.loop = false;

        // Prevent the crash source playing when awoken
        _crashSource.playOnAwake = false;

        // Initialise the crash playing boolean to false
        _crashPlaying = false;
    }

    /*
     * INITIALISE HEALTH SOURCE METHOD
     * 
     * Method initialises the health audio source.
     */
    private void InitialiseHealthSource()
    {
        // Obtain the health source channel
        _healthSource = GameObject.Find("HealthAudioChannel").GetComponent<AudioSource>();

        // Prevent the health source from looping
        _healthSource.loop = false;

        // Prevent the health source from playing when awoken
        _healthSource.playOnAwake = false;

        // Initialise the health playing boolean to false
        _healthPlaying = false;
    }

    /*
     * INITIALISE DEATH SOURCE METHOD
     * 
     * Method initialises the death audio source.
     */
    private void InitialiseDeathSource()
    {
        // Obtain the death source channel
        _deathSource = GameObject.Find("DeathAudioChannel").GetComponent<AudioSource>();

        // Prevent the death source from looping
        _deathSource.loop = false;

        // Prevent the death source from playing when awoken
        _deathSource.playOnAwake = false;

        // Initialise the dealth playing boolean to false
        _deathPlaying = false;
    }

    /*
     * PLAY JUMP METHOD
     * 
     * Method plays the jump sound at a random
     * pitch when invoked.
     */
    public void PlayJump()
    {
        // Check that the jump sound isn't playing
        if(!_jumpPlaying)
        {
            // Determine a pitch value
            float pitch = Random.Range(_jumpPitchMin, _jumpPitchMax);

            // Set the pitch for the audio source
            _jumpSource.pitch = pitch;

            // Play the jump source
            _jumpSource.Play();

            // Set jump played to true
            _jumpPlaying = true;
        }
    }

    /*
     * PLAY CRASH METHOD
     * 
     * Method plays the crash sound at a random
     * pitch when invoked.
     */
    public void PlayCrash()
    {
        // Check that the crash sound isn't playing
        if(!_crashPlaying)
        {
            // Determine a random pitch value
            float pitch = Random.Range(_crashPitchMin, _crashPitchMax);

            // Set the pitch for the crash audio source
            _crashSource.pitch = pitch;

            // Play the crash source audio
            _crashSource.Play();

            // Set the crash playing to true
            _crashPlaying = true;
        }
    }

    /*
     * PLAY HEALTH GAIN METHOD
     * 
     * Method plays the health gain sound at a random
     * pitch when invoked
     */
    public void PlayHealthGain()
    {
        // Check that the health is not play
        if(!_healthPlaying)
        {
            // Determine a pitch value
            float pitch = Random.Range(_healthPitchMin, _healthPitchMax);

            // Set the pitch 
            _healthSource.pitch = pitch;

            // Set the clip as an instance of the health gained clip
            _healthSource.clip = Instantiate(_healthGainedClip);

            // Play the health source
            _healthSource.Play();

            // Set the health played to true
            _healthPlaying = true;
        }
    }

    /*
     * PLAY HEALTH LOSS METHOD
     * 
     * Method plays the health loss sound at a random
     * pitch when invoked
     */
    public void PlayHealthLoss()
    {
        // Check that the health is not play
        if (!_healthPlaying)
        {
            // Determine a pitch value
            float pitch = Random.Range(_healthPitchMin, _healthPitchMax);

            // Set the pitch 
            _healthSource.pitch = pitch;

            // Set the clip as an instance of the health lost clip
            _healthSource.clip = Instantiate(_healthLostClip);

            // Play the health source
            _healthSource.Play();

            // Set the health played as true
            _healthPlaying = true;
        }
    }

    /*
     * PLAY DEATH OBSTACLE METHOD
     * 
     * Method plays the death by obstacle sound 
     * when invoked.
     */
    public void PlayDeathObstacle()
    {
        // Check that a death sound isn't playing
        if(!_deathPlaying)
        {
            // Set the death source clip to the obstacle death clip
            _deathSource.clip = Instantiate(_deathObstacleClip);

            // Play the death audio
            _deathSource.Play();

            // Set death playing to true
            _deathPlaying = true;
        }
    }

    /*
     * PLAY DEATH LIGHT GATE METHOD
     * 
     * Method plays the death by light gate sound
     * when invoked.
     */
    public void PlayDeathLightGate()
    {
        // Check that a death sound isn't playing
        if (!_deathPlaying)
        {
            // Set the death source clip to an instance of the death beam clip
            _deathSource.clip = Instantiate(_deathBeamClip);

            // Play the death source audio
            _deathSource.Play();

            // Set death playing to true
            _deathPlaying = true;
        }
    }

    /*
     * PLAY DEATH FALL METHOD
     * 
     * Method plays the death by fall sound 
     * when invoked.
     */
    public void PlayDeathFall()
    {
        // Check that a death sound isn't playing
        if (!_deathPlaying)
        {
            // Set the death source to an instance of the death fall clip
            _deathSource.clip = Instantiate(_deathFallClip);

            // Play the death source audio
            _deathSource.Play();

            // Set the death playing to true
            _deathPlaying = true;
        }
    }
}
