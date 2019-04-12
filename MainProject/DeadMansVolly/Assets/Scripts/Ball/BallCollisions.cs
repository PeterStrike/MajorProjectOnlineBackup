using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles all possiable outcomes if this 
/// object collides with almost any other object.
/// </summary>
public class BallCollisions : MonoBehaviour
{
    // Public variables
    public int attackDamage = 1;
    public Material neutralState;
    public Material enemyState;
    public Material playerState;

    // Private variables
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool isPlayerInRange;
    bool isEnemyInRange;
    GameObject levelManager;
    BallManager ballManager;
    Rigidbody ballRigidbody;
    int state;
    Renderer render;

    // Awake is called when the script instance is being loaded
    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        ballManager = levelManager.GetComponent<BallManager>();
        ballRigidbody = GetComponent<Rigidbody>();
        render = GetComponentInChildren<Renderer>();
        render.enabled = true;

    }

    // Called when an object with a Collider enters this objects colliders that are marked as triggers
    void OnTriggerEnter(Collider other) {
        if ((other.tag == "Player") && (state != 1))
        {
            isPlayerInRange = true;
        }
        else if ((other.tag == "Enemy") && (state != 2))
        {
            isEnemyInRange = true;
            enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        }
        else if (other.tag == "Despawner")
        {
            DestroyBall();
        }
        else if (other.tag == "Breakable")
        {
            Destroy(other.gameObject);
            DestroyBall();
        }
        else if (other.tag == "Wall")
        {
            ChangeBallState(0);
        }
    }

    // Called when an object with a Collider exits this objects colliders that are marked as triggers
    void OnTriggerExit(Collider other) {
        if (other.gameObject == player)
        {
            isPlayerInRange = false;
        }
        else if (other.tag == "Enemy")
        {
            isEnemyInRange = false;
        }
    }

    // Update is called once per frame
    void Update() {
        if (isPlayerInRange)
        {
            DamagePlayer();
        }
        if (isEnemyInRange)
        {
            DamageEnemy();
        }
    }

    // Pass damage onto the players health
    void DamagePlayer() {
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
            DestroyBall();
        }
    }

    // Pass damage onto the enemy health
    void DamageEnemy() {
        if (enemyHealth.currentHealth > 0)
        {
            enemyHealth.TakeDamage(attackDamage);
            DestroyBall();
        }
    }

    // Remove this ball from the manager, then selfdestruct
    void DestroyBall() {
        ballManager.RemoveBall(ballRigidbody);
        Destroy(this.gameObject);
    }

    // Change the colour of the ball based on state
    public void ChangeBallState(int newState) {
        state = newState;
        if (newState == 1)
        {
            render.sharedMaterial = playerState;
        }
        else if (newState == 2)
        {
            render.sharedMaterial = enemyState;
        }
        else if (newState == 0)
        {
            render.sharedMaterial = neutralState;
        }
    }
    /*
    The functions Awake(), OnTriggerEnter(Collider other), OnTriggerExit(Collider other) and Update() are modified from the function of the same name from the source below.
    The functions DamagePlayer() and DamageEnemy() are modified from the function Attack() from the source below.
 
    Title : Tutorials: Survival Shooter tutorial: Player Health
    Author : Unity
    Date Accessed : 07/04/2019
    Code Version : 1.0
    Availability : https://unity3d.com/learn/tutorials/projects/survival-shooter/player-health?playlist=17144
     */
}
