using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls how many balls are in the 
/// scene at any one time.
/// </summary>
public class BallManager : MonoBehaviour
{
    //Public variables
    public Rigidbody ballObject;
    public int maxNumberOfBalls = 25;

    //Private variables
    List<Rigidbody> ballInstances;

    // Awake is called when the script instance is being loaded
    void Awake() {
        ballInstances = new List<Rigidbody>();
    }

    // Get the current number of balls in the scene
    public int GetCurrentNumberOfBalls() {
        return ballInstances.Count;
    }

    // Create a new ball with the given position and rotation
    public Rigidbody CreateNewBall(Vector3 position, Quaternion rotation) {
        Rigidbody newBallInstance = Instantiate(ballObject, position, rotation) as Rigidbody;
        ballInstances.Add(newBallInstance);
        return newBallInstance;
    }

    // Remove the given ball from the list
    public void RemoveBall(Rigidbody ball) {
        if (ballInstances.Contains(ball))
        {
            ballInstances.Remove(ball);
        }
    }
}
