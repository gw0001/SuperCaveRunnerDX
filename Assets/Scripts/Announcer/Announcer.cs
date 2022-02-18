/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 13/02/22                      */
/* LAST MODIFIED - 18/02/22                */
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
    // *** SERIALISED ANNOUNCER SETTINGS *** //
    [Header ("Announcer settings")]
    [SerializeField] private AudioSource _announcerAudio; // Announcer audio source
    [SerializeField] private AnnouncerVoice _voiceType; // Voice type
    [SerializeField] private float _superCaveRunnerTime = 3.9f; // Time it takes to finish saying "Super Cave Runner"
    [SerializeField] private float _dTime = 0.75f; // Time it takes to finish saying "D"

    [Header("Announcer Audio Clips")]
    [SerializeField] private AudioClip[] _audio; // Audio array

    // Announcer voice enumerator
    public enum AnnouncerVoice
    {
        arcade1,
        arcade2,
        teuchter
    }

    /*
     * VOICE TYPE GET METHOD
     * 
     * Method returns the voice type of the
     * announcer.
     */
    public AnnouncerVoice VoiceType
    {
        get
        {
            return _voiceType;
        }
    }

    /*
     * SUPER CAVE RUNNER TIME GET METHOD
     * 
     * Method returns the time the announcer takes
     * to say "Super Cave Runner"
     */
    public float SuperCaveRunnerTime
    {
        get
        {
            return _superCaveRunnerTime;
        }
    }

    /*
     * D TIME GET METHOD
     * 
     * Method returns the time it takes the
     * announcer to say "D"
     */
    public float DTime
    {
        get
        {
            return _dTime;
        }
    }

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is awoken
     */
    void Awake()
    {
        // Add an audio source component
        _announcerAudio = gameObject.AddComponent<AudioSource>();

        // Disable the play on awake function
        _announcerAudio.playOnAwake = false;

        // Prevent the announcer from being destroyed
        DontDestroyOnLoad(this.gameObject);
    }

    /*
     * PLAY SOUND METHOD
     * 
     * Method plays a sound at an index
     * value when invoked. However, if
     * audio is currently playing,
     * the method will not play a new sound.
     */
    public void PlaySound(int index)
    {
        // Check if the announcer is playing any audio
        if (_announcerAudio.isPlaying)
        {
            // Return
            return;
        }

        // Set the announcer clip
        _announcerAudio.clip = _audio[index];

        // Play the audio clip
        _announcerAudio.Play();
    }

    /*
     * MUTE METHOD
     * 
     * Method mutes the audio when invoked.
     */
    public void Mute()
    {
        _announcerAudio.mute = true;
    }

    /*
     * UNMUTE METHOD
     * 
     * Method unmutes the audio when invoked.
     */
    public void Unmute()
    {
        _announcerAudio.mute = false;
    }

    /*
     * STOP METHOD
     * 
     * Method stops the audio when invoked.
     */
    public void Stop()
    {
        _announcerAudio.Stop();
    }
}