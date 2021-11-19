using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game_Core
{
    public class miniMap : MonoBehaviour
    {
        public Renderer tileRend;

        private void OnTriggerEnter(Collider other)
        {
            tileRend.enabled = true;
        }
    }
}
