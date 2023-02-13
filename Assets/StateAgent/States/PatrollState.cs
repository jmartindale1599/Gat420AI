using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class PatrolState : State{

	public PatrolState(StateAgent owner) : base(owner){

	}

	public override void OnEnter(){

		owner.movement.Resume();

		owner.navigation.targetNode = owner.navigation.GetNearestNode();

		owner.timer.value = Random.Range(5, 10);

	}

	public override void OnExit(){

	}

	public override void OnUpdate(){


        if (owner.percieved.Length > 0){ 
		
			owner.stateMachine.StartState(nameof(ChaseState));
		
		}

		//owner.timer -= Time.deltaTime;

		if(owner.timer <= 0){
			owner.stateMachine.StartState(nameof(WanderState));
		
		}


	}

}
