using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekPosition : SteeringBehaviour
{
    public Vector3 target;
    public float stoppingDistance = 0;
    
    public override Vector3 GetForce()
    {
        //Create a new force to calculate
        Vector3 force = Vector3.zero;
        //if there is no target return force
        if (target == Vector3.zero) return force;
        //Otherwise, calculate a desired velocity
        Vector3 desiredVelocity;
        //Get direction from target to agent
        Vector3 direction = target - transform.position;
        direction.y = 0; //Cancel out Y forces
        //Check if the direction is valid
        if (direction.magnitude > stoppingDistance)
        {
            //Calculate the force
            desiredVelocity = direction.normalized * weighting;
            //Apply to force
            force = desiredVelocity - owner.velocity;
        }
        return force;
    }
}