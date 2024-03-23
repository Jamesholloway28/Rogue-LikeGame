using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        //check if the other game object has the Icollectible interface
        if(col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            //if it does, call the collect method
            collectible.Collect();
        }
    }

}
