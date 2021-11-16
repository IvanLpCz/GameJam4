using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pools
{
    public class pooltrigger : MonoBehaviour
    {
        public Transform bulletPosition;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                fire();
            }
        }

        private void fire()
        {
            GameObject bullet = pooling.instance.GetPooledObject();

            if(bullet != null)
            {
                bullet.transform.position = bulletPosition.position;
                //Quaternion NewRotation = new Quaternion(bullet.transform.rotation.x, bulletPosition.transform.rotation.y, bullet.transform.rotation.z, bulletPosition.transform.rotation.w);
                //bullet.transform.rotation = NewRotation;
                bullet.SetActive(true);
            }
        }
    }
}
