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



        private void Update()
        {
            if (hp.isDead)
            {
                winCanvas.SetActive(true);
            }
        }
    }
}
