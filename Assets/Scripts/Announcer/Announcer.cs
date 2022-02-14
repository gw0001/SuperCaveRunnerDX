/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 13/02/22                      */
/* LAST MODIFIED - 14/02/22                */
/* ======================================= */
/* ANNOUNCER                               */
/* Announcer.cs                            */
/* ======================================= */
/* Script for handling the behaviour of    */
/* announcer objects.                      */
/* ======================================= */

// Directives
using UnityEngine;

public class Announcer : MonoBehaviour
{
    // Announcer voice enumerator
    public enum AnnouncerVoice
    {
        arcade1,
        arcade2,
        teuchter
    }

    [SerializeField] private AudioClip _super; // "Super" clip
    [SerializeField] private AudioClip _caveRunner; // "Cave Runner" clip
    [SerializeField] private AudioClip _title; // "Super Cave Runner" or "Super Cave Runner DX" clip
    [SerializeField] private AudioClip _d; // "D" clip
    [SerializeField] private AudioClip _x; // "X" clip
    [SerializeField] private AudioClip _ready; // "Ready" clip
    [SerializeField] private AudioClip _go; // "Go!" clip
    [SerializeField] private AudioSource _announcerAudio; // Announcer audio source
    [SerializeField] private AnnouncerVoice _voiceType; // Voice type

    private AudioClip[] _audio; // Audio array
    [SerializeField] private int _numberOfClips = 7;

    public AnnouncerVoice VoiceType
    {
        get
        {
            return _voiceType;
        }
    }

    void Awake()
    {
        // Initialise the array
        InitialiseComponent();



    }

    public void InitialiseComponent()
    {
        // Initialise the audio array by the number of voice clips
        _audio = new AudioClip[_numberOfClips];

        // Set element 0 to "Super"
        _audio[0] = _super;

        // Set element 1 to "Cave Runner"
        _audio[1] = _caveRunner;

        // Set element 2 to "Super Cave Runner"
        _audio[2] = _title;

        // Set element 3 to "D"
        _audio[3] = _d;

        // Set element 4 to "X"
        _audio[4] = _x;

        // Set element 5 to "Ready..."
        _audio[5] = _ready;

        // Set element 6 to "Go!"
        _audio[6] = _go;

        // Prevent the assets from being destroyed on loading of new scenes
        DontDestroyOnLoad(_super);
        DontDestroyOnLoad(_caveRunner);
        DontDestroyOnLoad(_title);
        DontDestroyOnLoad(_d);
        DontDestroyOnLoad(_x);
        DontDestroyOnLoad(_ready);
        DontDestroyOnLoad(_go);

        // Add an audio source component
        _announcerAudio = gameObject.AddComponent<AudioSource>();

        // Disable the play on awake function
        _announcerAudio.playOnAwake = false;

        // Prevent the announcer from being destroyed
        DontDestroyOnLoad(this.gameObject);
    }

    /*
     * 
     */
    public void PlaySound(int index)
    {
        if (_announcerAudio.isPlaying)
        {
            return;
        }

        _announcerAudio.clip = _audio[index];
        _announcerAudio.Play();
    }

    /*
     * 
     */
    public void PlaySound(int index, bool value)
    {
        // Check if the value is true
        if(value == true)
        {
            // Stop the sound that is currently playing
            _announcerAudio.Stop();
        }
        else
        {
            return;
        }

        // Change the audio clip to the element held at the index of the audio array
        _announcerAudio.clip = _audio[index];

        // Play the audio clip
        _announcerAudio.Play();
    }

    public void Mute()
    {
        _announcerAudio.mute = true;
    }

    public void Unmute()
    {
        _announcerAudio.mute = false;
    }


    public void StopSound()
    {
        _announcerAudio.Stop();
    }
}
