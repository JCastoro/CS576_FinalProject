using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject ScrapbookOverlay;
    public Player Player;
    private static float TimePerObjective=180f;
    public int currAnimalIndex;


    //HUD and its children
    public Canvas HUD;
    private GameObject CurrentObjectivePanel;
    private GameObject TimeRemainingPanel;
    private GameObject CameraMask;

    //Objective tracking
    public List<string> AnimalsToFind=new List<string>(){
        "Deer",
        "Boar",
        "Bear"
        };
    public Sprite[] spriteArray;
    private GameObject objectiveName;
    private GameObject objectiveImage;

    //timer
    public float TimeRemaining;
    private Text TimeRemainingText;




    // Start is called before the first frame update
    void Start(){
        //pull children of HUD
            //time
        TimeRemainingPanel = HUD.transform.Find("TimeRemaining").gameObject;
        TimeRemainingText = HUD.transform.Find("TimeRemaining").Find("TimeRemainingText").gameObject.GetComponent<Text>();
            //objectives
        currAnimalIndex=0;

        CurrentObjectivePanel = HUD.transform.Find("CurrentObjective").gameObject;
        objectiveName = CurrentObjectivePanel.transform.GetChild(2).gameObject;
        objectiveImage = CurrentObjectivePanel.transform.GetChild(0).gameObject;
        SetHUDbyAnimalIndex(currAnimalIndex);
            //camera mask
        CameraMask = HUD.transform.Find("CamMask").gameObject;

        ScrapbookOverlay.SetActive(false);

        //timer
        TimeRemaining = TimePerObjective;//3 min initial timer
        TimeRemainingText.text = "Time Remaining: "+ TimeRemaining.ToString();
    }

    // Update is called once per frame
    void Update(){
        CheckifDisplayScrapbook();
        updateTime(TimeRemainingText,ref TimeRemaining);
        //to be called when player captures the animal picture.
        //NextAnimalObjective(ref currAnimalIndex);

        if(Player.isTakingPicture){
            CameraMask.SetActive(true);
            Debug.Log("cam");
        }
        else
            CameraMask.SetActive(false);
    }

    void CheckifDisplayScrapbook(){
         
        // Debug.Log(ScrapbookOverlay);
        if(Player.isPressingTab){
            ScrapbookOverlay.SetActive(true);
        }
        else{
            ScrapbookOverlay.SetActive(false);
        }
    }

//moves us to the next objective
    void NextAnimalObjective(ref int currAnimalIndex){
        if(currAnimalIndex<AnimalsToFind.Count){
            currAnimalIndex+=1;
            objectiveName.GetComponent<Text>().text=AnimalsToFind[currAnimalIndex];
            objectiveImage.GetComponent<Image>().sprite = spriteArray[currAnimalIndex]; 
            ScrapbookOverlay.GetComponent<Scrapbook>().MarkAnimalFound(currAnimalIndex-1);
        }
        else{
            Debug.Log("Called next animal object when list was complete.");
        }
       
    }
    public void SetHUDbyAnimalIndex(int currAnimalIndex){
        objectiveName.GetComponent<Text>().text=AnimalsToFind[currAnimalIndex];
        objectiveImage.GetComponent<Image>().sprite = spriteArray[currAnimalIndex]; 
    }

    public void updateTime(Text timeRemainingText, ref float timeRemaining){
        if(timeRemaining>0f){
            timeRemaining-=Time.deltaTime;
            float minutes = Mathf.FloorToInt(timeRemaining / 60);
            float seconds = Mathf.FloorToInt(timeRemaining % 60);
            timeRemainingText.text = "Time Remaining: "+ minutes.ToString()+":"+seconds.ToString();
        }
        else{
            SceneManager.LoadScene("GameOver");
            //change scenes to game over.
        }


    }
}
