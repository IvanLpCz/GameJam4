using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace enemy
{
    public class popWin : MonoBehaviour
    {
        public GameObject winCanvas;
        public Health hp;
        private bool winPop;

        private void Update()
        {
            if (hp.isDead && !winPop)
            {
                Instantiate(winCanvas);
                winPop = true;
            }
        }
    }
}
