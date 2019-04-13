using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private DuckManager duckmg;
    float CurrentHealth { get; set; }
    float MaxHealth { get; set; }

    public Slider healthbar;
    GameOver gameOver;

    // Start is called before the first frame update
    void Start()
    {
        duckmg = GameObject.FindGameObjectWithTag("DuckManager").GetComponent<DuckManager>();
        MaxHealth = 200f;

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
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (PlayerPrefs.GetInt("HighScore") < duckmg.score) {
                PlayerPrefs.SetInt("HighScore", duckmg.score);
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", duckmg.score);
        }
        gameOver = FindObjectOfType<GameOver>();
        Debug.Log(duckmg.score);
        CurrentHealth = 0;
        gameOver.EndGame();
    }

    float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    public void showHealth()
    {
        Debug.Log(CurrentHealth); 
    }

    public void AddHealth(float bonus)
    {
      CurrentHealth += bonus;
      healthbar.value = CalculateHealth();
    }

}
