using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    float CurrentHealth { get; set; }
    float MaxHealth { get; set; }

    public Slider healthbar;

    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = 20f;

        CurrentHealth = MaxHealth;

        healthbar.value = CalculateHealth();
        
    }

    public void DealDamage(float damageValue)
    {
        CurrentHealth -= damageValue;
        healthbar.value = CalculateHealth();

        if (CurrentHealth <= 0)
            Die();
    }

    void Die()
    {
        CurrentHealth = 0;
        Debug.Log("Dead");
    }

    float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    public void showHealth()
    {
        Debug.Log(CurrentHealth); 
    }

}
