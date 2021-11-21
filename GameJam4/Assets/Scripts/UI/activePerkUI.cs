using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace menuLogic
{
    public class activePerkUI : MonoBehaviour
    {
        public GameObject UIperkA, UIperkC, UIperkD, UIperkG, UIperkL, UIperkH, UIperkS;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("perkA"))
            {
                UIperkA.SetActive(true);
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("PerkC"))
            {
                UIperkC.SetActive(true);
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("PerkD"))
            {
                UIperkD.SetActive(true);
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("PerkG"))
            {
                UIperkG.SetActive(true);
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("PerkL"))
            {
                UIperkL.SetActive(true);
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("PerkH"))
            {
                UIperkH.SetActive(true);
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("PerkS"))
            {
                UIperkS.SetActive(true);
                Destroy(other.gameObject);
            }
        }
    }
}
