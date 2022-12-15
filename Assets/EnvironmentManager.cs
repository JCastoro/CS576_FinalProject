using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour{
    public GameObject BearFood;
    
    // TODO Add tree
    void Start(){
       GenerateGameObject(BearFood, 10, 0, 300, 0, 300); 
    }

    void GenerateGameObject(GameObject gameObject, int numObjects, float minX, float maxX, float minZ, float maxZ){
        for(int i=0; i<numObjects; ++i){
            Vector3 GameObjectPosition = new Vector3(
                    Random.Range(minX, maxX),
                    255f, // maxY?
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
                        EnvironmentPosition.point.y,
                        EnvironmentPosition.point.z
                );
            }
        }

    }
}
