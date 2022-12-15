using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Handles scrapbook scene updates
public class Scrapbook : MonoBehaviour
{
    public GameObject AnimalPanels;
    // Start is called before the first frame update
    void Start(){
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
