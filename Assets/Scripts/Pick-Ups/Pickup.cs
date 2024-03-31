using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col) {

        //If it gets too close to the player, destroy it
        if (col.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
