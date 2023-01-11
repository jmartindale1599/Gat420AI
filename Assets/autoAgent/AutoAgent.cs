using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAgent : Agent
{
    void Update(){

        var gameObjects = perception.GetGameObjects();
        
        foreach (var gameObject in gameObjects){

            Debug.DrawLine(transform.position, gameObject.transform.position);

        }

        if (gameObjects.Length > 0){//< game objects array contains at least one game object){

            movement.ApplyForce(steering.Seek(this, gameObjects[0]) * 0);

            movement.ApplyForce(steering.Flee(this, gameObjects[0]) * 1);
        
        }

        transform.position = utilities.Wrap(transform.position, new Vector3(-10, -10, -10), new Vector3(10, 10, 10));

    }

}
