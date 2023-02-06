using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class PatrolState : State{

	public float timer;

	public PatrolState(StateAgent owner) : base(owner){

	}

	public override void OnEnter(){

		owner.movement.Resume();

		timer = Random.Range(5, 10);

		owner.navigation.targetNode = owner.navigation.GetNearestNode();

	}

	public override void OnExit(){

	}

	public override void OnUpdate(){

		timer =- Time.deltaTime;

        if (owner.percieved.Length == 0){

            owner.stateMachine.StartState(nameof(IdleState));

        }else{

            // move towards the perceived object position 
            
			owner.movement.MoveTowards(owner.percieved[0].transform.position);

            // create a direction vector toward the perceieved object from the owner
            
			Vector3 direction = owner.percieved[0].transform.position - owner.transform.position;
            
			// get the distance to the perceived object 
            
			float distance = owner.percieved[0].transform.position.magnitude;
            
			// get the angle between the owner forward vector and the direction vector 
            
			float angle = Vector3.Angle(owner.transform.forward, direction);
            
			// if within range and angle, attack 
            
			if (distance < 2.5 && angle < 60){

                owner.stateMachine.StartState(nameof(AttackState));
            
			}

        }

        if (owner.percieved.Length > 0){ 
		
			owner.stateMachine.StartState(nameof(ChaseState));
		
		}

		if(timer <= 0){

			owner.stateMachine.StartState(nameof(WanderState));
		
		}


	}

}
