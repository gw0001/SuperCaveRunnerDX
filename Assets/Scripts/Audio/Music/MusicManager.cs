/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 12/02/22                      */
/* LAST MODIFIED - 12/02/22                */
/* ======================================= */
/* MusicManager                            */
/* MusicManager.cs                         */
/* ======================================= */
/* Script manages the music that is played */
/* in the game.                            */
/* ======================================= */

// Directives
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource; // Music source

    private void Awake()
    {
        // Ensure the music object can be obtained from
        DontDestroyOnLoad(transform.gameObject);

        // Obtain the music source the script is attatched to
        _musicSource = GetComponent<AudioSource>();

        // Set the music source to loop
        _musicSource.loop = true;

        // Play Music
        PlayMusic();
    }

    /*
     * PLAY MUSIC METHOD
     * 
     * When invoked, the method first checks if
     * the music is playing. If the music is 
     * already playing, the script ends.
     * If not, the music is played.
     */
    public void PlayMusic()
    {
        // Check if the music is already playing
        if (_musicSource.isPlaying)
        {
            // Return
            return;
        }

        // Play the music
        _musicSource.Play();
    }

    /*
     * STOP MUSIC
     * 
     * When invoked, the music is stopped.
     */

    public void StopMusic()
    {
        _musicSource.Stop();
    }
}