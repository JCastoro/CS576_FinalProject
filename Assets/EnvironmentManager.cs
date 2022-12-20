using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour{
    public GameObject BearFood;
    public GameObject BoarFood;
    public GameObject DeerFood;

    public GameObject Bear;
    public GameObject Boar;
    public GameObject Deer;
    
    void Start(){
       GenerateGameObject(BearFood, 30, 50, 150, 50, 150); 
       GenerateGameObject(Bear, 5, 50, 150, 50, 150); 

       GenerateGameObject(BoarFood, 30, 150, 250, 150, 250); 
       GenerateGameObject(Boar, 5, 150, 250, 150, 250); 

       GenerateGameObject(DeerFood, 30, 50, 250, 50, 250);
       GenerateGameObject(Deer, 5, 50, 250, 50, 250); 
   }

    void GenerateGameObject(GameObject gameObject, int numObjects, float minX, float maxX, float minZ, float maxZ){
        for(int i=0; i<numObjects; ++i){
            Vector3 GameObjectPosition = new Vector3(
                    Random.Range(minX, maxX),
                    1f, // maxY?
                    Random.Range(minZ, maxZ)
            );

            GameObject InstantiatedGameObject = (GameObject) Instantiate(
                    gameObject,
                    GameObjectPosition,
                    Quaternion.identity
            );

            Ray GameObjectRay = new Ray(InstantiatedGameObject.transform.position, -InstantiatedGameObject.transform.up);
            RaycastHit EnvironmentPosition;

            if (Physics.Raycast(GameObjectRay, out EnvironmentPosition) && EnvironmentPosition.collider.tag == "Terrain"){
                InstantiatedGameObject.transform.position = new Vector3(
                        EnvironmentPosition.point.x,
                        EnvironmentPosition.point.y + 1,
                        EnvironmentPosition.point.z
                );
            }
        }

    }
}
