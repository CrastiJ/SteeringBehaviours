using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    public Transform target;
    public float stoppingDistance = 0;

    public override Vector3 GetForce()
    {
        //Create a new force to calculate
        Vector3 force = Vector3.zero;

        //Otherwise, calculate a desired velocity
        Vector3 desiredVelocity;
        //Get direction from target to agent
        Vector3 direction = target.position - transform.position;
        direction.y = 0; //Cancel out Y forces
        //Check if the direction is valid
        if(direction.magnitude > stoppingDistance)
        {
            //Calculate the force
            desiredVelocity = direction.normalized * weighting;
            //Apply to force
            force = desiredVelocity - owner.velocity;
        }
        return force;
    }
}
