using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game_Core
{
    public class roomMeshManager : MonoBehaviour
    {
        public Renderer roomRend;
        public GameObject lengueta;
        public bool isLeaving;
        private GameObject miniMapCam;
        public GameObject minimapPos;
        private void Start()
        {
            miniMapCam = GameObject.Find("minimapCamera");
        }
        private void OnTriggerEnter(Collider other)
        {
            if (isLeaving)
            {
                roomRend.enabled = false;
                lengueta.SetActive(false);
            }
            if (!isLeaving)
            {
                roomRend.enabled = true;
                lengueta.SetActive(true);
                miniMapCam.transform.position = minimapPos.transform.position;

            }
        }
    }
}
