using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WanderState : State{

    private Vector3 target;

    public WanderState(StateAgent owner) : base(owner){



    }

    public override void OnEnter(){

        owner.navigation.targetNode = null;

        owner.movement.Resume();

        // create random target position around owner 

        target = owner.transform.position + Quaternion.AngleAxis(Random.Range(0,360),Vector3.up).eulerAngles;

        owner.movement.MoveTowards(target);

    }

    public override void OnExit(){

    }

    public override void OnUpdate(){

    }

}
