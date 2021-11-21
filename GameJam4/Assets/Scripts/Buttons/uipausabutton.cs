using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace menuLogic
{
    public class uipausabutton : MonoBehaviour
    {
        public GameObject pauseMenu;
        public GameObject uiMenu;
        public void PauseMenuButton()
        {
            Time.timeScale = 0;
            uiMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }
}
