using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace menuLogic
{
    public class buttonsLogic : MonoBehaviour
    {
        public Button start, creddits, exit;

        public void StartGame()
        {
            SceneManager.LoadScene("Level0");
        }
        public void CreditsScene()
        {
            SceneManager.LoadScene("Creditos");
        }
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
