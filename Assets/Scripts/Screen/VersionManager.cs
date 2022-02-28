/* ======================================= */
/*          SUPER CAVE RUNNER DX           */
/*                 V 1.0                   */
/* ======================================= */
/* AUTHOR - Graeme White - 2022            */
/* CREATED - 28/02/22                      */
/* LAST MODIFIED - 28/02/22                */
/* ======================================= */
/* VERSION MANAGER                         */
/* VersionManager.cs                       */
/* ======================================= */
/* Script manages the version text         */
/* component on the main screen.           */
/* ======================================= */

// Directives
using UnityEngine;
using TMPro;

public class VersionManager : MonoBehaviour
{
    private TextMeshProUGUI _versionText; // Version Text

    /*
     * AWAKE METHOD
     * 
     * Method is invoked when the script is awoken.
     */
    private void Awake()
    {
        // Obtain the version text from the scene
        _versionText = GetComponent<TextMeshProUGUI>();

        // Set the version text
        _versionText.text = Application.version;
    }
}