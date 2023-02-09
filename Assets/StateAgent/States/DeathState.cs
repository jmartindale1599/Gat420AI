using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathState : State{

	public DeathState(StateAgent owner) : base(owner){

	}

	public override void OnEnter(){

		owner.animator.SetBool("isDead", true);
	
		owner.movement.Stop();

		Debug.Log("He died bro");

	}

	public override void OnExit(){

	}

	public override void OnUpdate(){

		if (owner.animationDone){

			GameObject.Destroy(owner.gameObject, 1);
		
		}

	}

}
