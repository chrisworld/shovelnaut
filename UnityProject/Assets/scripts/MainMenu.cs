using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string GameSceneName = "TestScene";
    bool displayHelp = false;
    public GameObject helpPanel;

    public void LoadScene()
    {
        Debug.Log("Loading Scene " + GameSceneName);
        SceneManager.LoadScene(GameSceneName, LoadSceneMode.Single);
    }

    public void DisplayHelpPanel ()
    {
        displayHelp = !displayHelp;

        helpPanel?.SetActive(displayHelp);

    }

}
