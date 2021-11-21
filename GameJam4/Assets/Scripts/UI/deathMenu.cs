using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace menuLogic
{
    public class deathMenu : MonoBehaviour
    {
        public Button tryAgame, exit;
        private Scene actualScene;

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
}
