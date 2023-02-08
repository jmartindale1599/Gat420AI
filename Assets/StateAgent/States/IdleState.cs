using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State{

	private float timer;

	public IdleState(StateAgent owner) : base(owner){

	}

	public override void OnEnter(){

		owner.movement.Stop();

		owner.timer.value = Random.Range(1,5);
	
	}

	public override void OnExit(){

	}

	public override void OnUpdate(){

	}

}
