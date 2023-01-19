using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{

    [Range(1, 10)] public float maxSpeed = 5;
    
    [Range(1, 10)] public float minSpeed = 5;

    [Range(1, 100)] public float maxForce = 5;

    public Vector3 velocity { get; set; } = Vector3.zero;

    public Vector3 acceleration { get; set; } = Vector3.zero;

    public Vector3 direction { get { return velocity.normalized; } }

    // Start is called before the first frame update

    void Start(){

    }

    // Update is called once per frame
    
    void Update(){

    }

    public void ApplyForce(Vector3 force){

        acceleration += force;

    }

	public void MoveTowards(Vector3 target){

		Vector3 direction = (target - transform.position).normalized;
		
        ApplyForce(direction * maxForce);

	}

	void LateUpdate(){

        velocity += acceleration * Time.deltaTime;

        velocity = utilities.ClampMagnitude(velocity, minSpeed, maxSpeed);

        if (velocity.sqrMagnitude > 0.1f){

            transform.rotation = Quaternion.LookRotation(velocity);

        }

        transform.position += velocity * Time.deltaTime;

        acceleration = Vector3.zero;

    }

}
