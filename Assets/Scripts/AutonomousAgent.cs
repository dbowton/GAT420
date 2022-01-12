using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Menenemy
{
    [SerializeField] Perception perception;
    [SerializeField] Steering steering;

    public float maxSpeed;
    public float maxForce;

    public Vector3 velocity { get; set; } = Vector3.zero;

    void Update()
    {
        Vector3 acc = Vector3.zero;

        GameObject[] gameObjects = perception.GetGameObjects();

        if (gameObjects.Length == 0)
        {
            acc += steering.Wander(this);
        }
        if (gameObjects.Length != 0)
        {
            Debug.DrawLine(transform.position, gameObjects[0].transform.position);

            acc += steering.Seek(this, gameObjects[0]);
        }

        velocity += acc * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;

        if(velocity.sqrMagnitude > 0.1f) transform.rotation = Quaternion.LookRotation(velocity);

        transform.position = Utilities.Wrap(transform.position, new Vector3(-10, -10, -10), new Vector3(10, 10, 10));
    }
}
