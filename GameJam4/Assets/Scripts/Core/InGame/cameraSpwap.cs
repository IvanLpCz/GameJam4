using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game_Core
{
    public class cameraSpwap : MonoBehaviour
    {
        private GameObject Camera;
        public GameObject cameraSpot;

        private void Start()
        {
            Camera = GameObject.Find("Main Camera");
        }
        private void OnTriggerEnter(Collider other)
        {
            Camera.transform.position = cameraSpot.transform.position;
        }
    }
}
