/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 12/02/22                      */
/* LAST MODIFIED - 12/02/22                */
/* ======================================= */
/* SOUND EFFECT                            */
/* SoundEffect.cs                          */
/* ======================================= */
/* Script manages the behaviour of sound   */
/* effect objects.                         */
/* ======================================= */

// Directives
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    // *** SERIALISED VARIABLES *** //
    [SerializeField] private float _minPitch = 0.5f;
    [SerializeField] private float _maxPitch = 1.0f;

    // *** VARIABLE *** //
    private AudioSource _soundEffect; // Sound effect audio source

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is awoken.
     */
    void Awake()
    {
        // Obtain the sound effect from the game object script is attached to
        _soundEffect = GetComponent<AudioSource>();
    }

    /*
     * PLAY SOUND EFFECT
     * 
     * When inoked, the sound effect
     * is instructed to play.
     */
    public void PlaySoundEffect()
    {
        _soundEffect.Play();
    }

    /*
     * PLAY WITH RANDOM PITCH
     */
    public void PlayWithRandomPitch()
    {
        float randomPitch = Random.Range(_minPitch, _maxPitch);

        _soundEffect.pitch = randomPitch;

        _soundEffect.Play();
    }




    /*
     * STOP SOUND EFFECT
     * 
     * When invoked, the sound effect
     * is instructed to stop.
     */
    public void StopSoundEffect()
    {
        _soundEffect.Stop();
    }
}