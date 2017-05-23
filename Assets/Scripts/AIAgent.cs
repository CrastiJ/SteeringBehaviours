using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    public Vector3 force;
    public Vector3 velocity;
    public float maxVelocity = 100f;

    private List<SteeringBehaviour> behaviours;

    void Start()
    {
        behaviours = new List<SteeringBehaviour>(GetComponents<SteeringBehaviour>());
    }

    void Update()
    {
        ComputeForces();
        ApplyVelocity();
    }

    void ComputeForces()
    {
        force = Vector3.zero;
        //Loop through all behaviours attach to a gameObject
        for (int i = 0; i < behaviours.Count; i++)
        {
            SteeringBehaviour behaviour = behaviours[i];
            if (!behaviour.enabled) continue;

            //Add force from behaviour
            force += behaviour.GetForce() * behaviour.weighting;

            //if the force is too big
            if(force.magnitude > maxVelocity)
            {
                //Clamp it to maxVelocity
                force = force.normalized * maxVelocity;
                //break out of loop
                break;
            }
        }
    }

    void ApplyVelocity()
    {
        //Add force to the velocity
        velocity += force * Time.deltaTime;
        //if velocity is too high
        if(velocity.magnitude > maxVelocity)
        {
            velocity = velocity.normalized * maxVelocity;
        }
        //Check if velocity is valid
        if(velocity != Vector3.zero)
        {
            //Apply position & velocity
            transform.position += velocity * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(velocity);
        }
    }
}
