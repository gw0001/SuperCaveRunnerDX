/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 11/02/22                      */
/* LAST MODIFIED - 28/02/22                */
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
    // *** TITLE SCREEN SETTINGS *** //
    [Header ("Title screen settings")]
    [SerializeField] private float _screenFadeTime = 2f; // Screen fade time
    [SerializeField] private float _titleAppearTime = 2f; // Title appear time

    // *** VARIABLES *** //
    private Image _fadeImage; // Fade image
    private Image _titleImageRegular; // Title image 
    private TitleSuper _titleSuper; // Super image
    private Image _titleCaveRunner; // Cave runner image
    private BigLetter _titleD; // D image
    private BigLetter _titleX; // X image
    private GameObject _pressStart; // Press start game object
    private AnnouncerManager _announcerVoice; // Announcer voice
    private Credits _credits; // Credits
    private Color _visible = new Color(1f, 1f, 1f, 1f); // Visible colour
    private Color _invisible = new Color(1f, 1f, 1f, 0f); // Invisible colour
    private Color _fadeStart = new Color(0f, 0f, 0f, 1f); // Fade start colour
    private float _screenFadeTimer = 0f; // Screen fade timer
    private float _titleAppearTimer = 0f; // title appear timer
    private bool _titleLoaded = false; // Title loaded boolean
    private bool _screenFadedIn = false; // Screen faded in boolean
    private bool _mainTitlePlayed = false; // Main title played boolean
    private bool _dPlayed = false; // D played boolean
    private bool _xPlayed = false; // X played boolean
    private bool _dLoaded = false; // D loaded boolean
    private bool _xLoaded = false; // X Loaded boolean

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
        _titleImageRegular = GameObject.Find("GameTitleRegular").GetComponent<Image>();

        // Obtain the Super image from the scene
        _titleSuper = GameObject.Find("TitleSuper").GetComponent<TitleSuper>();

        // Obtain the Credits script from the scene
        _credits = FindObjectOfType<Credits>();

        // Obtain the cave runner image from the scene
        _titleCaveRunner = GameObject.Find("TitleCaveRunner").GetComponent<Image>();

        // Obtain the D image from the scene
        _titleD = GameObject.Find("TitleD").GetComponent<BigLetter>();

        // Obtain the X image from the scene
        _titleX = GameObject.Find("TitleX").GetComponent<BigLetter>();

        // Obtain the press start component from the scene
        _pressStart = GameObject.Find("PressStartText");

        // Set the alpha of the fade image so the fade start colour
        _fadeImage.color = _fadeStart;

        // Set the colour of the title image to the invisible colour
        _titleImageRegular.color = _invisible;

        // Set the cave runner image to the invisible colour
        _titleCaveRunner.color = _invisible;

        // Set the d image to the invisible colour
        _titleD.enabled = false;

        // Set the X 
        _titleX.enabled = false;

        // Disable the press start component
        _pressStart.SetActive(false);

        // Obtain the announcer voice from the game scene
        _announcerVoice = GameObject.FindObjectOfType<AnnouncerManager>();
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
        // Check the voice is not the "Teuchter" voice
        if (_announcerVoice.GameAnnouncer.VoiceType != Announcer.AnnouncerVoice.teuchter)
        {
            // Invoke the arcade intro method
            AnnouncerArcadeIntro();
        }
        else
        {
            // Invoke the teuchter intro method
            AnnouncerTeuchterIntro();
        }
    }

    /*
     * ANNOUNCER ARCADE INTRO METHOD
     * 
     * Method is used to control the title with the 
     * standard arcade announcers.
     */
    private void AnnouncerArcadeIntro()
    {
        // Check if the screen has not faded in
        if (!_screenFadedIn)
        {
            // Increment the screen fade timer by delta time
            _screenFadeTimer += Time.deltaTime;

            // Determine the alpha value
            float alpha = 1 - (_screenFadeTimer / _screenFadeTime);

            // Alter the colour of the fade image with the new alpha colour
            _fadeImage.color = new Vector4(0f, 0f, 0f, alpha);

            // Check if the screen fade time is greater than or equal to, the screen fade time
            if (_screenFadeTimer >= _screenFadeTime)
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
            if (!_titleLoaded)
            {
                // Increment the title appear timer by delta time
                _titleAppearTimer += Time.deltaTime;

                // Determine the alpha value
                float alpha = _titleAppearTimer / _titleAppearTime;

                // Set the colour of the title image with the new alpha value
                _titleCaveRunner.color = new Vector4(1f, 1f, 1f, alpha);

                // Check if the title appear timer is greater than, or equal to, the title appear time
                if (_titleAppearTimer >= _titleAppearTime && !_mainTitlePlayed)
                {
                    // Set the title colour so that it will be completely visible
                    _titleCaveRunner.color = _visible;

                    // Enable the press start object
                    _pressStart.SetActive(true);

                    // Enable the credits
                    _credits.EnableCredits();

                    // Have the "Super" move to the title
                    _titleSuper.CanMove();

                    // Play the title sound effect
                    _announcerVoice.GameAnnouncer.PlaySound(0);

                    // Set main title played to true
                    _mainTitlePlayed = true;
                }

                // Check if the title appear time is greater than sum of the appear time and "super cave runner" time, and main title played is true and d played is false
                if (_titleAppearTimer >= (_titleAppearTime + _announcerVoice.GameAnnouncer.SuperCaveRunnerTime) && _mainTitlePlayed && !_dPlayed)
                {
                    // Check is "D" has not been loaded
                    if(!_dLoaded)
                    {
                        // Enable the "D" for the title
                        _titleD.enabled = true;

                        // Set "D" loaded to true
                        _dLoaded = true;
                    }

                    // Stop any current audio from the announcer
                    _announcerVoice.GameAnnouncer.Stop();

                    // Play the "D" sound effect
                    _announcerVoice.GameAnnouncer.PlaySound(1);

                    // Set d played to true
                    _dPlayed = true;
                }

                // Check if the title appear time is greater than sum of the appear time, "super cave runner" time and "D" time, and main title played is true and d played is false
                if (_titleAppearTimer >= (_titleAppearTime + _announcerVoice.GameAnnouncer.SuperCaveRunnerTime + +_announcerVoice.GameAnnouncer.DTime) && _mainTitlePlayed && _dPlayed && !_xPlayed)
                {
                    // Check if "X" has not loaded
                    if (!_xLoaded)
                    {
                        // Enable the "X" for the title
                        _titleX.enabled = true;

                        // Set "X" loaded to true
                        _xLoaded = true;
                    }

                    // Stop any current audio from the announcer
                    _announcerVoice.GameAnnouncer.Stop();

                    // Play the "X" voice
                    _announcerVoice.GameAnnouncer.PlaySound(2);

                    // Set x played boolean to true
                    _xPlayed = true;

                    // Set title loaded to true
                    _titleLoaded = true;
                }
            }
        }
    }

    /*
     * ANNOUNCER TEUCHTER INTRO METHOD
     * 
     * Method is used to control the title
     * with the special teuchter announcer.
     */
    private void AnnouncerTeuchterIntro()
    {
        // Check if the screen has not faded in
        if (!_screenFadedIn)
        {
            // Increment the screen fade timer by delta time
            _screenFadeTimer += Time.deltaTime;

            // Determine the alpha value
            float alpha = 1 - (_screenFadeTimer / _screenFadeTime);

            // Alter the colour of the fade image with the new alpha colour
            _fadeImage.color = new Vector4(0f, 0f, 0f, alpha);

            // Check if the screen fade time is greater than or equal to, the screen fade time
            if (_screenFadeTimer >= _screenFadeTime)
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
            if (!_titleLoaded)
            {
                // Increment the title appear timer by delta time
                _titleAppearTimer += Time.deltaTime;

                // Determine the alpha value
                float alpha = _titleAppearTimer / _titleAppearTime;

                // Set the colour of the title image with the new alpha value
                _titleImageRegular.color = new Vector4(1f, 1f, 1f, alpha);

                // Check if the title appear timer is greater than, or equal to, the title appear time
                if (_titleAppearTimer >= _titleAppearTime && !_mainTitlePlayed)
                {
                    // Set the title colour so that it will be completely visible
                    _titleImageRegular.color = new Vector4(1f, 1f, 1f, 1f);

                    // Enable the press start object
                    _pressStart.SetActive(true);

                    // Enable the credits
                    _credits.EnableCredits();

                    // Play the title sound effect
                    _announcerVoice.GameAnnouncer.PlaySound(0);

                    // Set the main title boolean played to true
                    _mainTitlePlayed = true;

                    // Set title loaded to true
                    _titleLoaded = true;
                }
            }
        }
    }
}