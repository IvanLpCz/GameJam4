using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapons
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Bullets/Weapon Data")]
    public class weaponScript : ScriptableObject
    {
        [SerializeField] public string weaponName;
        [SerializeField] public int damage;
        [SerializeField] public float fireRate;

    }
}
