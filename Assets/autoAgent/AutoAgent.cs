using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAgent : Agent{

	public Perception flockPerception;

	public ObstaclePerception obstacleAvoidance;

	public AutonomousAgentData data;

    public float wanderAngle { get; set; } = 0;

    void Update(){

        var gameObjects = perception.GetGameObjects();
        
        foreach (var gameObject in gameObjects){

            Debug.DrawLine(transform.position, gameObject.transform.position);

        }

        if (gameObjects.Length > 0){//< game objects array contains at least one game object){

            movement.ApplyForce(steering.Seek(this, gameObjects[0]) * data.seekWeight);

            movement.ApplyForce(steering.Flee(this, gameObjects[0]) * data.fleeWeight);
        
        }

        gameObjects = flockPerception.GetGameObjects();

        if(gameObjects.Length > 0){

            Debug.DrawLine(transform.position, gameObject.transform.position);

            movement.ApplyForce(steering.Cohesion(this, gameObjects) * data.cohesionWeight);
            
            movement.ApplyForce(steering.Seperation(this, gameObjects, data.separationRadius) * data.separationWeight);

            movement.ApplyForce(steering.Allignment(this, gameObjects) * data.alignmentWeight);
        
        }

		// obstacle avoidance

		if (obstacleAvoidance.IsObstacleInFront()){

			Vector3 direction = obstacleAvoidance.GetOpenDirection();
			
            movement.ApplyForce(steering.CalculateSteering(this, direction) * data.obstacleWeight);

		}

		if (movement.acceleration.sqrMagnitude <= movement.maxForce * 0.1f){

            movement.ApplyForce(steering.Wander(this));
        
        }

        Vector3 position = transform.position;

        position = utilities.Wrap(position, new Vector3(-20, -20, -20), new Vector3(20, 20, 20));

        position.y = 0;

        transform.position = position;

    }

}
