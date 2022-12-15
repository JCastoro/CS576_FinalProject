using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Handles scrapbook scene updates
public class Scrapbook : MonoBehaviour
{
    public GameObject AnimalPanels;

    private GameObject DeerPanel;
    private GameObject BoarPanel; 
    private GameObject BearPanel;
    // Start is called before the first frame update
    void Start(){
        GameObject DeerPanel = AnimalPanels.transform.GetChild(0).gameObject;
        GameObject BoarPanel = AnimalPanels.transform.GetChild(1).gameObject;
        GameObject BearPanel = AnimalPanels.transform.GetChild(2).gameObject;

        Debug.Log("Missing: "+ DeerPanel.GetComponentInChildren<Text>().text);
        

        //testing to make sure they all work
        UpdateAnimalPanel(0);
        UpdateAnimalPanel(1);
        //UpdateAnimalPanel(2);
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void UpdateAnimalPanel(int AnimalIndex){
            GameObject AnimalPanel = AnimalPanels.transform.GetChild(AnimalIndex).gameObject;
            Image Image = AnimalPanel.transform.Find("ProfilePic").GetComponent<Image>();
            AnimalPanel.GetComponentInChildren<Text>().text = "FOUND";
            
            //adjusting PP
            Color TempColor = Image.color;
            TempColor.a = 0.4f;
            Image.color = TempColor;

    }

}
