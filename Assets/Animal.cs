using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Animal : MonoBehaviour{

    private Vector3 Direction;
    private Vector3 Velocity;
    private Vector3 CurrentDestination;
    private GameObject Food;

    private float FleeDistance;


    //navmesh testing
    public NavMeshAgent agent;
    [Range(0,100)] public float speed;
    [Range(0,500)] public float walkRadius;

    //animator
    private Animator animation_controller;



    // Start is called before the first frame update
    void Start(){

        agent = GetComponent<NavMeshAgent>();
        if(agent != null){
            agent.speed = speed;
            agent.SetDestination(RandomNavMeshLocation());
        }

        animation_controller = GetComponent<Animator>();
        animation_controller.SetBool("isWalking",true);
        
    }

    public Vector3 RandomNavMeshLocation(){
        Vector3 finalPosition = Vector3.zero;
        //gets a random point in 3d space within a sphere the size of our WalkRadius
        Vector3 randomPosition = Random.insideUnitSphere*walkRadius;
        //makes it so this sphere is centered around agent
        randomPosition += transform.position;

        // if our chosen position is found on the navMesh
        //position is found by projecting input point onto navMesh along vertical axis.
        //hit is point on navMesh we projected to.
        if(NavMesh.SamplePosition(randomPosition,out NavMeshHit hit, walkRadius,1)){
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    // Update is called once per frame
    void Update(){
        // If not doing any other animation
        //Scavenge()
        if(agent != null && agent.remainingDistance <= agent.stoppingDistance){
            agent.SetDestination(RandomNavMeshLocation());
            animation_controller.SetBool("isWalking",true);
        }
        else{
            //animation_controller.SetBool("isWalking",false);

        }
    }

    //Finds nearest food of animals preference___walk animation when moving
    // COULD have a timer where idle animation is played
    public void Scavenge(GameObject FoodPreference){
        // OnTriggerEnter 
        // Eat
    }

    // triggers eat animation and deletes food game object upon completion
    // animal will keep eating if it detects player. this is a feature.
    public void Eat(GameObject Food){// make this a coroutine? whatever the hell that is....
        // Walks to food
        // If distance between animal and food < 0.5m trigger eat animation and destroy plant
    }


    // run away from player
    public void Flee(){
        // animal keeps running until set distance away from player.
            //could use this to control difficulty in game, harder animals run further away??

    }
}
