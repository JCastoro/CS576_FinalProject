using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deer : Animal
{

    public Sprite deerprint;
    // Start is called before the first frame update
    protected override void Start()
    {
        //calls animal start
        base.Start();
        FoodPreference = "DeerFood";
        footprint = deerprint;
        

    }
    // Update is called once per frame
    protected override void Update()
    {
        //calls base update
        base.Update();
    }
}
