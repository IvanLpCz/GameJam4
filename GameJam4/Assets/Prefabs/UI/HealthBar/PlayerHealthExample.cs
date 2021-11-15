using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

public class PlayerHealthExample : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space)) //para usar como ejemplo de perder vida
        {
            TakeDamage(20);
        }

    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
