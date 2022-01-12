using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    [SerializeField] float wanderDistance = 1;
    [SerializeField] float wanderRadius = 3;
    [SerializeField] float wanderDisplacement = 5;

    float wanderAngle = 0;

    public Vector3 Seek(AutonomousAgent agent, GameObject target)
    {
        Vector3 force = CalcSteering(agent, target.transform.position - agent.transform.position);

        return force;
    }

    public Vector3 Flee(AutonomousAgent agent, GameObject target)
    {
        Vector3 force = CalcSteering(agent, agent.transform.position - target.transform.position);

        return force;
    }

    public Vector3 Wander(AutonomousAgent agent)
    {
        wanderAngle = wanderAngle + Random.Range(-wanderDisplacement, wanderDisplacement);
        Quaternion rotation = Quaternion.AngleAxis(wanderAngle, Vector3.up);

        Vector3 point = rotation * (Vector3.forward * wanderRadius);
        Vector3 forward = agent.transform.forward * wanderDistance;

        Vector3 force = CalcSteering(agent, forward + point);

        return force;
    }

    Vector3 CalcSteering(AutonomousAgent agent, Vector3 vector)
    {
        Vector3 dir = vector.normalized;
        Vector3 des = dir * agent.maxSpeed;
        Vector3 steer = des - agent.velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, agent.maxForce);

        return force;
    }
}
