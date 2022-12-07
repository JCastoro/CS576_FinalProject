using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Animal : MonoBehaviour{

    private Vector3 Direction;
    private Vector3 Velocity;
    private Vector3 CurrentDestination;
    private protected string FoodPreference;

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
    private GameObject currFood = null;
 


    //sphere colider
    private SphereCollider SphereOfAwareness;
    //animator
    private protected Animator animation_controller;

    // Start is called before the first frame update

    //Animator Notes
    /*
        0 | Idle
        1 | Walking
        2 | Running
        3 | Eating
        4 | Dieing
        5 | Attacking
    */


    protected virtual void Start(){

        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance=0f;

        animation_controller = GetComponent<Animator>();
        SphereOfAwareness = GetComponent<SphereCollider>();
        if(agent != null){
            agent.speed = speed;
            agent.SetDestination(RandomNavMeshLocationGRID());
            animation_controller.SetInteger("AnimationState",1);
        }



        
    }
    // Update is called once per frame
    protected virtual void Update(){
        if(Time.time < 1f)
            return;
        // foreach(AnimatorControllerParameter param in animation_controller.parameters){
        //     animation_controller.SetBool(param.name,false);
        // }
        if(agent.remainingDistance <= agent.stoppingDistance){//if at our currentDestination

            //flag to see if we have first arrived somewhere
            if (ArrivalTime == -1f){
                ArrivalTime = Time.time;
                Debug.Log("Arrival Time: " + (ArrivalTime));
            } else if(Time.time - ArrivalTime > 5f){//if waiting time is over, set new destination
                //Debug.Log("Time passed: " + (Time.time - ArrivalTime));
                agent.SetDestination(RandomNavMeshLocationGRID());
                animation_controller.SetInteger("AnimationState",1);
                StartCoroutine("arrivalDelay"); 
            }
            else{//do while waiting
                if(isWalkingToFood & currFood!=null){// if we are walking to food
                    Debug.Log("eating food " );
                    StartCoroutine("Eating",currFood);
                }
            }
        }

        else{
            animation_controller.SetInteger("AnimationState",1);
        }
    }

    public Vector3 RandomNavMeshLocationGRID(){
        Vector3 finalPosition = Vector3.zero;
        //gets a random point in 3d space within a sphere the size of our WalkRadius
        float randomPositionX = Random.Range(ScavengeAreaMinX,ScavengeAreaMaxX);
        float randomPositionZ = Random.Range(ScavengeAreaMinY,ScavengeAreaMaxY);
        Vector3 randomPosition = new Vector3(randomPositionX,transform.position.y,randomPositionZ);
        //Debug.Log(randomPosition);

        return randomPosition;
    }  
    private void OnTriggerEnter(Collider other){

        if(other.gameObject.tag == FoodPreference){//if animal sees the type of food it likes
            agent.SetDestination(other.gameObject.transform.position);
            isWalkingToFood=true;
            currFood = other.gameObject;

        }
        else if(other.gameObject.tag == "Player"&& other.gameObject.GetComponent<Player>().isCrouching == true){//will need to check for if player is crouching
            Flee(other.gameObject.transform.position);
        }
    }

    // triggers eat animation and deletes food game object upon completion
    // animal will keep eating if it detects player. this is a feature.
    public void Eat(GameObject Food){// make this a coroutine? whatever the hell that is....
        // Walks to food
        // If distance between animal and food < 0.5m trigger eat animation and destroy plant
        Debug.Log("eating food " );
        animation_controller.SetInteger("AnimationState",3);
        Destroy(Food);
    }

    IEnumerator arrivalDelay(){
        yield return new WaitForSeconds(0.05f);
        Debug.Log("delay over");
        ArrivalTime = -1f; 
    }

    IEnumerator Eating(GameObject Food){
        Debug.Log("eating food " );
        animation_controller.SetInteger("AnimationState",3);
        yield return new WaitForSeconds(2f);
        Destroy(Food);
    }


    // run away from player
    public void Flee(Vector3 CurrPlayerPosition){
        // animal keeps running until set distance away from player.
            //could use this to control difficulty in game, harder animals run further away??
        agent.speed = 2*agent.speed;
        animation_controller.SetInteger("AnimationState",2);


        //could check if we are near edge of map here so that character will run 
        //normalized direciton away from player
        Vector3 fleeDirection = (CurrPlayerPosition - agent.transform.position).normalized;

        //test this spot and move to it if its on the navmesh

    }
}
