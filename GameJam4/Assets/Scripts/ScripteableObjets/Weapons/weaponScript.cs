using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon Data")]
public class weaponScript : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private int damage;
    [SerializeField] private GameObject skill;
    [SerializeField] private float fireRate;

}
