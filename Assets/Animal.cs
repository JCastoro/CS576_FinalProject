using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Animal : MonoBehaviour{

    private Vector3 Direction;
    private Vector3 Velocity;
    private Vector3 CurrentDestination;
    private protected GameObject FoodPreference;

    private float FleeDistance;


    //navmesh testing
    private protected NavMeshAgent agent;
    private float ArrivalTime = -1f;
    [Range(0,100)] public float speed;
    [Range(10,300)] public float ScavengeAreaMinX;
    [Range(10,300)] public float ScavengeAreaMinY;
    [Range(10,300)] public float ScavengeAreaMaxX;
    [Range(10,300)] public float ScavengeAreaMaxY;

    private bool isWalkingToFood = false;


    //sphere colider
    private SphereCollider SphereOfAwareness;
    //animator
    private protected Animator animation_controller;

    // Start is called before the first frame update
    protected virtual void Start(){

        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance=5f;

        animation_controller = GetComponent<Animator>();
        SphereOfAwareness = GetComponent<SphereCollider>();
        if(agent != null){
            agent.speed = speed;
            agent.SetDestination(RandomNavMeshLocationGRID());
            animation_controller.SetBool("isWalking",true);
        }



        
    }
    // Update is called once per frame
    protected virtual void Update(){
        if(Time.time < 1f)
            return;
        // foreach(AnimatorControllerParameter param in animation_controller.parameters){
        //     animation_controller.SetBool(param.name,false);
        // }

        // could probably delete this, the child class update will be called
        //Scavenge(FoodPreference);
        if(agent != null && agent.remainingDistance <= agent.stoppingDistance){//if at our currentDestination

            Debug.Log("Arrivad at dest. TIme: "+ ArrivalTime);
            if (ArrivalTime == -1f){
                ArrivalTime = Time.time;
            } else if(Time.time - ArrivalTime > 5f){//if waiting time is over, set new destination
                Debug.Log("Time passed: " + (Time.time - ArrivalTime));
                agent.SetDestination(RandomNavMeshLocationGRID());
                animation_controller.SetBool("isWalking",true);
                ArrivalTime = -1f;
                isWalkingToFood = false;  
            }
            else{//waiting
                if(isWalkingToFood){
                    animation_controller.SetBool("isWalking",false);
                    animation_controller.SetBool("isEating",true); 
                }

                animation_controller.SetBool("isWalking",false);


            }
        }

        else if(false){//if we come close to food, set this as destination

        }

        else if(false){//if player gets too close, set a new destination away from player

        }
    }

    // public Vector3 RandomNavMeshLocation(){
    //     Vector3 finalPosition = Vector3.zero;
    //     //gets a random point in 3d space within a sphere the size of our WalkRadius
    //     Vector3 randomPosition = Random.insideUnitSphere*walkRadius;
    //     //makes it so this sphere is centered around agent
    //     randomPosition += transform.position;

    //     // if our chosen position is found on the navMesh
    //     //position is found by projecting input point onto navMesh along vertical axis.
    //     //hit is point on navMesh we projected to.
    //     if(NavMesh.SamplePosition(randomPosition,out NavMeshHit hit, walkRadius,1)){
    //         finalPosition = hit.position;
    //         Debug.Log(finalPosition);
    //     }
    //     return finalPosition;
    // }
    public Vector3 RandomNavMeshLocationGRID(){
        Vector3 finalPosition = Vector3.zero;
        //gets a random point in 3d space within a sphere the size of our WalkRadius
        float randomPositionX = Random.Range(ScavengeAreaMinX,ScavengeAreaMaxX);
        float randomPositionZ = Random.Range(ScavengeAreaMinY,ScavengeAreaMaxY);
        Vector3 randomPosition = new Vector3(randomPositionX,transform.position.y,randomPositionZ);
        //Debug.Log(randomPosition);

        return randomPosition;
    }  

    //returns true if animal gets close to its food of choice.
    private protected bool isFoodClose(GameObject FoodPreference){
        // OnTriggerEnter
        return true;

        return false;
       
        // Eat
    }

    private void OnTriggerEnter(Collider other){
        agent.SetDestination(other.gameObject.transform.position);
        isWalkingToFood=true;
        //Destroy(other.gameObject);
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
