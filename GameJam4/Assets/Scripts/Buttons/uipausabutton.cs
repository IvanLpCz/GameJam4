using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace menuLogic
{
    public class uipausabutton : MonoBehaviour
    {
        public GameObject pauseMenu;
        public void PauseMenuButton()
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }
}
