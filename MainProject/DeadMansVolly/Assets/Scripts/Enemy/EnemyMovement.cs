using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This class handled the enemys movment and rotation
/// based on the position and proximity to the player.
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    //Private variables
    EnemyPlayerInRange isPlayerinRangeCode;
    EnemyPlayerTooClose isPlayertooCloseCode;
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    Rigidbody enemyRigidBody;
    int leftOrRight = 50;
    bool isPlayerWithinRange;
    bool isPlayerTooClose;
    NavMeshAgent nav;
    Vector3 backwards;
    Vector3 sideways;

    // Start is called before the first frame update
    void Awake(){
        isPlayerinRangeCode = GetComponentInChildren<EnemyPlayerInRange>();
        isPlayertooCloseCode = GetComponentInChildren<EnemyPlayerTooClose>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyRigidBody = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update(){
        GetPlayerRange();
        Rotation();
        Move();
    }

    // Uses two different colliders to get the current range from the player
    void GetPlayerRange() {
        isPlayerWithinRange = isPlayerinRangeCode.isPlayerInRange;
        isPlayerTooClose = isPlayertooCloseCode.isPlayerTooClose;
    }

    // Point the enemy to face where the player is
    void Rotation() {
        Vector3 enemyToPlayer = player.position - transform.position;
        enemyToPlayer.y = 0f;
        enemyToPlayer.Normalize();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(enemyToPlayer), 12 * Time.deltaTime);
    }

    // Move based on the distance from the player
    void Move() {
        if (playerHealth.currentHealth > 0 && enemyHealth.currentHealth > 0)
        {
            if (isPlayerTooClose == true)
            {
                //move away from the player
                backwards = transform.position - transform.forward * 6;
                nav.SetDestination(backwards);
            }
            else if (isPlayerWithinRange == true)
            {
                bool direction = IsLeftOrRight();
                //move to the side
                if (direction == false)
                {
                    //move right
                    sideways = gameObject.transform.position + gameObject.transform.right * 6;
                }
                else if (direction == true)
                {
                    //move left
                    sideways = gameObject.transform.position - gameObject.transform.right * 6;
                }
                nav.SetDestination(sideways);
            }
            else
            {
                // move towards the player
                nav.SetDestination(player.position);
            }
        }
        else
        {
            // Stop enemy from moving
            nav.enabled = false;
        }
    }

    //Randomly choose wether to move left or right
    bool IsLeftOrRight() {
        int directionNumber = Random.Range(1, 100);
        if (directionNumber > leftOrRight)
        { 
            //left
            leftOrRight -= 1; // 15/04/2019: I just relised that this function increases the likly hood that the enemy will move in one direction rarther than decreaseing it. I currently don't have time to fix this as I am currently trying to write the report. That and the current behavior might be more desirable. who knows?
            return true;
        }
        else if (directionNumber < leftOrRight)
        {
            //right
            leftOrRight += 1;
            return false;
        }
        else
        {
            //if the random number is the same as the leftOrRight number
            bool wtf = (Random.Range(0, 2) == 0);
            return wtf;
        }
    }
    /*
    The function Awake() is modified from a function of the same name from the source below.
    The function Move() is loosly based on the function Update() from the source below.

    Title : Tutorials: Survival Shooter tutorial: Creating Enemy #1
    Author : Unity
    Date Accessed : 07/04/2019
    Code Version : 1.0
    Availability : https://unity3d.com/learn/tutorials/projects/survival-shooter/enemy-one?playlist=17144

    ~~~

    The function Rotation() is modified from the function Turning() from the source below.

    Title : Tutorials: Survival Shooter tutorial: Player Character
    Author : Unity
    Date Accessed : 07/04/2019
    Code Version : 1.0
    Availability : https://unity3d.com/learn/tutorials/projects/survival-shooter/player-character?playlist=17144
     */
}
