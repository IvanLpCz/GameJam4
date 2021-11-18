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
            SceneManager.LoadScene("Level1");
        }
        public void CreditsScene()
        {
            SceneManager.LoadScene("CreditosIniciales");
        }
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
