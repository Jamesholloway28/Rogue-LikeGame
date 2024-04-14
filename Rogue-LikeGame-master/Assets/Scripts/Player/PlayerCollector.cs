using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    PlayerStats player;
    CircleCollider2D playerCollector;
    public float pullSpeed;

    void Update() {
        playerCollector.radius = player.CurrentMagnet;
    }

    void Start() {
        player = FindObjectOfType<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //check if the other game object has the Icollectible interface
        if(col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            //Pulling animation
            //Gets the Rigidbody2D component on the item
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            //Vector2 pointing from the item to the player
            Vector2 forceDirection = (transform.position - col.transform.position).normalized;
            //Applies force to the item in the forceDirection with pullSpeed
            rb.AddForce(forceDirection * pullSpeed);

            //if it does, call the collect method
            collectible.Collect();
        }
    }

}
