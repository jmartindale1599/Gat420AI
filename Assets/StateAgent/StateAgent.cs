using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAgent : Agent{

    public GameObject[] percieved;

    private Camera mainCamera;

    // condition parameters

    public FloatRef health = new FloatRef();
    
    public FloatRef timer = new FloatRef();
    
    public FloatRef enemyDistance = new FloatRef();

    public BoolRef enemySeen = new BoolRef();
    
    public BoolRef animationDone = new BoolRef();
    
    public BoolRef atDestination = new BoolRef();

    public StateMachine stateMachine = new StateMachine();

    void Start(){

        mainCamera = Camera.main;

        stateMachine.AddState(new IdleState(this));

        stateMachine.AddState(new PatrolState(this));

        stateMachine.AddState(new ChaseState(this));

        stateMachine.AddState(new WanderState(this));

        stateMachine.AddState(new AttackState(this));

        stateMachine.AddState(new DeathState(this));

        stateMachine.AddState(new EvadeScript(this));

        stateMachine.StartState(nameof(IdleState));

        // create conditions

        Condition timerExpiredCondition = new FloatCondition(timer, Condition.Predicate.LESS_EQUAL, 0);

        Condition enemySeenCondition = new BoolCondition(enemySeen, true);

        Condition enemyNear = new FloatCondition(enemyDistance,Condition.Predicate.LESS, 2);
        
        Condition enemyNotSeenCondition = new BoolCondition(enemySeen, false);
        
        Condition healthLowCondition = new FloatCondition(health, Condition.Predicate.LESS_EQUAL, 30);
        
        Condition healthOkCondition = new FloatCondition(health, Condition.Predicate.GREATER, 30);
        
        Condition deathCondition = new FloatCondition(health, Condition.Predicate.LESS_EQUAL, 0);

        Condition animationDoneCondition = new BoolCondition(animationDone, true);
        
        Condition atDestinationCondition = new BoolCondition(atDestination, true);

        //Create transitions, idles:

        stateMachine.AddTransition(nameof(IdleState), new Transition(new Condition[] { timerExpiredCondition }), nameof(PatrolState));

        stateMachine.AddTransition(nameof(IdleState), new Transition(new Condition[] { enemySeenCondition, healthOkCondition }), nameof(ChaseState));

        stateMachine.AddTransition(nameof(IdleState), new Transition(new Condition[] { enemySeenCondition, healthLowCondition }), nameof(EvadeScript));

        //patrols:

        stateMachine.AddTransition(nameof(PatrolState), new Transition(new Condition[] { timerExpiredCondition }), nameof(WanderState));

		stateMachine.AddTransition(nameof(PatrolState), new Transition(new Condition[] { enemySeenCondition, healthOkCondition }), nameof(ChaseState));

		stateMachine.AddTransition(nameof(PatrolState), new Transition(new Condition[] { enemySeenCondition, healthLowCondition }), nameof(EvadeScript));

        //chase

        stateMachine.AddTransition(nameof(ChaseState), new Transition(new Condition[] { enemyNotSeenCondition, timerExpiredCondition }), nameof(IdleState));

        stateMachine.AddTransition(nameof(ChaseState), new Transition(new Condition[] { enemySeenCondition, enemyNear}), nameof(AttackState));

        //wander

        stateMachine.AddTransition(nameof(WanderState), new Transition(new Condition[] { timerExpiredCondition }), nameof(IdleState));

        stateMachine.AddTransition(nameof(WanderState), new Transition(new Condition[] { enemyNear, healthOkCondition }), nameof(AttackState));

        stateMachine.AddTransition(nameof(WanderState), new Transition(new Condition[] { enemySeenCondition, healthLowCondition }), nameof(EvadeScript));

        //attack

        stateMachine.AddTransition(nameof(AttackState), new Transition(new Condition[] { enemySeenCondition, timerExpiredCondition }), nameof(ChaseState));
        
        stateMachine.AddTransition(nameof(AttackState), new Transition(new Condition[] { enemyNotSeenCondition, timerExpiredCondition }), nameof(PatrolState));

        //evade

        stateMachine.AddTransition(nameof(EvadeScript), new Transition(new Condition[] { enemyNotSeenCondition, timerExpiredCondition }), nameof(IdleState));

        //other

        stateMachine.AddAnyTransition(new Transition(new Condition[] { deathCondition }), nameof(DeathState));

        stateMachine.StartState(nameof(IdleState));

    }

    void Update(){

        percieved = perception.GetGameObjects();

        // update condition parameters

        enemySeen.value = (percieved.Length != 0);
        
        enemyDistance.value = (enemySeen) ? (Vector3.Distance(transform.position, percieved[0].transform.position)) : float.MaxValue;
        
        timer.value -= Time.deltaTime;
        
        atDestination.value = ((movement.destination - transform.position).sqrMagnitude <= 1);

        animationDone.value = (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && !animator.IsInTransition(0));

        stateMachine.Update();

        if(navigation.targetNode != null) {

            movement.MoveTowards(navigation.targetNode.transform.position);
        
        }

        animator.SetFloat("Speed", movement.velocity.magnitude);        

    }

    private void OnGUI(){

        Vector3 point = mainCamera.WorldToScreenPoint(transform.position);

        GUI.backgroundColor = Color.black;
        
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        
        Rect rect = new Rect(0, 0, 100, 20);
        
        rect.x = point.x - (rect.width / 2);
        
        rect.y = Screen.height - point.y - rect.height - 20;
        
        GUI.Label(rect, stateMachine.currentState.name);

    }

}
