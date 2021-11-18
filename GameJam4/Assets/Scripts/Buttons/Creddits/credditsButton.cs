using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace menuLogic
{
    public class credditsButton : MonoBehaviour
    {
        public Button backToMenu;
        public void toMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
