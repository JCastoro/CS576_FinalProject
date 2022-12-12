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
        
    }

    public void updateTime(Text timeRemainingText, float timeRemaining){


    }
}
