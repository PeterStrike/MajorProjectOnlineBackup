using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to apply a velocity to all objects 
/// identified as 'ball' in the 'bat swing area'.
/// This is done when the player clicks the left mouse button.
/// </summary>
public class BatSwing : MonoBehaviour
{
    // Public variables
    public float newballSpeed;
    public Transform BatArea;

    // Private variables
    Animator animator;
    List<Collider> ballColliders = new List<Collider>();

    // Start is called before the first frame update
    void Start() {
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        ApplyNewVelocity();
    }

    // Called when an object with a Collider enters this objects colliders that are marked as triggers
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Ball" && !ballColliders.Contains(other))
        {
            ballColliders.Add(other);
        }
    }

    // Called when an object with a Collider exits this objects colliders that are marked as triggers
    void OnTriggerExit(Collider other) {
        RemoveBall(other);
    }

    // Remove this ball from the list
    public void RemoveBall(Collider ball) {
        if (ballColliders.Contains(ball))
        {
            ballColliders.Remove(ball);
        }
    }

    // Clears the list of balls
    public void ClearBalls() {
        ballColliders.Clear();
    }

    // Calculate and apply a new velocity to all balls in the bat swing area
    private void ApplyNewVelocity() {
        if (Input.GetButtonDown("Fire1") && !animator.GetBool("IsGuard"))
        {
            foreach (Collider o in ballColliders)
            {
                Rigidbody ballInstance = o.gameObject.GetComponent<Rigidbody>();
                ballInstance.velocity = newballSpeed * BatArea.forward;
                BallCollisions ballColliderInstance = o.gameObject.GetComponent<BallCollisions>();
                ballColliderInstance.ChangeBallState(1);
            }
        }
    }
    /*
    The function ApplyNewVelocity() is loosly based on the function Fire() from the source below.
 
    Title : Tutorials: Tanks tutorial: Firing Shells
    Author : Unity
    Date Accessed : 07/04/2019
    Code Version : 1.0
    Availability : https://unity3d.com/learn/tutorials/projects/tanks-tutorial/firing-shells?playlist=20081
     */
}
