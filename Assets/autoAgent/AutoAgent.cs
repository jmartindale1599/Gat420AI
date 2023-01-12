using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAgent : Agent{

    public Perception flockPerception;

    [Range(0, 3)] public float fleeWeight;

    [Range(0, 3)] public float seekWeight;

    [Range(0, 3)] public float cohesionWeight;
    
    [Range(0, 3)] public float seperationWeight;
    
    [Range(0, 3)] public float alignmentWeight;

    [Range(0, 10)] public float seperationRadius;

    public float wanderDistance = 1;

    public float wanderRadius = 3;
    
    public float wanderDisplacement = 5;

    public float wanderAngle { get; set; } = 0;

    void Update(){

        var gameObjects = perception.GetGameObjects();
        
        foreach (var gameObject in gameObjects){

            Debug.DrawLine(transform.position, gameObject.transform.position);

        }

        if (gameObjects.Length > 0){//< game objects array contains at least one game object){

            movement.ApplyForce(steering.Seek(this, gameObjects[0]) * seekWeight);

            movement.ApplyForce(steering.Flee(this, gameObjects[0]) * fleeWeight);
        
        }

        gameObjects = flockPerception.GetGameObjects();

        if(gameObjects.Length > 0){

            Debug.DrawLine(transform.position, gameObject.transform.position);

            movement.ApplyForce(steering.Cohesion(this, gameObjects) * cohesionWeight);
            
            movement.ApplyForce(steering.Seperation(this, gameObjects, seperationRadius) * seperationWeight);

            movement.ApplyForce(steering.Allignment(this, gameObjects) * alignmentWeight);
        
        }

        if (movement.acceleration.sqrMagnitude <= movement.maxForce * 0.1f){

            movement.ApplyForce(steering.Wander(this));
        
        }

        transform.position = utilities.Wrap(transform.position, new Vector3(-10, -10, -10), new Vector3(10, 10, 10));

    }

}
