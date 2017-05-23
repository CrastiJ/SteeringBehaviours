using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleePosition : SteeringBehaviour
{
    public Vector3 target;
    public float safeDistance = 10f;

    public override Vector3 GetForce()
    {
        //Create a new force to calculate
        Vector3 force = Vector3.zero;
        //if there is no target return force
        if (target == Vector3.zero) return force;
        //Otherwise, calculate a desired velocity
        Vector3 desiredVelocity;
        //Get direction from target to agent
        Vector3 direction = transform.position - target;
        direction.y = 0; //Cancel out Y forces
        //Check if the direction is valid
        if (Vector3.Distance(transform.position, target) < safeDistance)
        {
            //Calculate the force
            desiredVelocity = direction.normalized * weighting;
            //Apply to force
            force = desiredVelocity - owner.velocity;
        }
        return force;
    }
}