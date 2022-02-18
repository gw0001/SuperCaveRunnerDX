/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 13/02/22                      */
/* LAST MODIFIED - 18/02/22                */
/* ======================================= */
/* ANNOUNCER MANAGER                       */
/* AnnouncerManager.cs                     */
/* ======================================= */
/* Script manages which announcer will be  */
/* used when the game begins.              */
/* ======================================= */

// Directives
using UnityEngine;

public class AnnouncerManager : MonoBehaviour
{
    // *** SERIALIZED VARIABLES *** //
    [Header ("Announcer manager settings")]
    [SerializeField] private Announcer _announcerArcade01; // Arcade announcer 01
    [SerializeField] private Announcer _announcerArcade02; // Arcade announcer 02
    [SerializeField] private Announcer _announcerTeuchter; // Teuchter announcer
    [SerializeField] private float _teuchterChance = 0.05f; // Teuchter change
    [SerializeField] private float _arcade01Chance = 0.7f; // Chance for the first arcade announcer

    // *** VARIABLES *** //
    private static AnnouncerManager _instance; // Instance
    private Announcer _gameAnnouncer; // Game announcer

    /*
     * GAME ANNOUNCER GET METHOD
     * 
     * Method returns the value held by the
     * game announcer variable.
     */
    public Announcer GameAnnouncer
    {
        get
        {
            return _gameAnnouncer;
        }
    }

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is 
     * awoken.
     */
    private void Awake()
    {
        // Check if instance is null
        if(_instance == null)
        {
            // Change instance to this
            _instance = this;

            // Invoke the choose announcer method
            ChooseAnnouncer();

            // Prevent the game object from being destroyed when loading new scenes
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // Destroy the game object
            Destroy(gameObject);
        }
    }

    /*
     * ANNOUNCER MANAGER GET METHOD
     * 
     * Method returns the value held by the
     * instance variable
     */
    public static AnnouncerManager _announcerManager
    {
        get
        {
            // Check if the instance is null
            if (_instance == null)
            {
                // Obtain the announcer manager from the scene
                _instance = FindObjectOfType<AnnouncerManager>();
            }

            return _instance;
        }
    }

    /*
     * CHOOSE ANNOUNCER METHOD
     * 
     * When invoked, the method determines
     * which announcer will be present
     * for the game.
     */
    private void ChooseAnnouncer()
    {
        // Randomly determine a float between 0f and 1f
        float teuchterChance = Random.Range(0f, 1f);

        // Check if the teuchter chance is lower than the teuchter chance setting
        if (teuchterChance < _teuchterChance)
        {
            // Set the teuchter as the announcer
            _gameAnnouncer = Instantiate(_announcerTeuchter);
        }
        else
        {
            // Determine a random arcade chance value
            float arcadeChance = Random.Range(0f, 1f);

            // Check if the standard chance vaalue is less than the arcade chance 
            if (arcadeChance < _arcade01Chance)
            {
                // Set the announcer to arcade 01
                _gameAnnouncer = Instantiate(_announcerArcade01);
            }
            else
            {
                // Set the announcer to arcade 02
                _gameAnnouncer = Instantiate(_announcerArcade02);
            }
        }
    }
}