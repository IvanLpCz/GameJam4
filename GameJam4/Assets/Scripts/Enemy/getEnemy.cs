using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enemy
{
    public class getEnemy : MonoBehaviour
    {
        private bool haspassed;
        public bool lastRoom;
        public Transform spwan;
        private int randomAmountOfEnemys;
        public GameObject Enemy, Boss;

        private void OnTriggerEnter(Collider other)
        {
            if (!haspassed && !lastRoom)
            {
                randomAmountOfEnemys = Random.Range(1,3);
                for(int i = 0; i <= randomAmountOfEnemys; i++)
                {
                    Instantiate(Enemy, spwan);
                }
                haspassed = true;
            }
            if (!haspassed && lastRoom)
            {
                Instantiate(Boss, spwan);
                haspassed = true;
            }

        }
    }
}
