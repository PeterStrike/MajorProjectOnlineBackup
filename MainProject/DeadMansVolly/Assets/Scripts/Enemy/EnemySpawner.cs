using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class spawns an enemy after X seconds or when 
/// the player pressed the left shift.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    //Public variables
    public Transform fireTransform;
    public float spawnTimer = 3f;

    //Private variables
    GameObject levelManager;
    EnemyManager enemyManager;
    int maxNumberOfEnemys = 0;
    int currentNumberOfEnemys;

    // Start is called before the first frame update
    void Start() {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        enemyManager = levelManager.GetComponent<EnemyManager>();
        maxNumberOfEnemys = enemyManager.maxNumberOfEnemys;
        //InvokeRepeating("CreateEnemy", spawnTimer, spawnTimer);       //Disabled so that the manager can handle the spawning of enemys
    }

    // Update is called once per frame
    void Update() {
        currentNumberOfEnemys = enemyManager.GetCurrentNumberOfEnemys();
        if (Input.GetButtonUp("Fire3"))
        {
            CreateEnemy();
        }
    }

    //Creates and instance of a enemy object
    public void CreateEnemy() {
        if (currentNumberOfEnemys < maxNumberOfEnemys)
        {
            Rigidbody newBallInstance = enemyManager.CreateNewEnemyType1(fireTransform.position, fireTransform.rotation);
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
