using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace enemy
{
    public class dropLoot : MonoBehaviour
    {
        public Health hp;
        public GameObject[] Loot;
        private bool alreadyDrop = false;
        private void Update()
        {
            GiveRandomLoot();
        }
        public void GiveRandomLoot()
        {
            int rand = Random.Range(0, Loot.Length);

            if (hp.isDead && !alreadyDrop)
            {
                GameObject GivenLoot = Instantiate(Loot[rand], transform.position, transform.rotation);
                alreadyDrop = true;
            }

        }
    }
}
