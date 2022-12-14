using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum AnimatorState {
    Idle = 0,
    Walking = 1,
    Running = 2,
    Eating = 3,
    Dieing = 4,
    Attacking = 5
}

public class Animal : MonoBehaviour{
    // TODO Convert to Pascal naming convention
    private Vector3 Direction;
    private Vector3 Velocity;
    private Vector3 CurrentDestination;
    protected string FoodPreference;

    private float FleeDistance;

    //navmesh testing
    protected NavMeshAgent agent;
    private float ArrivalTime = -1f;
    [Range(0,100)] public float speed;
    [Range(10,300)] public float ScavengeAreaMinX;
    [Range(10,300)] public float ScavengeAreaMinY;
    [Range(10,300)] public float ScavengeAreaMaxX;
    [Range(10,300)] public float ScavengeAreaMaxY;

    private bool isWalkingToFood = false;
    private GameObject currFood = null;
 
    // Detection Sphere
    private SphereCollider SphereOfAwareness;

    // Animator Notes (check AnimatorState)
    protected Animator animation_controller;

    protected virtual void Start(){
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0f;
        agent.speed = speed;
        agent.SetDestination(RandomNavMeshLocationGRID());

        animation_controller = GetComponent<Animator>();

        SphereOfAwareness = GetComponent<SphereCollider>();
    }

    protected virtual void Update(){
        if(Time.time < 1f)
            return;

        if(agent.remainingDistance <= agent.stoppingDistance){//if at our currentDestination
            //flag to see if we have first arrived somewhere
            if (ArrivalTime == -1f){
                ArrivalTime = Time.time;
                Debug.Log("Arrival Time: " + (ArrivalTime));
            } else if(Time.time - ArrivalTime > 5f){
                //if waiting time is over, set new destination
                agent.SetDestination(RandomNavMeshLocationGRID());
                animation_controller.SetInteger("AnimationState", (int) AnimatorState.Walking);
                StartCoroutine("arrivalDelay"); 
            } else{
                //do while waiting
                if(isWalkingToFood && currFood!=null){// if we are walking to food
                    Debug.Log("eating food");
                    StartCoroutine("Eating",currFood);
                }
            }
        }

        else{
            animation_controller.SetInteger("AnimationState", (int) AnimatorState.Walking);
        }
    }

    public Vector3 RandomNavMeshLocationGRID(){
        Vector3 finalPosition = Vector3.zero;
        //gets a random point in 3d space within a sphere the size of our WalkRadius
        float randomPositionX = Random.Range(ScavengeAreaMinX,ScavengeAreaMaxX);
        float randomPositionZ = Random.Range(ScavengeAreaMinY,ScavengeAreaMaxY);
        Vector3 randomPosition = new Vector3(randomPositionX,transform.position.y,randomPositionZ);

        return randomPosition;
    }  

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == FoodPreference){
            // Checks for food preference
            agent.SetDestination(other.gameObject.transform.position);
            isWalkingToFood=true;
            currFood = other.gameObject;
        } else if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Player>().isCrouching == true){
            // Checks for player presence (i.e. not crouching)
            Flee(other.gameObject.transform.position);
        }
    }

    IEnumerator arrivalDelay(){
        Debug.Log("coroutine arrival delay");
        yield return new WaitForSeconds(0.05f);
        Debug.Log("delay over");
        ArrivalTime = -1f; 
    }

    IEnumerator Eating(GameObject Food){
        Debug.Log("coroutine eating food" );
        animation_controller.SetInteger("AnimationState", (int) AnimatorState.Eating);
        yield return new WaitForSeconds(2f);
        Destroy(Food);
    }


    public void Flee(Vector3 CurrPlayerPosition){
        // TODO: add option to make animals flee further away
        // TODO: check edge of map
        agent.speed = 2*agent.speed;
        animation_controller.SetInteger("AnimationState", (int) AnimatorState.Running);
        Vector3 fleeDirection = (CurrPlayerPosition - agent.transform.position).normalized;
    }
}
