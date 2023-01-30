using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAgent : MonoBehaviour{

    [SerializeField] private Animator animator;

    void Start(){


        
    }

    void Update(){

        if (Input.GetKey(KeyCode.Space)){

            animator.SetFloat("Speed", 1.5f);
        
        }else{

            animator.SetFloat("Speed", 0.5f);

        }

    }

}
