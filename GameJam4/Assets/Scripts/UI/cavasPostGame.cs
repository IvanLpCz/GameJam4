using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cavasPostGame : MonoBehaviour
{
    public Button tryAgame, salir, exit;
    private Scene actualScene;
    public void Start()
    {
        actualScene = SceneManager.GetActiveScene();
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(actualScene.name);
        Time.timeScale = 1;
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}
