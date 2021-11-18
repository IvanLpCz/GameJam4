using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game_Core
{
    public class roomMeshManager : MonoBehaviour
    {
        public Renderer roomRend;
        public bool isLeaving;

        private void OnTriggerEnter(Collider other)
        {
            if (isLeaving)
            {
                roomRend.enabled = false;
            }
            if (!isLeaving)
            {
                roomRend.enabled = true;
            }
        }
    }
}
