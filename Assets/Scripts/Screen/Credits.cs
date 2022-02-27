/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 27/02/22                      */
/* LAST MODIFIED - 27/02/22                */
/* ======================================= */
/* Credits                                 */
/* Credits.cs                              */
/* ======================================= */
/* Script controls the credits which are   */
/* displayed on the title screen. Also has */
/* Credit class with various strings for   */
/* holding credit values.                  */
/* ======================================= */

// Directives
using UnityEngine;
using TMPro;

/*
 * CREDIT CLASS
 * 
 * Serialised class with strings for
 * credit values.
 */
[System.Serializable]
public class Credit
{
    public string _creditType; // Credit type string
    public string _credit; // Credit string
    public string _creditAdditional; // Credit additional string
}

public class Credits : MonoBehaviour
{
    // *** SERIALISED ARRAY AND VARIABLES *** //
    [Header ("Credits")]
    [SerializeField] private Credit[] _credits; // Credits

    [Header("Credit settings")]
    [SerializeField] private float _fadeInTime = 0.5f; // Fade in time
    [SerializeField] private float _creditScreenTime = 3f; // Credit screen time
    [SerializeField] private float _fadeOutTime = 0.5f; // Credit fade out time 
    [SerializeField] private float _waitTime = 0.5f; // Wait time 
    [SerializeField] private float _beginWaitTime = 2f; // Begin wait time

    // *** VARIABLES *** //
    private TextMeshProUGUI _creditsLabel; // Credits label
    private TextMeshProUGUI _creditType; // Credit type text
    private TextMeshProUGUI _credit; // Credit text
    private TextMeshProUGUI _creditAdditional; // Credit additional
    private float _beginWaitTimer; // Begin wait timer
    private float _creditTimer; // Credit timer
    private bool _creditsEnabled; // Credits enabled boolean
    private bool _creditsStarted; // Credits started boolean
    private bool _creditLabelDisplayed; // Credits label displayed boolean
    private int _creditIndex; // Credit index

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is 
     * awoken.
     */
    private void Awake()
    {
        // Set the credit timer to 0 seconds
        _creditTimer = 0f;

        // Set the credits started to false
        _creditsStarted = false;

        // Set the credit index to 0
        _creditIndex = 0;

        // Set the credit label displayed to false
        _creditLabelDisplayed = false;

        // Invoke the initialise text fields method
        InitialiseTextFields();

        // Invoke the set credit method
        SetCredit();
    }

    /*
     * FIXED UPDATE METHOD
     * 
     * Method is invoked at regular fixed time intervals.
     * 
     * Method controls when text items are displayed
     * on the screen, provided the credits started boolean
     * is true.
     */
    void FixedUpdate()
    {
        // Check the value of the credits started boolean
        if(!_creditsStarted)
        {
            // Check if the credits are enabled
            if (_creditsEnabled)
            {
                // Increment the wait timer
                _beginWaitTimer += Time.fixedDeltaTime;

                // Check if the begin wait timer is greather than, or equal to, the begin wait time
                if (_beginWaitTimer >= _beginWaitTime)
                {
                    // Set the credits started to true
                    _creditsStarted = true;
                }
            }
        }
        else
        {
            // Increment the credit timer by the fixed delta time value
            _creditTimer += Time.fixedDeltaTime;

            // Check that the credits label displayed is false
            if(!_creditLabelDisplayed)
            {
                // Invoke the fade in credits label
                FadeInCreditsLabel();
            }

            // Check the value of the credit timer and invoke methods at various stages
            if(_creditTimer >= 0 && _creditTimer < _fadeInTime)
            {
                // Invoke the fade in method
                FadeIn();
            }
            else if(_creditTimer >= _fadeInTime && _creditTimer < _fadeInTime + _creditScreenTime)
            {
                // Ensure the credit is fully visible
                CreditVisible();
            }
            else if(_creditTimer >= _fadeInTime + _creditScreenTime && _creditTimer < _fadeInTime + _creditScreenTime + _fadeOutTime)
            {
                // Fade out the current credit
                FadeOut();
            }
            else if (_creditTimer >= _fadeInTime + _creditScreenTime + _fadeOutTime && _creditTimer < _fadeInTime + _creditScreenTime + _fadeOutTime + _waitTime)
            {
                // Ensure the credits text is not visible on screen
                CreditInvisible();
            }
            else if (_creditTimer >= _fadeInTime + _creditScreenTime + _fadeOutTime + _waitTime)
            {
                // Invoke the next credit method
                NextCredit();

                // Set the next credit
                SetCredit();

                // Reset the credit timer
                _creditTimer = 0f;
            }
        }
    }

    /*
     * INITIALISE TEXT FIELDS METHOD
     * 
     * Obtains and initialises the alpha of various
     * text objects.
     */
    private void InitialiseTextFields()
    {
        // Obtain the credits label from the scene
        _creditsLabel = GameObject.Find("CreditsLabel").GetComponent<TextMeshProUGUI>();

        // Obtain the credit type text from the scene
        _creditType = GameObject.Find("CreditType").GetComponent<TextMeshProUGUI>();

        // Obtain the credit text from the scene
        _credit = GameObject.Find("CreditInfo").GetComponent<TextMeshProUGUI>();

        // Obtain the additional credit information
        _creditAdditional = GameObject.Find("CreditLink").GetComponent<TextMeshProUGUI>();

        // Set the alpha for the credits label text as 0
        _creditsLabel.alpha = 0f;

        // Set the alpha for the credit type text as 0
        _creditType.alpha = 0f;

        // Set the alpha for the credits text as 0
        _credit.alpha = 0f;

        // Set the alpha for the credits additional text as 0
        _creditAdditional.alpha = 0f;
    }

    /*
     * SET CREDIT METHOD
     * 
     * Method is used to set the text items
     * to the values from the credits array.
     */
    private void SetCredit()
    {
        // Set the credit type text from the credits array
        _creditType.text = _credits[_creditIndex]._creditType;

        // Set the credit text from the credits array
        _credit.text = _credits[_creditIndex]._credit;

        // Set the credit additiona text from the credits array
        _creditAdditional.text = _credits[_creditIndex]._creditAdditional;
    }

    /*
     * NEXT CREDIT METHOD
     * 
     * Method sets the next credit by incrementing
     * the credit index by one. Method also 
     * checks that the credit does not exceed the 
     * length of the credits array.
     */
    private void NextCredit()
    {
        // Increment the credit index
        _creditIndex++;

        // Check if the credit index is greater than, or equal to, the length of the credits array
        if(_creditIndex >= _credits.Length)
        {
            // Set the credit index to 0
            _creditIndex = 0;
        }
    }

    /*
     * FADE IN CREDITS LABEL METHOD
     * 
     * Method fades in the credits label
     * and sets the label displayed to true
     * when the alpha value is equal to, or
     * greater than, 1.
     */
    private void FadeInCreditsLabel()
    {
        // Determine the alpha value
        float alpha = _creditTimer / _fadeInTime;

        // Check if the alpha value is greater than, or equal to, 1
        if(alpha >= 1f)
        {
            // Set the alpha value to 1
            _creditsLabel.alpha = 1f;

            // Set the credut label displayed to true
            _creditLabelDisplayed = true;
        }
        else
        {
            // Set the alpha of the credits label to the alpha value determined
            _creditsLabel.alpha = alpha;
        }
    }

    /*
     * FADE IN METHOD
     * 
     * Method is used to fade in the 
     * credit text.
     */
    private void FadeIn()
    {
        // Determine an alpha value
        float alpha = _creditTimer / _fadeInTime;

        // Set the alpha value for the credit type text
        _creditType.alpha = alpha;

        // Set the alpha value for the credit text
        _credit.alpha = alpha;

        // Set the alpha value for the credit additional text
        _creditAdditional.alpha = alpha;
    }

    /*
     * FADE OUT METHOD
     * 
     * Method is used to fade out the
     * credit text
     */
    private void FadeOut()
    {
        // Determine an alpha value
        float alpha = 1 - ((_creditTimer - _fadeInTime - _creditScreenTime) / _fadeOutTime);

        // Set the alpha value for the credit type text
        _creditType.alpha = alpha;

        // Set the alpha value for the credit text
        _credit.alpha = alpha;

        // Set the alpha value for the credit additional text
        _creditAdditional.alpha = alpha;
    }

    /*
     * CREDIT VISIBLE METHOD
     * 
     * Method is used to ensure the credit
     * text is visible to the screen.
     */
    private void CreditVisible()
    {
        // Set credit type text alpha to 1
        _creditType.alpha = 1f;

        // Set credit text alpha to 1
        _credit.alpha = 1f;

        // Set credit additional text alpha to 1
        _creditAdditional.alpha = 1f;
    }

    /*
     * CREDIT INVISIBLE METHOD
     * 
     * Method is used to ensure the credit
     * text is invisible from the screen.
     */
    private void CreditInvisible()
    {
        // Set credit type text alpha to 0
        _creditType.alpha = 0f;

        // Set credit text alpha to 0
        _credit.alpha = 0f;

        // Set credit additional text alpha to 0
        _creditAdditional.alpha = 0f;
    }

    /*
     * ENABLE CREDITS METHOD
     * 
     * When invoked, the method sets the 
     * credits enabled boolean to true.
     */
    public void EnableCredits()
    {
        _creditsEnabled = true;
    }
}