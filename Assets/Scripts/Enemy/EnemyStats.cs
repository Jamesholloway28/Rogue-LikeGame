using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    //current stats
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;
    public float despawnDistance = 20f;
    Transform player;

    void Start() {
        player = FindObjectOfType<PlayerStats>().transform;
    }

    void Update() {
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance) {
            ReturnEnemy();
        }
    }

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

    private void OnDestroy() {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        es.OnEnemyKilled();
    }

    void ReturnEnemy() {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }
}
