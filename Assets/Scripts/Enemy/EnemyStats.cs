using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    //current stats

    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;
    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    // Update is called once per frame
    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if(currentHealth <= 0)
        {
            
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        //reference the script from the collided collider and deal damage using TakeDamage()
        if(col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage); //make sure to use current damage instead of weaponData.damage in case any damage multipliers are in effect
        }
        
    }
}
