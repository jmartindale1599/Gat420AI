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

        if (gameObjects != null){//< game objects array contains at least one game object){

            Vector3 direction = (gameObjects[0].transform.position - transform.position).normalized;

            movement.ApplyForce(direction * 2);
        
        }

        transform.position = utilities.Wrap(transform.position, new Vector3(-10, -10, -10), new Vector3(10, 10, 10));

    }

}
