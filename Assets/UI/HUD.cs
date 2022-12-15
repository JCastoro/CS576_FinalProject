using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    private Player player;
    public GameObject playerHolder;
    public GameObject objectiveHolder;
    public Canvas hud;


    //hud aspects
    public Text timeRemainingText;
    public GameObject cameraMask;
    public float timeRemaining;

        //objectives
    public int currAnimalIndex;
    private GameObject objectiveName;
    private GameObject objectiveImage;

    public List<string> animalsToFind=new List<string>(){
        "Deer",
        "Boar",
        "Bear"
        };
    public Sprite[] spriteArray;

    void Start()
    {
        player = playerHolder.GetComponent<Player>();
        Debug.Log(player);
        hud = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Canvas>();
        Debug.Log(hud);
        timeRemaining = 180f;//3 min initial timer
        timeRemainingText.text = "Time Remaining: "+ timeRemaining.ToString();

        //setting up objectives
        currAnimalIndex=0;
        objectiveName = objectiveHolder.transform.GetChild(2).gameObject;
        objectiveName.GetComponent<Text>().text=animalsToFind[currAnimalIndex];
        objectiveImage = objectiveHolder.transform.GetChild(0).gameObject;
        objectiveImage.GetComponent<Image>().sprite = spriteArray[currAnimalIndex];   
    }

    // Update is called once per frame
    void Update()
    {
        //list of animals needed to find, when one is removed we change HUD behavior.
        updateTime(timeRemainingText,ref timeRemaining);

        //changes animal objective
        updateAnimalObjective(currAnimalIndex);

        if(player.isTakingPicture){
            cameraMask.SetActive(true);
            Debug.Log("cam");
        }
        else
            cameraMask.SetActive(false);
    }

    public void updateTime(Text timeRemainingText, ref float timeRemaining){
        timeRemaining-=Time.deltaTime;
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        timeRemainingText.text = "Time Remaining: "+ minutes.ToString()+":"+seconds.ToString();


    }

    public void updateAnimalObjective(int currAnimalIndex){
        objectiveName.GetComponent<Text>().text=animalsToFind[currAnimalIndex];
        objectiveImage.GetComponent<Image>().sprite = spriteArray[currAnimalIndex]; 
    }


}
