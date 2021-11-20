using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripteable_Bullet
{
    [CreateAssetMenu(fileName = "New Bullet", menuName = "Bullet Data")]
    public class basicBullet : ScriptableObject
    {
        [SerializeField] public float bulletForce = 20f;
        [SerializeField] public float bulletDamage = 10f;


    }
}
