using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public void StartButton()
    {
        SceneManager.LoadScene("NyPlayer");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
