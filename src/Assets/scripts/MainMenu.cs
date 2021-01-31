using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string GameSceneName = "TestScene";
    bool displayHelp = false;
    public GameObject helpPanel;

    bool displayCredits = false;
    public GameObject creditsPanel;

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (displayHelp)
            {
                DisplayHelpPanel();
            } else if (displayCredits)
            {
                DisplayCreditsPanel();
            }
        }
    }


    public void LoadScene()
    {
        Debug.Log("Loading Scene " + GameSceneName);
        SceneManager.LoadScene(GameSceneName, LoadSceneMode.Single);
    }

    public void DisplayHelpPanel()
    {
        displayHelp = !displayHelp;

        helpPanel?.SetActive(displayHelp);

    }

    public void DisplayCreditsPanel()
    {
        displayCredits = !displayCredits;

        creditsPanel?.SetActive(displayCredits);

    }

    public void QuitGame()
    {
#if UNITY_STANDALONE
        //Quit the application
        Application.Quit();
#endif

        //If we are running in the editor
#if UNITY_EDITOR
        //Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
