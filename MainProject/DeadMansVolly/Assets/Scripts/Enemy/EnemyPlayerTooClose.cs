using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to identify the enemys 
/// proximity to the player.
/// </summary>
public class EnemyPlayerTooClose : MonoBehaviour
{
    //Public variables
    public bool isPlayerTooClose;

    // Start is called before the first frame update
    void Start() {
        isPlayerTooClose = false;
    }

    //Called when an object with a Collider enters this objects colliders that are marked as triggers
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "PlayerMovement")
        {
            isPlayerTooClose = true;
        }
    }

    //Called when an object with a Collider exits this objects colliders that are marked as triggers
    private void OnTriggerExit(Collider other) {
        if (other.tag == "PlayerMovement")
        {
            isPlayerTooClose = false;
        }
    }
}
