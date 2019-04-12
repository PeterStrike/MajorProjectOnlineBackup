using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class spawns a ball infront of the 
/// enemy after X seconds an shoots it towards 
/// the player.
/// </summary>
public class EnemyBallSpawner : MonoBehaviour
{
    //Public variables
    public Transform fireTransform;
    public float ballSpeed;
    
    //Private variables
    GameObject levelManager;
    BallManager ballManager;
    Transform player;
    PlayerHealth playerHealth;
    EnemyFiringRange isTargetinRangeCode;
    bool isPlayerWithinRange;
    int maxNumberOfBalls = 0;
    int currentNumberOfBalls;
    float spawnTimer;

    // Start is called before the first frame update
    void Awake(){
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        ballManager = levelManager.GetComponent<BallManager>();
        maxNumberOfBalls = ballManager.maxNumberOfBalls;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        isTargetinRangeCode = GetComponentInChildren<EnemyFiringRange>();
        spawnTimer = Random.Range(2,4);
        InvokeRepeating("CreateBall", spawnTimer, spawnTimer);
    }

    // Update is called once per frame
    void Update(){
        currentNumberOfBalls = ballManager.GetCurrentNumberOfBalls();
        GetPlayerRange();
    }

    // Uses a collider to get the current range from the player
    void GetPlayerRange(){
        isPlayerWithinRange = isTargetinRangeCode.isShootableInRange;
    }

    //Creates and instance of a ball object and gives it an initial velocity
    void CreateBall(){
        if ((currentNumberOfBalls < maxNumberOfBalls) && (isPlayerWithinRange == true) && (playerHealth.currentHealth > 0))
        {
            Rigidbody newBallInstance = ballManager.CreateNewBall(fireTransform.position, fireTransform.rotation);
            newBallInstance.velocity = ballSpeed * fireTransform.forward;
            BallCollisions ballColliderInstance = newBallInstance.gameObject.GetComponent<BallCollisions>();
            ballColliderInstance.ChangeBallState(2);
        }
    }
    /*
    The functions Start() is modified from the function of the same name from the source below.

    Title : Survival Shooter tutorial: Spawning Enemies
    Author : Unity
    Date Accessed : 07/04/2019
    Code Version : 1.0
    Availability : https://unity3d.com/learn/tutorials/projects/survival-shooter/more-enemies?playlist=17144

    ~~~

    The function CreateBall() is modified from the function Fire() from the source below.
 
    Title : Tutorials: Tanks tutorial: Firing Shells
    Author : Unity
    Date Accessed : 07/04/2019
    Code Version : 1.0
    Availability : https://unity3d.com/learn/tutorials/projects/tanks-tutorial/firing-shells?playlist=20081
     */
}
