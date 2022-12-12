using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    private Player player;
    public GameObject playerHolder;
    public Canvas hud;


    //hud aspects
    public Text timeRemainingText;
    public GameObject cameraMask;
    public float timeRemaining;
    // Start is called before the first frame update

    public List<string> animalsToFind=new List<string>(){
        "Deer",
        "Boar",
        "Bear"
        };

    void Start()
    {
        player = playerHolder.GetComponent<Player>();
        Debug.Log(player);
        hud = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Canvas>();
        Debug.Log(hud);
        timeRemaining = 180f;//3 min initial timer
        timeRemainingText.text = "Time Remaining: "+ timeRemaining.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        //list of animals needed to find, when one is removed we change HUD behavior.
        updateTime(timeRemainingText,ref timeRemaining);

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
}
