using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon Data")]
public class weaponScript : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private string description;
    [SerializeField] private int damage;
    [SerializeField] private GameObject skill;
    [SerializeField] private float fireRate;


    public string WeaponNane { get { return weaponName; } }
    public string Description { get { return description; } }
    public int Damage { get { return damage; } }
    public GameObject Skill { get {return skill; } }
    public float FireRate { get { return fireRate; } }

}
