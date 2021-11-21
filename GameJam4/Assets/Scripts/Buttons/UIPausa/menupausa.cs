using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace menuLogic
{
    public class menupausa : MonoBehaviour
    {
        public Button contune, restart, exit;
        public GameObject inGameHUD, pauseMenu;

        public void ContinueButton()
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            inGameHUD.SetActive(true);
        }
        public void ExitButton()
        {
            SceneManager.LoadScene("MainMenu");
            Time.timeScale = 1;
        }
    }
}
