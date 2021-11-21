using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace menuLogic
{
    public class activePerkUI : MonoBehaviour
    {
        public GameObject UIperk;

        private void OnTriggerEnter(Collider other)
        {
            UIperk.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
