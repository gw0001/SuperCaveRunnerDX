/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 11/02/22                      */
/* LAST MODIFIED - 13/02/22                */
/* ======================================= */
/* TITLE SCREEN MANAGER                    */
/* TitleScreenManager.cs                   */
/* ======================================= */
/* Script manages various items for the    */
/* title screen.                           */
/* ======================================= */

// Directives
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] private float _screenFadeTime = 2f; // Screen fade time
    [SerializeField] private float _titleAppearTime = 2f; // Title appear time

    private Image _fadeImage; // Fade image
    private Image _titleImage; // Title image 
    private GameObject _pressStart; // Press start game object
    //private SoundEffect _titleSound; // Title Sound
    private AnnouncerManager _announcerVoice;

    private float _screenFadeTimer = 0f; // Screen fade timer
    private float _titleAppearTimer = 0f; // title appear timer
    private bool _titleLoaded = false; // Title loaded boolean
    private bool _screenFadedIn = false; // Screen faded in boolean

    /*
     * AWAKE METHOD
     * 
     * Method is activated when the script is first
     * woken up
     */
    private void Awake()
    {
        // Obtain the fade image
        _fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();

        // Obtain the title image from the scene
        _titleImage = GameObject.Find("GameTitle").GetComponent<Image>();

        // Obtain the press start component from the scene
        _pressStart = GameObject.Find("PressStartText");

        // Set the alpha of the fade image so it is visible to begin with
        _fadeImage.color = new Vector4(0f, 0f, 0f, 1f);

        // Set the colour of the title image so it is invisible to begin with
        _titleImage.color = new Vector4(1f, 1f, 1f, 0f);

        // Disable the press start component
        _pressStart.SetActive(false);

        //
        _announcerVoice = GameObject.FindObjectOfType<AnnouncerManager>();

        // Obtain the title sound effect
        //_titleSound = GameObject.Find("TitleSound01").GetComponent<SoundEffect>();
    }

    /*
     * UPDATE METHOD
     * 
     * Method updates at every frame.
     * 
     * First begins by controlling the screen
     * to fade in.
     * 
     * Once the screen has faded in, the tile 
     * image is then faded in.
     * 
     * After the title has faded in, the start
     * text is then enabled.
     */
    private void Update()
    {
        // Check if the screen has not faded in
        if(!_screenFadedIn)
        {
            // Increment the screen fade timer by delta time
            _screenFadeTimer += Time.deltaTime;

            // Determine the alpha value
            float alpha = 1 - (_screenFadeTimer / _screenFadeTime);

            // Alter the colour of the fade image with the new alpha colour
            _fadeImage.color = new Vector4(0f, 0f, 0f, alpha);

            // Check if the screen fade time is greater than or equal to, the screen fade time
            if(_screenFadeTimer >= _screenFadeTime)
            {
                // Set the colour of the fade image to make it completely invisible
                _fadeImage.color = new Vector4(0f, 0f, 0f, 0f);

                // Set the screen faded in boolean to true
                _screenFadedIn = true;
            }
        }
        else
        {
            // Check if the title has not loaded
            if(!_titleLoaded)
            {
                // Increment the title appear timer by delta time
                _titleAppearTimer += Time.deltaTime;

                // Determine the alpha value
                float alpha = _titleAppearTimer / _titleAppearTime;

                // Set the colour of the title image with the new alpha value
                _titleImage.color = new Vector4(1f, 1f, 1f, alpha);

                // Check if the title appear timer is greater than, or equal to, the title appear time
                if(_titleAppearTimer >= _titleAppearTime)
                {
                    // Set the title colour so that it will be completely visible
                    _titleImage.color = new Vector4(1f, 1f, 1f, 1f);

                    // Set title loaded to true
                    _titleLoaded = true;

                    // Enable the press start object
                    _pressStart.SetActive(true);

                    // Play the title sound effect
                    //_titleSound.PlaySoundEffect();
                    _announcerVoice.GameAnnouncer.PlaySound(2);
                }
            }
        }
    }
}