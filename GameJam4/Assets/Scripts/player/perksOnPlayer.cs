using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class perksOnPlayer : MonoBehaviour
    {
        public bool perkActivated;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("perkA"))
            {
                perkActivated = true;
            }
        }
    }
}
