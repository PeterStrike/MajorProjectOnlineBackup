using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to identify the enemys 
/// proximity to the player.
/// </summary>
public class EnemyFiringRange : MonoBehaviour
{
    //Public variables
    public bool isShootableInRange;

    // Start is called before the first frame update
    void Awake()
    {
        isShootableInRange = false;
    }

    //Called when an object with a Collider enters this objects colliders that are marked as triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerMovement")
        {
            isShootableInRange = true;
        }
    }

    //Called when an object with a Collider exits this objects colliders that are marked as triggers
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerMovement")
        {
            isShootableInRange = false;
        }
    }
}
