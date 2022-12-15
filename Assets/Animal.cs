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
    // TODO Convert to public variable speed Pascal naming convention
    private Vector3 Direction;
    private Vector3 Velocity;
    protected string FoodPreference;

    //navmesh testing
    protected NavMeshAgent Agent;
    private float ArrivalTime = -1f;
    [Range(0,100)] public float speed;
    [Range(10,300)] public float ScavengeAreaMinX;
    [Range(10,300)] public float ScavengeAreaMinY;
    [Range(10,300)] public float ScavengeAreaMaxX;
    [Range(10,300)] public float ScavengeAreaMaxY;
    private float distanceSinceLastFootprint;
    private Vector3 lastLocation;

    private bool IsWalkingToFood = false;
    private GameObject CurrFood = null;
 
    // Detection Sphere
    private SphereCollider DetectionSphere;

    // Animator Notes (check AnimatorState)
    protected Animator AnimationController;
    protected Sprite footprint;

    protected virtual void Start(){
        Agent = GetComponent<NavMeshAgent>();
        Agent.stoppingDistance = 0f;
        Agent.speed = speed;
        Agent.SetDestination(RandomNavMeshLocationGRID());

        AnimationController = GetComponent<Animator>();
        DetectionSphere = GetComponent<SphereCollider>();
        
        distanceSinceLastFootprint=0;
        lastLocation = new Vector3(0f,0f,0f);
    }

    protected virtual void Update(){
        if(Time.time < 1f)
            return;
        leaveFootprints(footprint);



        if(Agent.remainingDistance <= Agent.stoppingDistance){//if at our currentDestination
            //flag to see if we have first arrived somewhere
            if (ArrivalTime == -1f){
                ArrivalTime = Time.time;
                Debug.Log("Arrival Time: " + (ArrivalTime));
            } else if(Time.time - ArrivalTime > 5f){
                //if waiting time is over, set new destination
                Agent.SetDestination(RandomNavMeshLocationGRID());
                AnimationController.SetInteger("AnimationState", (int) AnimatorState.Walking);
                StartCoroutine("arrivalDelay"); 
            } else{
                //do while waiting
                if(IsWalkingToFood && CurrFood!=null){// if we are walking to food
                    Debug.Log("eating food");
                    StartCoroutine("Eating", CurrFood);
                }
            }
        } else{
            AnimationController.SetInteger("AnimationState", (int) AnimatorState.Walking);
        }
    }

    // Gets a random point in 3d space within a sphere the size of our WalkRadius
    public Vector3 RandomNavMeshLocationGRID(){
        Vector3 randomPosition = new Vector3(
                Random.Range(ScavengeAreaMinX,ScavengeAreaMaxX),
                transform.position.y,
                Random.Range(ScavengeAreaMinY,ScavengeAreaMaxY)
        );

        return randomPosition;
    }  

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == FoodPreference){
            // Checks for food preference
            Agent.SetDestination(other.gameObject.transform.position);
            IsWalkingToFood=true;
            CurrFood = other.gameObject;
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

    IEnumerator Eating(GameObject food){
        Debug.Log("coroutine eating food" );
        AnimationController.SetInteger("AnimationState", (int) AnimatorState.Eating);
        yield return new WaitForSeconds(2f);
        Destroy(food);
    }
    public void leaveFootprints(Sprite footprintSprite){
        distanceSinceLastFootprint += (lastLocation - Agent.transform.position).magnitude;    
        lastLocation = Agent.transform.position;

        if(distanceSinceLastFootprint > 5f){//if agent has moved as crow flies 5f
            
            //initiallizing footprint at animals current location
            GameObject footprint = new GameObject();
            footprint.transform.position = Agent.transform.position;
            //sizing footprint
            footprint.transform.localScale=new Vector3(0.2f,0.2f,0.2f);
            //orients footprint in same direction animal is walking
            footprint.transform.forward = Agent.transform.forward;
            footprint.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
    
            //adds that animals sprite to the game object
            footprint.AddComponent<SpriteRenderer>();
            footprint.GetComponent<SpriteRenderer>().sprite=footprintSprite;
            Debug.Log("footprint placed");
            
            distanceSinceLastFootprint = 0f;
        }
    }


    public void Flee(Vector3 CurrPlayerPosition){
        // TODO: add option to make animals flee further away
        // TODO: check edge of map
        Agent.speed = 2*Agent.speed;
        AnimationController.SetInteger("AnimationState", (int) AnimatorState.Running);
        Vector3 fleeDirection = (CurrPlayerPosition - Agent.transform.position).normalized;
    }
}
