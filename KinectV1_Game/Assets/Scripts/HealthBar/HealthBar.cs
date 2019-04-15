using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //creates duck manager to use from hierarchy 
    private DuckManager duckmg;
    //creates get and set methods for health and max health
    float CurrentHealth { get; set; }
    float MaxHealth { get; set; }
    //health bar as slider 
    public Slider healthbar;
    //imports GameOver script 
    GameOver gameOver;

    // Start is called before the first frame update
    void Start()
    {
        //sets the duckmanager to the script DuckManager
        duckmg = GameObject.FindGameObjectWithTag("DuckManager").GetComponent<DuckManager>();
        //sets max health 
        MaxHealth = 200f;
        //sets current health equal to max health
        CurrentHealth = MaxHealth;
        //and sets the health bar on screen equal to calcalute health method where it calculates the current health
        healthbar.value = CalculateHealth();
        
    }

    //method to deal damage to the health and if health is less than 0 make the player die
    public void DealDamage(float damageValue)
    {
        CurrentHealth -= damageValue;
        healthbar.value = CalculateHealth();

        if (CurrentHealth <= 0)
            Die();
    }
    //kills the player if the health is 0 or less and adds the high score to the game 
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
    //calculates the current health and returns it 
    float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    // show health for testing purposes throughout the game
    public void showHealth()
    {
        Debug.Log(CurrentHealth); 
    }
    //adds health from the pick ups
    public void AddHealth(float bonus)
    {
      CurrentHealth += bonus;
      healthbar.value = CalculateHealth();
    }

}
